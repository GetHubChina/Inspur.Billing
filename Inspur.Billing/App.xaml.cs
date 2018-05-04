using Inspur.Billing.Commom;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Inspur.Billing
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);
            if (ServiceHelper.TcpClient!=null&&ServiceHelper.TcpClient.IsConnected)
            {
                ServiceHelper.TcpClient.Close();
            }
        }
    }
}
