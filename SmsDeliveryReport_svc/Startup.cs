using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Prometheus;
using SmsDeliveryReport_svc.Configurations;
using SmsDeliveryReport_svc.Data;
using SmsDeliveryReport_svc.Examples.Configurations;
using SmsDeliveryReport_svc.Filters;
using SmsDeliveryReport_svc.Helpers;
using SmsDeliveryReport_svc.Middlewares;
using SmsDeliveryReport_svc.Startups;
using System;

namespace SmsDeliveryReport_svc
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

            if (configuration.GetSection("Project") is null) throw new ArgumentNullException("Project is not setted in appsetting.json");

            ProjectConfiguration = configuration.GetSection("Project")?.Get<ProjectSetting>();
            OAuthConfiguration = configuration.GetSection("OAuth")?.Get<OAuthSetting>();
            MasstransitConfiguration = configuration.GetSection("Masstransit")?.Get<MasstransitSetting>();
            QuartzConfiguration = configuration.GetSection("Quartz")?.Get<QuartzSetting>();
            ServiceURL = configuration.GetSection("ServiceURL")?.Get<ServiceURL>();
        }

        private IConfiguration Configuration { get; }
        private ProjectSetting ProjectConfiguration { get; }
        private OAuthSetting OAuthConfiguration { get; }
        private MasstransitSetting MasstransitConfiguration { get; }
        private QuartzSetting QuartzConfiguration { get; }
        private ServiceURL ServiceURL { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers(options =>
            {
                options.Filters.Add(typeof(ErrorFilter));
                options.Filters.Add(new ValidateModelFilter());
            })
                .AddNewtonsoftJson(options =>
                {
                    //ReferenceLoopHandling : ????????????????????????????????? dto ??????????????? dto;
                    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;

                    // NullValueHandling : ?????????????????????????????? null ????????????????????????????????? response;
                    options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                });

            services.AddHttpContextAccessor();

            services.AddResponseCaching();

            // CORS *
            services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
                builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()));

            // HealthChecks
            services.AddHealthChecks()
                .AddDbContextCheck<AppDBContext>(tags: new[] { "ready" })
                .ForwardToPrometheus();

            // AutoMapper *
            services.AddAutoMapper(typeof(Startup));

            // DBContext *
            services.AddDbContext<AppDBContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            // Masstransit (RabbitMQ & Kafka)
            services.AddMassTransit(ProjectConfiguration, MasstransitConfiguration);

            // Dependency Injection *
            services.ConfigDependency(Configuration);

            // Quart (Cron Job) *
            services.AddQuart(QuartzConfiguration);

            // Swagger (API Document) *
            services.AddSwaggerWithOAuth(ProjectConfiguration, OAuthConfiguration);

            // Authenication *
            services.AddAuthorizationWithOAuth(OAuthConfiguration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMiddleware<ExceptionHandlingMiddleware>();

            app.UseSwaggerWithOAuth(ProjectConfiguration);

            //app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseHttpMetrics();

            app.UseResponseCaching();

            app.UseAuthentication();

            app.UseAuthorization();

            // CORS
            app.UseCors("MyPolicy");

            // HealthChecks
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHealthChecks("/health/ready", new HealthCheckOptions
                {
                    ResponseWriter = HealthCheckResponseWriter.WriteResponseReadiness,
                    Predicate = (check) => check.Tags.Contains("ready")
                });

                endpoints.MapHealthChecks("/health/live", new HealthCheckOptions
                {
                    ResponseWriter = HealthCheckResponseWriter.WriteResponseLiveness,
                    Predicate = (check) => !check.Tags.Contains("ready")
                });

                endpoints.MapMetrics();
            });

            app.UseEndpoints(endpoints => endpoints.MapControllers());
        }
    }
}