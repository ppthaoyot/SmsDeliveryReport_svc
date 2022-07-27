using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmsDeliveryReport_svc.DTOs.Auth
{
    public class LoginDetailDto
    {
        public string Token { get; set; }
        public string SubjectId { get; set; }
        public int UserId { get; set; }
        public string EmployeeCode { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public int BranchId { get; set; }
        public string Branchname { get; set; }
    }
}