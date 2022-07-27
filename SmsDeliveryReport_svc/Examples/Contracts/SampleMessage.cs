using SmsDeliveryReport_svc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmsDeliveryReport_svc.Examples.Contracts
{
    public class SampleMessage : IId
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

    }
}
