using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmsDeliveryReport_svc.Configurations
{
    public class QuartzSetting
    {
        public bool EnableQuartz { get; set; }
        public Dictionary<string, string> Jobs { get; set; }
    }

}
