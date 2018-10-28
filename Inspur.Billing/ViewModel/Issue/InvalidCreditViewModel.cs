using CommonLib.Net;
using ControlLib.Controls.Dialogs;
using DataModels;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using Inspur.Billing.Commom;
using Inspur.Billing.Model;
using Inspur.Billing.Model.Service.Sign;
using LinqToDB;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO.Ports;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Inspur.Billing.ViewModel.Issue
{
    public class InvalidCreditViewModel : ViewModelBase
    {
        #region 字段
        SignRequest signRequest;
        SignResponse signResponse;
        /// <summary>
        /// 签章请求
        /// </summary>
        public TcpHelper _signTcpClient = new TcpHelper();
        #endregion

        #region 属性
        /// <summary>
        /// 获取或设置开票日期
        /// </summary>
        private DateTime? _issueTime;
        /// <summary>
        /// 获取或设置开票日期
        /// </summary>
        public DateTime? IssueTime
        {
            get { return _issueTime; }
            set { Set<DateTime?>(ref _issueTime, value, "IssueTime"); }
        }
        /// <summary>
        /// 获取或设置
        /// </summary>
        private string _invoiceCode;
        /// <summary>
        /// 获取或设置
        /// </summary>
        public string InvoiceCode
        {
            get { return _invoiceCode; }
            set { Set<string>(ref _invoiceCode, value, "InvoiceCode"); }
        }
        /// <summary>
        /// 获取或设置
        /// </summary>
        private string _invoiceNumber;
        /// <summary>
        /// 获取或设置
        /// </summary>
        public string InvoiceNumber
        {
            get { return _invoiceNumber; }
            set { Set<string>(ref _invoiceNumber, value, "InvoiceNumber"); }
        }
        /// <summary>
        /// 获取或设置
        /// </summary>
        private ObservableCollection<InvoiceInfo> _invoiceList;
        /// <summary>
        /// 获取或设置
        /// </summary>
        public ObservableCollection<InvoiceInfo> InvoiceList
        {
            get { return _invoiceList; }
            set { Set<ObservableCollection<InvoiceInfo>>(ref _invoiceList, value, "InvoiceList"); }
        }
        /// <summary>
        /// 获取或设置选中的退票信息
        /// </summary>
        private InvoiceInfo _selectedItem;
        /// <summary>
        /// 获取或设置选中的退票信息
        /// </summary>
        public InvoiceInfo SelectedItem
        {
            get { return _selectedItem; }
            set { Set<InvoiceInfo>(ref _selectedItem, value, "SelectedItem"); }
        }
        #endregion

        #region 命令
        /// <summary>
        /// 获取或设置
        /// </summary>
        private ICommand _commonCommand;
        /// <summary>
        /// 获取或设置
        /// </summary>
        public ICommand CommonCommand
        {
            get
            {
                return _commonCommand ?? (_commonCommand = new RelayCommand<string>(p =>
                {
                    try
                    {
                        if (p != null)
                        {
                            switch (p.ToString())
                            {
                                case "Query":
                                    if (InvoiceList == null)
                                    {
                                        InvoiceList = new ObservableCollection<InvoiceInfo>();
                                    }
                                    else
                                    {
                                        InvoiceList.Clear();
                                    }
                                    var invoiceInfoes = (from a in Const.dB.InvoiceAbbreviation
                                                         where
                                                         (string.IsNullOrWhiteSpace(InvoiceCode) ? true : a.InvoiceCode == InvoiceCode) &&
                                                         (string.IsNullOrWhiteSpace(InvoiceNumber) ? true : a.InvoiceNumber == InvoiceNumber)
                                                         select a).ToList().Where(a => IsIssueDate(a.IssueDate)).ToList();
                                    if (invoiceInfoes != null && invoiceInfoes.Count > 0)
                                    {
                                        InvoiceInfo info = null;
                                        foreach (var item in invoiceInfoes)
                                        {
                                            info = new InvoiceInfo
                                            {
                                                SalesOrderNum = item.SalesorderNum,
                                                TPin = item.TaxpayerTin,
                                                IssueTime = item.IssueDate,
                                                TaxpayerName = item.TaxpayerName,
                                                TerminalID = item.TerminalId,
                                                InvoiceCode = item.InvoiceCode,
                                                InvoiceNumber = item.InvoiceNumber,
                                                TotalAmount = item.TotalAmount,
                                                CreditFlag = item.CreditFlag == "1" ? "Credit" : "Normal",
                                                BuyerTPIN = item.BuyerTin,
                                                BuyerName = item.BuyerName,
                                            };
                                            InvoiceList.Add(info);
                                        }
                                    }
                                    break;
                                case "Reset":
                                    IssueTime = null;
                                    InvoiceCode = "";
                                    InvoiceNumber = "";
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBoxEx.Show(ex.Message);
                    }
                }, a =>
                {
                    return true;
                }));
            }
        }

        private bool IsIssueDate(string issueDate)
        {
            if (string.IsNullOrWhiteSpace(issueDate))
            {
                if (IssueTime == null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            DateTime dt;
            DateTime.TryParse(issueDate, out dt);
            if (DateTime.TryParse(issueDate, out dt))
            {
                if (IssueTime == null)
                {
                    return true;
                }
                else
                {
                    if (dt.Year == IssueTime.Value.Year &&
                        dt.Month == IssueTime.Value.Month &&
                        dt.Day == IssueTime.Value.Day)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// 获取或设置退票命令
        /// </summary>
        private ICommand _invalidCommand;
        /// <summary>
        /// 获取或设置置退票命令
        /// </summary>
        public ICommand InvalidCommand
        {
            get
            {
                return _invalidCommand ?? (_invalidCommand = new RelayCommand<string>(p =>
                {
                    if (SelectedItem == null)
                    {
                        return;
                    }
                    switch (p)
                    {
                        case "Invalid"://退票命令
                            Sign(SelectedItem);
                            break;
                        case "Detail"://详情命令
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
        private void Sign(InvoiceInfo info)
        {
            signRequest = new SignRequest
            {
                PosSerialNumber = Const.Locator.Main.PosInfo.Id.ToString(),
                IssueTime = info.IssueTime,
                SaleType = 0,
                LocalPurchaseOrder = "1000582782",
                Cashier = info.Cashier,
                BuyerTPIN = info.BuyerTPIN,
                BuyerName = info.BuyerName,
                BuyerTaxAccountName = "",
                BuyerAddress = info.BuyerAddress,
                BuyerTel = info.BuyerTel,
                OriginalInvoiceCode = info.InvoiceCode,
                OriginalInvoiceNumber = info.InvoiceNumber
            };

            signRequest.TransactionType = 1;
            //if (int.TryParse(Credit.SelectedPaymentType.Code, out paymentCode))
            //{
            //    signRequest.PaymentMode = paymentCode;
            //}
            //else
            //{
            //    MessageBoxEx.Show("PaymentMode is not number.");
            //    return;
            //}

            if (!Const.Locator.OperationModeVm.IsNormal)
            {
                if (Const.Locator.OperationModeVm.IsTest)
                {
                    signRequest.OperationMode = 1;
                }
                if (Const.Locator.OperationModeVm.IsSeperate)
                {
                    signRequest.OperationMode = 2;
                }
            }
            else
            {
                signRequest.OperationMode = 0;
            }


            signRequest.Items = new List<SignGoodItem>();
            SignGoodItem signGoodItem;
            int id = 1;


            //查询对应的商品
            var items = from a in Const.dB.InvoiceItems
                        where a.SalesorderNum == SelectedItem.SalesOrderNum
                        select a;
            if (items != null)
            {
                List<InvoiceItems> invoiceItems = items.ToList();
                foreach (var item in invoiceItems)
                {
                    signGoodItem = new SignGoodItem();
                    signGoodItem.GTIN = id;
                    signGoodItem.BarCode = item.GoodsGin.ToString();
                    id++;
                    
                    signGoodItem.Name = item.GoodsDesc;
                    signGoodItem.Quantity = -item.GoodsQty.Value;
                    signGoodItem.UnitPrice = item.GoodsPrice.Value;
                    signGoodItem.TotalAmount = -item.TotalAmount.Value;
                    signGoodItem.IsTaxInclusive = true;
                    signGoodItem.Labels = new string[1] { item.TaxItem };
                    signRequest.Items.Add(signGoodItem);
                }
            }
            switch (Const.Locator.ParameterSetting.CommModel)
            {
                case CommModel.NetPort:
                    SignRequest(signRequest);
                    break;
                case CommModel.SerialPort:
                    SignRequestSerial(signRequest);
                    break;
                default:
                    break;
            }

        }
        public void SignRequest(SignRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException("SignRequest data can not be null.");
            }
            _signTcpClient = new TcpHelper();
            if (string.IsNullOrWhiteSpace(Const.Locator.ParameterSetting.SdcUrl))
            {
                MessageBoxEx.Show("EFD URL can not be null.", MessageBoxButton.OK);
                return;
            }
            string[] sdc = Const.Locator.ParameterSetting.SdcUrl.Split(':');
            if (sdc != null && sdc.Count() != 2)
            {
                MessageBoxEx.Show("EFD URL is not in the right format.", MessageBoxButton.OK);
                return;
            }
            bool isConn = _signTcpClient.Connect(IPAddress.Parse(sdc[0]), int.Parse(sdc[1]));
            if (!isConn)
            {
                MessageBoxEx.Show("Failed to connect to EFD.", MessageBoxButton.OK);
                return;
            }
            string requestString = JsonConvert.SerializeObject(request);
            _signTcpClient.Complated -= _signTcpClient_Complated;
            _signTcpClient.Complated += _signTcpClient_Complated;
            _signTcpClient.Send(0x02, requestString);
            _signTcpClient.ReciveAsync();
        }

        public void SignRequestSerial(SignRequest request)
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
            string requestString = JsonConvert.SerializeObject(request);

            SerialClient _client = new SerialClient(Const.Locator.ParameterSetting.SelectedPort,
               int.Parse(Const.Locator.ParameterSetting.SelectedBaudRate),
               (Parity)Enum.Parse(typeof(Parity), Const.Locator.ParameterSetting.SelectedParity),
               int.Parse(Const.Locator.ParameterSetting.SelectedDataBits),
               (StopBits)Enum.Parse(typeof(StopBits), Const.Locator.ParameterSetting.SelectedStopBits));
            _client.Open();
            _client.Complated -= _signTcpClient_Complated;
            _client.Complated += _signTcpClient_Complated;
            _client.Send(0x02, requestString);
        }
        private void _signTcpClient_Complated(object sender, MessageModel e)
        {
            try
            {
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
                if (e.MessageId != 0x02)
                {
                    return;
                }
                signResponse = JsonConvert.DeserializeObject<SignResponse>(e.Message);
                if (signResponse != null)
                {
                    //更新退款状态
                    if (SelectedItem != null)
                    {
                        SelectedItem.CreditFlag = "Credit";
                        var invoice = (from a in Const.dB.InvoiceAbbreviation
                                       where a.SalesorderNum == SelectedItem.SalesOrderNum
                                       select a).ToList();
                        if (invoice != null && invoice.Count > 0)
                        {
                            invoice[0].CreditFlag = "1";
                        }
                        Const.dB.Update<InvoiceAbbreviation>(invoice[0]);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBoxEx.Show(ex.Message);
            }
        }
        #endregion
    }
}
