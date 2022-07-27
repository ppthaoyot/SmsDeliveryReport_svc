using MassTransit;
using SmsDeliveryReport_svc.Examples.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmsDeliveryReport_svc.Examples.Consumers
{
    public class SampleKafkaConsumer : IConsumer<SampleMessage>
    {
        public Task Consume(ConsumeContext<SampleMessage> context)
        {
            throw new NotImplementedException();
        }
    }
}
