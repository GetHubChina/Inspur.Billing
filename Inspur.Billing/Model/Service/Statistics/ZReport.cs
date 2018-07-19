using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inspur.Billing.Model.Service.Statistics
{
    public class ZReport
    {
        public string ReportNumber { get; set; }
        public string DateTime { get; set; }
        public double TotalSales { get; set; }
        public double TotalTax { get; set; }
        public double InvoiceQuantity { get; set; }
    }
}
