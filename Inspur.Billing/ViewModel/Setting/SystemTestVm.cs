using CommonLib.Net;
using ControlLib.Controls.Dialogs;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Inspur.Billing.Commom;
using Inspur.Billing.Model;
using Inspur.Billing.Model.Service.Status;
using Newtonsoft.Json;
using NLog;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Inspur.Billing.ViewModel.Setting
{
    public class SystemTestVm : ViewModelBase
    {
        #region 字段
        /// <summary>
        /// 发送按钮是否可用
        /// </summary>
        private bool _isCanSend = true;
        /// <summary>
        /// 日志对象
        /// </summary>
        Logger _logger = LogManager.GetCurrentClassLogger();
        /// <summary>
        /// 串口客户端
        /// </summary>
        SerialClient _client;
        /// <summary>
        /// 网口客户端（应该抽象一个类，统一串口和网口行为，时间关系，以后在做）
        /// </summary>
        TcpHelper _netClient;
        #endregion

        #region 属性
        /// <summary>
        /// 获取或设置请求报文
        /// </summary>
        private string _request;
        /// <summary>
        /// 获取或设置请求报文
        /// </summary>
        public string Request
        {
            get { return _request; }
            set { Set<string>(ref _request, value, "Request"); }
        }
        /// <summary>
        /// 获取或设置返回报文
        /// </summary>
        private string _response;
        /// <summary>
        /// 获取或设置返回报文
        /// </summary>
        public string Response
        {
            get { return _response; }
            set { Set<string>(ref _response, value, "Response"); }
        }
        /// <summary>
        /// 获取或设置
        /// </summary>
        private List<string> _cmdList = new List<string> { "Status", "Sign" };
        /// <summary>
        /// 获取或设置
        /// </summary>
        public List<string> CmdList
        {
            get { return _cmdList; }
            set { Set<List<string>>(ref _cmdList, value, "CmdList"); }
        }
        /// <summary>
        /// 获取或设置
        /// </summary>
        private string _cmd;
        /// <summary>
        /// 获取或设置
        /// </summary>
        public string Cmd
        {
            get { return _cmd; }
            set { Set<string>(ref _cmd, value, "Cmd"); }
        }
        #endregion

        #region 命令
        /// <summary>
        /// 获取或设置
        /// </summary>
        private ICommand _sendCommand;
        /// <summary>
        /// 获取或设置
        /// </summary>
        public ICommand SendCommand
        {
            get
            {
                return _sendCommand ?? (_sendCommand = new RelayCommand<string>(p =>
                {
                    try
                    {
                        switch (Const.Locator.ParameterSetting.CommModel)
                        {
                            case Commom.CommModel.NetPort://网口通讯
                                if (string.IsNullOrWhiteSpace(Cmd))
                                {
                                    throw new Exception("Request cmd can not be null.");
                                }
                                if (string.IsNullOrWhiteSpace(Request))
                                {
                                    throw new Exception("Request can not be null.");
                                }

                                _netClient = new TcpHelper();
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
                                bool isConn = _netClient.Connect(IPAddress.Parse(sdc[0]), int.Parse(sdc[1]));
                                if (!isConn)
                                {
                                    MessageBoxEx.Show("Failed to connect to E-SDC.", MessageBoxButton.OK);
                                    return;
                                }

                                _netClient.Complated -= _client_Complated;
                                _netClient.Complated += _client_Complated;
                                switch (Cmd)
                                {
                                    case "Status":
                                        _netClient.Send(0x01, Request);
                                        break;
                                    case "Sign":
                                        _netClient.Send(0x02, Request);
                                        break;
                                    default:
                                        break;
                                }
                                _netClient.ReciveAsync();
                                break;
                            case Commom.CommModel.SerialPort://串口通讯
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
                                if (string.IsNullOrWhiteSpace(Cmd))
                                {
                                    throw new Exception("Request cmd can not be null.");
                                }
                                if (string.IsNullOrWhiteSpace(Request))
                                {
                                    throw new Exception("Request can not be null.");
                                }
                                _client = new SerialClient(Const.Locator.ParameterSetting.SelectedPort,
                                   int.Parse(Const.Locator.ParameterSetting.SelectedBaudRate),
                                   (Parity)Enum.Parse(typeof(Parity), Const.Locator.ParameterSetting.SelectedParity),
                                   int.Parse(Const.Locator.ParameterSetting.SelectedDataBits),
                                   (StopBits)Enum.Parse(typeof(StopBits), Const.Locator.ParameterSetting.SelectedStopBits));
                                _client.Open();
                                _isCanSend = false;
                                _client.Complated += _client_Complated;
                                switch (Cmd)
                                {
                                    case "Status":
                                        _client.Send(0x01, Request);
                                        break;
                                    case "Sign":
                                        _client.Send(0x02, Request);
                                        break;
                                    default:
                                        break;
                                }
                                break;
                            default:
                                break;
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBoxEx.Show(ex.Message, MessageBoxButton.OK);
                    }
                    finally
                    {
                        _isCanSend = true;
                    }
                }, a =>
                {
                    return _isCanSend;
                }));
            }
        }

        private void _client_Complated(object sender, MessageModel e)
        {
            try
            {
                _isCanSend = true;
                Response = e.Message;
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
            }
        }
        #endregion
    }
}
