using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inspur.Billing.Commom
{
    public class TaxCalculation
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mode">计税方式</param>
        /// <param name="price">单价</param>
        /// <param name="count">数量</param>
        /// <param name="rate">税率</param>
        /// <param name="fixTaxAmount">固定税额</param>
        /// <returns></returns>
        public static double Calculation(string mode, double price, double count, double rate, double fixTaxAmount)
        {
            double result = 0;
            //不含税 价格+税款
            if (mode == "1")
            {
                //固定税额
                result = count * fixTaxAmount;
            }
            else
            {
                //税率
                result = price * count * rate;
            }
            return result;
        }
    }
}
