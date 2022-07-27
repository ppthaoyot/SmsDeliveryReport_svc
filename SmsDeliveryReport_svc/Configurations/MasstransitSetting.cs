using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmsDeliveryReport_svc.Configurations
{
    public class MasstransitSetting
    {
        public bool EnableRabbitMQ { get; set; }
        public RabbitMQSetting RabbitMQSetting { get; set; }
        public bool EnableKafka { get; set; }
        public KafkaSetting KafkaSetting { get; set; }
    }

}
