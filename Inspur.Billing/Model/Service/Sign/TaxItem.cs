using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inspur.Billing.Model.Service.Sign
{
    public class TaxItem
    {
        public string Label{ get; set; }
        public string CategoryName{ get; set; }
        public double Rate { get; set; }
        public double Amount{ get; set; }
    }
}
