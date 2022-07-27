using MassTransit.KafkaIntegration;
using MassTransit.Registration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmsDeliveryReport_svc.Configurations
{
    public abstract class KafkaProducerSetting
    {
        public Type Contract { get; set; }

        public string TopicName { get; set; }

        public abstract void GetProducerConfig(ref IRiderRegistrationConfigurator rider);
    }

    public class KafkaProducerSetting<TContract> : KafkaProducerSetting where TContract : class
    {
        public KafkaProducerSetting(string topicName)
        {
            Contract = typeof(TContract);
            TopicName = topicName;
        }

        public override void GetProducerConfig(ref IRiderRegistrationConfigurator rider)
        {
            rider.AddProducer<TContract>(TopicName);
        }
    }
}
