using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmsDeliveryReport_svc.Examples.Clients;
using SmsDeliveryReport_svc.Examples.Services;
using SmsDeliveryReport_svc.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmsDeliveryReport_svc.Examples.Controllers
{
    [Authorize(Policy = Permission.Base)]
    [Route("/api/Example")]
    public class ExampleContoller : ControllerBase
    {
        private readonly IExampleServices _services;

        public ExampleContoller(IExampleServices services, SendSmsClient sendSmsClient)
        {
            _services = services;
        }

        //[Authorize(Policy =Permission.TestPermission)]
        //[Authorize(Policy =Permission.TestPermission2)]
        [ClaimPermission(Permission.Test, Permission.Test2)]
        [HttpGet("Get")]
        public IActionResult Get()
        {
            return Ok();
        }
    }
}
