using DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inspur.Billing.Commom
{
    class Const
    {
        /// <summary>
        /// sqlite连接字符串
        /// </summary>
        public static string ConnectString = "Data Source=" + AppDomain.CurrentDomain.BaseDirectory + "/Billing.db";
        /// <summary>
        /// 数据库对象
        /// </summary>
        public static BillingDB dB = new BillingDB();
    }
}
