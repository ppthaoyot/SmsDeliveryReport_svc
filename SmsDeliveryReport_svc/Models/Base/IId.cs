using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmsDeliveryReport_svc.Models
{
    public interface IId
    {
        public Guid Id { get; set; }
    }
}