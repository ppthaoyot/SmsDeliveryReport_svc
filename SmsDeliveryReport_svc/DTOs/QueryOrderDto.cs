using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmsDeliveryReport_svc.DTOs
{
    public class QuerySortDto
    {
        public string OrderingField { get; set; } = null;
        public bool AscendingOrder { get; set; } = true;
    }
}
