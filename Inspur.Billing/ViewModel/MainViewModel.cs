using CommonLib.Net;
using ControlLib.Controls.Dialogs;
using DataModels;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Inspur.Billing.Commom;
using Inspur.Billing.Model;
using Inspur.Billing.Model.Service.Attention;
using Inspur.Billing.Model.Service.Status;
using LinqToDB;
using Newtonsoft.Json;
using NLog;
using System;
using System.IO.Ports;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Timers;
using System.Windows;
using System.Windows.Input;

namespace Inspur.Billing.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        /// <summary>
        /// 日志对象
        /// </summary>
        Logger _logger = LogManager.GetCurrentClassLogger();
        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
            Timer timer = new Timer(3000);
            timer.Elapsed += Timer_Elapsed;
            timer.AutoReset = true;
            timer.Start();


            Timer sendStatustimer = new Timer(60000);
            sendStatustimer.Elapsed += SendStatustimer_Elapsed;
            sendStatustimer.AutoReset = true;
            sendStatustimer.Start();
            LoadPosInfo();
        }

        private void SendStatustimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            CheckStatue();
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            CheckIsOnline();
        }

        private void CheckIsOnline()
        {
            try
            {
                switch (Const.Locator.ParameterSetting.CommModel)
                {
                    case CommModel.NetPort:
                        TcpHelper helper = new TcpHelper();
                        string[] sdc = Const.Locator.ParameterSetting.SdcUrl.Split(':');
                        if (sdc != null && sdc.Count() != 2)
                        {
                            _logger.Info("EFD URL is not in the right format.");
                            SetIsOnline(false);
                            return;
                        }
                        try
                        {
                            SetIsOnline(helper.Connect(IPAddress.Parse(sdc[0]), int.Parse(sdc[1])));
                        }
                        catch
                        {
                            SetIsOnline(false);
                        }
                        break;
                    case CommModel.SerialPort:
                        string[] ports = SerialPort.GetPortNames();
                        if (ports != null && ports.Count() > 0)
                        {
                            if (ports.Contains(Const.Locator.ParameterSetting.SelectedPort))
                            {
                                SetIsOnline(true);
                            }
                            else
                            {
                                SetIsOnline(false);
                            }
                        }
                        else
                        {
                            SetIsOnline(false);
                        }
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
            }
        }


        private void SetIsOnline(bool isOnline)
        {
            _isOnline = isOnline;
            if (isOnline)
            {
                OnLineVisibility = Visibility.Visible;
                OffLineVisibility = Visibility.Collapsed;
            }
            else
            {
                OnLineVisibility = Visibility.Collapsed;
                OffLineVisibility = Visibility.Visible;
            }
        }
        #region 属性
        /// <summary>
        /// 获取或设置
        /// </summary>
        private string _uri = "Issue/CreditView.xaml";
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
        private string _message;
        /// <summary>
        /// 获取或设置
        /// </summary>
        public string Message
        {
            get { return _message; }
            set { Set<string>(ref _message, value, "Message"); }
        }
        /// <summary>
        /// 获取或设置
        /// </summary>
        private Visibility _onLineVisibility = Visibility.Visible;
        /// <summary>
        /// 获取或设置
        /// </summary>
        public Visibility OnLineVisibility
        {
            get { return _onLineVisibility; }
            set { Set<Visibility>(ref _onLineVisibility, value, "OnLineVisibility"); }
        }
        /// <summary>
        /// 获取或设置
        /// </summary>
        private Visibility _offLineVisibility = Visibility.Collapsed;
        /// <summary>
        /// 获取或设置
        /// </summary>
        public Visibility OffLineVisibility
        {
            get { return _offLineVisibility; }
            set { Set<Visibility>(ref _offLineVisibility, value, "OffLineVisibility"); }
        }
        /// <summary>
        /// 获取或设置
        /// </summary>
        private bool _isBusy;
        /// <summary>
        /// 获取或设置
        /// </summary>
        public bool IsBusy
        {
            get { return _isBusy; }
            set { Set<bool>(ref _isBusy, value, "IsBusy"); }
        }

        private bool _isOnline = true;
        /// <summary>
        /// sdc是否连接
        /// </summary>
        public bool IsOnline
        {
            get { return _isOnline; }
        }
        /// <summary>
        /// pos 信息
        /// </summary>
        public PosInfo PosInfo { get; set; }
        #endregion

        #region 命令
        /// <summary>
        /// 获取或设置
        /// </summary>
        private ICommand _navigationCommand;
        /// <summary>
        /// 获取或设置
        /// </summary>
        public ICommand NavigationCommand
        {
            get
            {
                return _navigationCommand ?? (_navigationCommand = new RelayCommand<string>(p =>
                {
                    if (p == null || string.IsNullOrEmpty(p))
                    {
                        return;
                    }
                    Uri = p;
                }, a =>
                {
                    return true;
                }));
            }
        }
        /// <summary>
        /// 获取或设置
        /// </summary>
        private ICommand _command;
        /// <summary>
        /// 获取或设置
        /// </summary>
        public ICommand Command
        {
            get
            {
                return _command ?? (_command = new RelayCommand<string>(p =>
                           {
                               switch (p)
                               {
                                   case "Loaded":
                                       LoadSDCInfo();
                                       break;
                                   default:
                                       break;
                               }
                           }, a =>
                           {
                               return true;
                           }));
            }
        }

        #endregion

        #region 方法
        private void LoadSDCInfo()
        {
            var sdcInfoes = (from a in Const.dB.SdcInfo
                             select a).ToList();
            if (sdcInfoes != null && sdcInfoes.Count() > 0)
            {
                Const.Locator.ParameterSetting.SdcUrl = string.Format("{0}:{1}", sdcInfoes[0].SdcIp, sdcInfoes[0].SdcPort);
            }
        }
        public void LoadPosInfo()
        {
            var posInfo = (from a in Const.dB.PosInfo
                           select a).ToList();
            if (posInfo != null && posInfo.Count > 0)
            {
                PosInfo = posInfo[0];
            }
        }




        #region 检测状态
        public bool CheckStatue()
        {
            bool result = false;
            try
            {
                string number = "";
                string vendor = "";
                string model = "";
                string softVersion = "";
                if (Const.Locator.Main.PosInfo == null)
                {
                    _logger.Info("Pos software info is null,please check the database which named Billing.db.");
                    return false;
                }
                else
                {
                    number = Const.Locator.Main.PosInfo.SerialNumber.ToString();
                    vendor = Const.Locator.Main.PosInfo.CompanyName;
                    model = Const.Locator.Main.PosInfo.Desc;
                    softVersion = Const.Locator.Main.PosInfo.Version;
                }
                StatusRequest statusRequest = new StatusRequest()
                {
                    PosSerialNumber = number,
                    PosVendor = vendor,
                    PosModel = model,
                    PosSoftVersion = softVersion
                };
                string requestString = JsonConvert.SerializeObject(statusRequest);
                switch (Const.Locator.ParameterSetting.CommModel)
                {
                    case CommModel.NetPort:
                        StatueRequest(requestString);
                        break;
                    case CommModel.SerialPort:
                        StatueRequestSerial(requestString);
                        break;
                    default:
                        break;
                }

            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
            }
            return result;
        }


        public void StatueRequest(string requestString)
        {
            TcpHelper _statusTcpClient = new TcpHelper();
            if (string.IsNullOrWhiteSpace(Const.Locator.ParameterSetting.SdcUrl))
            {
                _logger.Info("EFD URL can not be null.", MessageBoxButton.OK);
                return;
            }
            string[] sdc = Const.Locator.ParameterSetting.SdcUrl.Split(':');
            if (sdc != null && sdc.Count() != 2)
            {
                _logger.Info("EFD URL is not in the right format.", MessageBoxButton.OK);
                return;
            }
            bool isConn = _statusTcpClient.Connect(IPAddress.Parse(sdc[0]), int.Parse(sdc[1]));
            if (!isConn)
            {
                _logger.Info("Failed to connect to EFD.", MessageBoxButton.OK);
                return;
            }
            _statusTcpClient.Complated -= _statusTcpClient_Complated;
            _statusTcpClient.Complated += _statusTcpClient_Complated;
            _statusTcpClient.Send(0x01, requestString);
            _statusTcpClient.ReciveAsync();
        }

        private void _statusTcpClient_Complated(object sender, MessageModel e)
        {
            try
            {
                if (e.MessageId == 0x03)
                {
                    //返回错误
                    ErrorInfo erroInfo = JsonConvert.DeserializeObject<ErrorInfo>(e.Message);
                    if (erroInfo != null)
                    {
                        _logger.Info(erroInfo.Description, MessageBoxButton.OK);
                        return;
                    }
                }
                if (e.MessageId != 0x01)
                {
                    return;
                }
                if (!e.IsSuccess)
                {
                    _logger.Info(e.ErrorMessage);
                    return;
                }

                StatusResponse statusResponse = JsonConvert.DeserializeObject<StatusResponse>(e.Message);
                if (statusResponse != null)
                {
                    Const.IsHasGetStatus = true;
                    //记录税种信息
                    if (statusResponse.TaxInfo != null && statusResponse.TaxInfo.Count > 0)
                    {
                        Const.dB.CodeTaxtype.Delete();
                        long id = 1;
                        foreach (var item in statusResponse.TaxInfo)
                        {
                            if (item.TaxCategory != null && item.TaxCategory.Count > 0)
                            {
                                foreach (var itm in item.TaxCategory)
                                {
                                    Const.dB.Insert<CodeTaxtype>(new CodeTaxtype
                                    {
                                        TaxtypeId = id,
                                        TaxTypeName = item.TaxTpye,
                                        TaxTypeCode = item.TaxTpye,

                                        TaxItemLable = itm.TaxLabel,
                                        TaxItemName = itm.TaxName,
                                        TaxItemCode = itm.CategoryId.ToString(),
                                        TaxRate = itm.TaxRate,
                                        EffectDate = itm.EffectiveDate,
                                        ExpireDate = itm.ExpiredDate
                                    });
                                    id++;
                                }
                            }
                        }
                    }

                    //记录monitor信息


                    if (!statusResponse.isInitialized)
                    {
                        _logger.Info("EFD is not initialized.");
                    }
                    else
                    {
                        if (statusResponse.isLocked)
                        {
                            _logger.Info("EFD is locked.");
                        }
                        else
                        {
                            if (Const.IsNeedMessage)
                            {
                                _logger.Info("EFD is available");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Info(ex.Message);
            }
        }

        public void StatueRequestSerial(string request)
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
            if (string.IsNullOrWhiteSpace(request))
            {
                throw new Exception("Request can not be null.");
            }

            StopBits ss = (StopBits)Enum.Parse(typeof(StopBits), Const.Locator.ParameterSetting.SelectedStopBits);


            SerialClient _client = new SerialClient(Const.Locator.ParameterSetting.SelectedPort,
               int.Parse(Const.Locator.ParameterSetting.SelectedBaudRate),
               (Parity)Enum.Parse(typeof(Parity), Const.Locator.ParameterSetting.SelectedParity),
               int.Parse(Const.Locator.ParameterSetting.SelectedDataBits),
               (StopBits)Enum.Parse(typeof(StopBits), Const.Locator.ParameterSetting.SelectedStopBits));

            if (_client.IsOpen)
            {
                //如果端口打开，则不发送
                return;
            }

            _client.Open();
            _client.Complated += _statusTcpClient_Complated;
            _client.Send(0x01, request);
        }
        #endregion

        #endregion
    }
}