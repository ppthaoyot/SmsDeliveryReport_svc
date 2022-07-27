using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmsDeliveryReport_svc.Validations
{
    public class ClaimPermissionAttribute : TypeFilterAttribute
    {
        public ClaimPermissionAttribute(params string[] permission) : base(typeof(ClaimPermissionFilter))
        {
            Arguments = permission;
            IsReusable = true;
        }
    }

    public class ClaimPermissionFilter : IAuthorizationFilter
    {
        readonly string[] _claim;

        public ClaimPermissionFilter(string[] claim)
        {
            _claim = claim;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var hasClaim = context.HttpContext.User.Claims.Any(_ => _.Type == "permission" && _claim.Contains(_.Value));

            if (!hasClaim)
            {
                context.Result = new ForbidResult();
            }
        }
    }
}
