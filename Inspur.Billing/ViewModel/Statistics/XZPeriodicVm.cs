using CommonLib.Net;
using ControlLib.Controls.Dialogs;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using Inspur.Billing.Commom;
using Inspur.Billing.Model;
using Inspur.Billing.Model.Service.Statistics;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Inspur.Billing.ViewModel.Statistics
{
    public class XZPeriodicVm : ViewModelBase
    {
        #region 字段
        /// <summary>
        /// 请求报表的指令代码
        /// </summary>
        const byte Cmd = 0x04;
        #endregion

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
        /// <summary>
        /// 获取或设置
        /// </summary>
        private string _uri;
        /// <summary>
        /// 获取或设置
        /// </summary>
        public string Uri
        {
            get { return _uri; }
            set { Set<string>(ref _uri, value, "Uri"); }
        }

        /// <summary>
        /// 获取或设置
        /// </summary>
        private object _reportDataContext;
        /// <summary>
        /// 获取或设置
        /// </summary>
        public object ReportDataContext
        {
            get { return _reportDataContext; }
            set { Set<object>(ref _reportDataContext, value, "ReportDataContext"); }
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

                    string requestString = JsonConvert.SerializeObject(request);

                    switch (Const.Locator.ParameterSetting.CommModel)
                    {
                        case CommModel.NetPort:
                            NetRequest(requestString);
                            break;
                        case CommModel.SerialPort:
                            SerialRequest(requestString);
                            break;
                        default:
                            break;
                    }
                }, () =>
                {
                    return true;
                }));
            }
        }

        #endregion

        #region 方法
        public void NetRequest(string requestString)
        {
            if (requestString == null)
            {
                throw new ArgumentNullException("SignRequest data can not be null.");
            }
            TcpHelper _signTcpClient = new TcpHelper();
            if (string.IsNullOrWhiteSpace(Const.Locator.ParameterSetting.SdcUrl))
            {
                MessageBoxEx.Show("E-SDC URL can not be null.", MessageBoxButton.OK);
                return;
            }
            string[] sdc = Const.Locator.ParameterSetting.SdcUrl.Split(':');
            if (sdc != null && sdc.Count() != 2)
            {
                MessageBoxEx.Show("E-SDC URL is not in the right format.", MessageBoxButton.OK);
                return;
            }
            bool isConn = _signTcpClient.Connect(IPAddress.Parse(sdc[0]), int.Parse(sdc[1]));
            if (!isConn)
            {
                MessageBoxEx.Show("Failed to connect to E-SDC.", MessageBoxButton.OK);
                return;
            }

            _signTcpClient.Complated -= _signTcpClient_Complated;
            _signTcpClient.Complated += _signTcpClient_Complated;
            _signTcpClient.Send(Cmd, requestString);
            _signTcpClient.ReciveAsync();
        }

        public void SerialRequest(string requestString)
        {
            if (string.IsNullOrWhiteSpace(Const.Locator.ParameterSetting.SelectedPort))
            {
                throw new Exception("Port can not be null.");
            }
            if (string.IsNullOrWhiteSpace(Const.Locator.ParameterSetting.SelectedDataBits))
            {
                throw new Exception("DataBits can not be null.");
            }
            if (string.IsNullOrWhiteSpace(Const.Locator.ParameterSetting.SelectedBaudRate))
            {
                throw new Exception("BaudRate can not be null.");
            }
            if (string.IsNullOrWhiteSpace(Const.Locator.ParameterSetting.SelectedParity))
            {
                throw new Exception("Parity can not be null.");
            }
            if (string.IsNullOrWhiteSpace(Const.Locator.ParameterSetting.SelectedStopBits))
            {
                throw new Exception("StopBits can not be null.");
            }

            SerialClient _client = new SerialClient(Const.Locator.ParameterSetting.SelectedPort,
               int.Parse(Const.Locator.ParameterSetting.SelectedBaudRate),
               (Parity)Enum.Parse(typeof(Parity), Const.Locator.ParameterSetting.SelectedParity),
               int.Parse(Const.Locator.ParameterSetting.SelectedDataBits),
               (StopBits)Enum.Parse(typeof(StopBits), Const.Locator.ParameterSetting.SelectedStopBits));
            _client.Open();
            _client.Complated -= _signTcpClient_Complated;
            _client.Complated += _signTcpClient_Complated;
            _client.Send(Cmd, requestString);
        }

        private void _signTcpClient_Complated(object sender, MessageModel e)
        {



            Uri = "XReportView.xaml";
            ReportDataContext = new XReportModel
            {
                CurrentTime = "20180719",
                TotalSlaes = 300,
                TotalTax = 300,
                InvoiceQuantity = 1,
                TaxItems = new List<Model.Service.Statistics.ReportTaxItems>
                {
                    new Model.Service.Statistics.ReportTaxItems
                    {
                        TaxLable="A",
                        TaxName="Coffe",
                        TaxRate="12%",
                        TaxAmount="300"
                    }
                }
            }; ;


            if (e.MessageId == 0x03)
            {
                //返回错误
                ErrorInfo erroInfo = JsonConvert.DeserializeObject<ErrorInfo>(e.Message);
                if (erroInfo != null)
                {
                    MessageBoxEx.Show(erroInfo.Description, MessageBoxButton.OK);
                    return;
                }
            }
            if (e.MessageId != Cmd)
            {
                return;
            }
            ReportResponse response = JsonConvert.DeserializeObject<ReportResponse>(e.Message);
            switch (response.ReportType)
            {
                case 0:
                    Uri = "XReportView.xaml";
                    ReportDataContext = response.X;
                    break;
                case 1:
                    Uri = "ZReportView.xaml";
                    ReportDataContext = response.Z;
                    break;
                case 2:
                    Uri = "PeriodicReportView.xaml";
                    ReportDataContext = response.Periodic;
                    break;
                default:
                    break;
            }
        }
        #endregion
    }
}
