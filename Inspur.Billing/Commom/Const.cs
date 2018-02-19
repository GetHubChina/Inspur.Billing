using DataModels;
using Inspur.Billing.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

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
        private static ViewModelLocator _locator;
        /// <summary>
        /// 数据源字典
        /// </summary>
        public static ViewModelLocator Locator
        {
            get
            {
                if (_locator == null)
                {
                    _locator = (ViewModelLocator)Application.Current.Resources["Locator"];
                }
                return _locator;
            }
        }

    }
}
