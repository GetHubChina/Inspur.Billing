using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inspur.Billing.Model.Service.Status
{
    public class StatusResponse
    {
        public bool IsPinRequired { get; set; }
        public bool AuditRequired { get; set; }
        public DateTime DT { get; set; }
    }
}
