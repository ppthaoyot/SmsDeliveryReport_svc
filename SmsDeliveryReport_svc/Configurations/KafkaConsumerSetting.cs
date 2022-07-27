using Confluent.Kafka;
using MassTransit;
using MassTransit.KafkaIntegration;
using MassTransit.KafkaIntegration.Configurators;
using MassTransit.Registration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmsDeliveryReport_svc.Configurations
{
    public abstract class KafkaConsumerSetting
    {

        public Type TargetConsumer { get; set; }

        public Type KeyType { get; set; }
        public Type ValueType { get; set; }

        public string TopicName { get; set; }
        protected string _ProjectName { get; set; }
        public AutoOffsetReset Offset { get; set; }
        public string GetConsumerName()
        {
            return $"consumer-{_ProjectName}-{TargetConsumer.GetType().Name}";
        }

        public abstract void GetConsumerConfig(ref IKafkaFactoryConfigurator config, IRiderRegistrationContext context);
    }

    public class KafkaConsumerSetting<TConsumer, TKey, TValue> : KafkaConsumerSetting where TValue : class
    {
        public KafkaConsumerSetting(string topicName, string projectName, AutoOffsetReset autoOffsetReset)
        {
            TargetConsumer = typeof(TConsumer);
            KeyType = typeof(TKey);
            ValueType = typeof(TValue);
            TopicName = topicName;
            _ProjectName = projectName;
            Offset = autoOffsetReset;
        }

        public override void GetConsumerConfig(ref IKafkaFactoryConfigurator config, IRiderRegistrationContext context)
        {
            config.TopicEndpoint<TKey, TValue>(TopicName, GetConsumerName(), e =>
            {
                e.AutoOffsetReset = Offset;
                e.ConfigureConsumer(context, TargetConsumer);
            });
        }
    }
}
