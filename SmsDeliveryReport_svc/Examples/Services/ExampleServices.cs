using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using SmsDeliveryReport_svc.Data;
using SmsDeliveryReport_svc.Examples.DTOs;
using SmsDeliveryReport_svc.Examples.Models;
using SmsDeliveryReport_svc.Filters;
using SmsDeliveryReport_svc.Models;
using SmsDeliveryReport_svc.Services;
using SmsDeliveryReport_svc.Services.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmsDeliveryReport_svc.Examples.Services
{
    public class ExampleServices : ServiceBase, IExampleServices
    {
        private readonly AppDBContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContext;
        private readonly ILoginDetailServices _loginDetail;

        public ExampleServices(AppDBContext dbContext, IMapper mapper, IHttpContextAccessor httpContext, ILoginDetailServices loginDetail)
            : base(dbContext, mapper, httpContext, loginDetail)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _httpContext = httpContext;
            _loginDetail = loginDetail;
        }

        public ServiceResponse<List<GetExampleReponseDto>> GetAllExample()
        {
            var examples = Examples;
            var query = Examples.AsQueryable();
            var user = _loginDetail.GetClaim();


            if (_loginDetail.CheckPermission(Permission.Test))
            {
                query = query.Where(_ => _.UserId == user.UserId);
                //ResponseResult.NotFound<GetExampleReponseDto>("Example not found");
            }

            var response = _mapper.Map<List<GetExampleReponseDto>>(examples);

            return ResponseResult.Success(response);
        }

        public ServiceResponse<GetExampleReponseDto> GetExample(GetExampleRequestDto dto)
        {
            var example = GetExampleData(dto.Id);

            if (example == null)
            {
                ResponseResult.NotFound<GetExampleReponseDto>("Example not found");
            }

            var userDetail = _loginDetail.GetClaim();

            example.UserId = userDetail.UserId;

            var response = _mapper.Map<GetExampleReponseDto>(example);

            return ResponseResult.Success(response);
        }

        private ExampleModels GetExampleData(Guid Id)
        {
            return Examples.Where(_ => _.Id == Id).FirstOrDefault();
        }

        private IList<ExampleModels> Examples = new List<ExampleModels>()
        {
            new ExampleModels()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                ExampleName = "Example 1",
                Price = 1
            },
            new ExampleModels()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000002"),
                ExampleName = "Example 2",
                Price = 2
            },
            new ExampleModels()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000003"),
                ExampleName = "Example 3",
                Price = 3
            },
            new ExampleModels()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000004"),
                ExampleName = "Example 4",
                Price = 4
            },
            new ExampleModels()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000005"),
                ExampleName = "Example 5",
                Price = 5
            },
            new ExampleModels()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000006"),
                ExampleName = "Example 6",
                Price = 6
            },
            new ExampleModels()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000007"),
                ExampleName = "Example 7",
                Price = 7
            },
            new ExampleModels()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000008"),
                ExampleName = "Example 8",
                Price = 8
            },
            new ExampleModels()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000009"),
                ExampleName = "Example 9",
                Price = 9
            },
            new ExampleModels()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000010"),
                ExampleName = "Example 10",
                Price = 10
            },
            new ExampleModels()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000011"),
                ExampleName = "Example 11",
                Price = 12
            },
            new ExampleModels()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000013"),
                ExampleName = "Example 13",
                Price = 13
            },
        };
    }
}
