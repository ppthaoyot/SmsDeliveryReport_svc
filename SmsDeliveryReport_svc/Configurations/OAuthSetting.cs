using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmsDeliveryReport_svc.Configurations
{
    public class OAuthSetting
    {
        public bool EnableOAuth { get; set; }
        public string Authority { get; set; }
        public string Audience { get; set; }
        public Dictionary<string, string> Scopes { get; set; }
    }
}