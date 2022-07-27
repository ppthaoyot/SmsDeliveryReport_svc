using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmsDeliveryReport_svc.Exceptions
{
    public class AppExceptionBase : Exception
    {
        public string ObjectTypeName { get; set; }
        public string Keys { get; set; }
    }
}