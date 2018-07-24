using ControlLib.Controls.Dialogs;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using Inspur.Billing.Commom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Inspur.Billing.ViewModel.Setting
{
    public class PrintSettingVm : ViewModelBase
    {
        #region 构造函数
        public PrintSettingVm()
        {
            Printer.Instance.PrintPort = PrintPort;
        }
        #endregion

        #region 属性
        /// <summary>
        /// 获取或设置打印端口
        /// </summary>
        private string _printPort = "SP-USB1";
        /// <summary>
        /// 获取或设置打印端口
        /// </summary>
        public string PrintPort
        {
            get { return _printPort; }
            set { Set<string>(ref _printPort, value, "Port"); }
        }
        #endregion

        #region 命令
        /// <summary>
        /// 获取或设置设置中使用的命令 此处使用同一个命令，使用参数区分不同操作，如果本身需要传递参数则可以另行定义命令
        /// </summary>
        private ICommand _command;
        /// <summary>
        /// 获取或设置设置中使用的命令 此处使用同一个命令，使用参数区分不同操作，如果本身需要传递参数则可以另行定义命令
        /// </summary>
        public ICommand Command
        {
            get
            {
                return _command ?? (_command = new RelayCommand<string>(p =>
                {
                    try
                    {
                        Printer.Instance.PrintPort = PrintPort;
                        Printer.Instance.PrintTestPaper();
                    }
                    catch (Exception ex)
                    {
                        MessageBoxEx.Show(ex.Message, MessageBoxButton.OK);
                    }
                }, a =>
                {
                    return true;
                }));
            }
        }
        #endregion
    }
}
