using SmsDeliveryReport_svc.Examples.DTOs;
using SmsDeliveryReport_svc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmsDeliveryReport_svc.Examples.Services
{
    public interface IExampleServices
    {
        ServiceResponse<GetExampleReponseDto> GetExample(GetExampleRequestDto dto);
    }
}
