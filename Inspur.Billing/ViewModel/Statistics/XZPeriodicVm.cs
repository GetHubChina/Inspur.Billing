using ControlLib.Controls.Dialogs;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using Inspur.Billing.Commom;
using Inspur.Billing.Model;
using Inspur.Billing.Model.Service.Statistics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Inspur.Billing.ViewModel.Statistics
{
    public class XZPeriodicVm : ViewModelBase
    {
        #region 属性
        /// <summary>
        /// 获取或设置
        /// </summary>
        private DateTime? _beginTime;
        /// <summary>
        /// 获取或设置
        /// </summary>
        public DateTime? BeginTime
        {
            get { return _beginTime; }
            set { Set<DateTime?>(ref _beginTime, value, "BeginTime"); }
        }
        /// <summary>
        /// 获取或设置
        /// </summary>
        private DateTime? _endTime;
        /// <summary>
        /// 获取或设置
        /// </summary>
        public DateTime? EndTime
        {
            get { return _endTime; }
            set { Set<DateTime?>(ref _endTime, value, "EndTime"); }
        }
        /// <summary>
        /// 获取或设置
        /// </summary>
        private string _reportType;
        /// <summary>
        /// 获取或设置
        /// </summary>
        public string ReportType
        {
            get { return _reportType; }
            set { Set<string>(ref _reportType, value, "ReportType"); }
        }
        /// <summary>
        /// 获取或设置
        /// </summary>
        private List<CodeTable> _reportTypes = new List<CodeTable>
        {
            new CodeTable { Code="0",Name="X-report"},
            new CodeTable { Code="1",Name="Z-report"},
            new CodeTable { Code="2",Name="Periodic report"}
        };
        /// <summary>
        /// 获取或设置
        /// </summary>
        public List<CodeTable> ReportTypes
        {
            get { return _reportTypes; }
            set { Set<List<CodeTable>>(ref _reportTypes, value, "ReportTypes"); }
        }

        #endregion

        #region 命令
        /// <summary>
        /// 获取或设置
        /// </summary>
        private ICommand _queryCommand;
        /// <summary>
        /// 获取或设置
        /// </summary>
        public ICommand QueryCommand
        {
            get
            {
                return _queryCommand ?? (_queryCommand = new RelayCommand(() =>
                {
                    ReportRequest request = new ReportRequest();
                    if (ReportType == "1" || ReportType == "2")
                    {
                        if (BeginTime == null)
                        {
                            MessageBoxEx.Show("Please select the BeginDate.");
                            return;
                        }
                        if (EndTime == null)
                        {
                            MessageBoxEx.Show("Please select the EndDate.");
                            return;
                        }
                        request.BeginDate = BeginTime.Value.ToString("yyyyMMdd");
                        request.EndDate = EndTime.Value.ToString("yyyyMMdd");
                    }
                    request.CurrentTime = DateTime.Now.ToString("yyyyMMddHHmmss");
                    request.ReportType = int.Parse(ReportType);

                    if (Const.Locator.ParameterSetting.CommModel == CommModel.SerialPort)
                    {
                        //串口通信

                    }
                    else
                    {
                        //网口通讯
                    }
                }, () =>
                {
                    return true;
                }));
            }
        }

        #endregion
    }
}
