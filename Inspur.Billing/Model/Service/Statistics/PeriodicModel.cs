using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inspur.Billing.Model.Service.Statistics
{
    public class PeriodicModel
    {
        public string CurrentTime { get; set; }
        public string BeginDate { get; set; }
        public string EndDate { get; set; }
        public List<ZReport> Z_Reports { get; set; }
    }
}
