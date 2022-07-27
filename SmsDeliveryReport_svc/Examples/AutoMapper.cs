using AutoMapper;
using SmsDeliveryReport_svc.Examples.Contracts;
using SmsDeliveryReport_svc.Examples.DTOs;
using SmsDeliveryReport_svc.Examples.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmsDeliveryReport_svc.Examples
{
    public class AutoMapper : Profile
    {
        public AutoMapper()
        {
            CreateMap<SampleMessage, ExampleModels>()
                .ForMember(_ => _.ExampleName, _ => _.MapFrom(_ => _.Name))
                .ReverseMap();

            CreateMap<ExampleModels, GetExampleReponseDto>();
        }
    }
}
