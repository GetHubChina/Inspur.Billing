using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inspur.Billing.Model.Service.Sign
{
    public class SignGoodItem
    {
        /// <summary>
        /// Global Trade Item Number (GTIN) is an identifier for trade items, incorporated the ISBN, ISSN, ISMN, IAN (which includes the European Article Number and Japanese Article Number) and some Universal Product Codes, into a universal number space.
        /// </summary>
        public string GTIN { get; set; }
        /// <summary>
        /// Human readable name of the product or service. Required Max Length 2048 character
        /// </summary>
        public string Name { get; set; }
        public double Quantity { get; set; }
        public double UnitPrice { get; set; }
        public double Discount { get; set; }
        /// <summary>
        /// Array of labels. Each Label represents one of the Tax Rates applied on invoice item. Tax Items are calculated based on TotalAmount and applied Labels as described in Calculate Taxes section. 
        /// Required, Array of strings.In case no taxes are applicable online item this field is optional
        /// </summary>
        public string[] Labels { get; set; }
        /// <summary>
        /// Gross price for the line item, including discount. 
        /// Required, Decimal(14,2)
        /// </summary>
        public double TotalAmount { get; set; }
        
    }
}
