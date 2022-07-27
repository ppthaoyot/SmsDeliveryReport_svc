using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmsDeliveryReport_svc.Exceptions
{
    public class NotFoundException : AppExceptionBase
    {
        public NotFoundException(string objectTypeName)
        {
            ObjectTypeName = objectTypeName;
        }

        public override string Message => $"Object [{ObjectTypeName}] is not found.";
    }
}