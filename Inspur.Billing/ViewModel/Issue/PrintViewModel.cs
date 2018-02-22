using DataModels;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using Inspur.Billing.Commom;
using Inspur.TaxModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using LinqToDB;
using ControlLib.Controls.Dialogs;
using Inspur.Billing.Model.Service.Sign;
using Inspur.Billing.Model.Service.Attention;
using Inspur.Billing.View.Setting;

namespace Inspur.Billing.ViewModel.Issue
{
    public class PrintViewModel : ViewModelBase
    {
        #region 字段
        private bool _isHasPrint = false;
        #endregion

        #region 属性
        /// <summary>
        /// 获取或设置
        /// </summary>
        private CreditViewModel _credit;
        /// <summary>
        /// 获取或设置
        /// </summary>
        public CreditViewModel Credit
        {
            get { return _credit; }
            set { Set<CreditViewModel>(ref _credit, value, "Credit"); }
        }
        /// <summary>
        /// 获取或设置当前时间
        /// </summary>
        private string _currentTime = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
        /// <summary>
        /// 获取或设置当前时间
        /// </summary>
        public string CurrentTime
        {
            get { return _currentTime; }
            set { Set<string>(ref _currentTime, value, "CurrentTime"); }
        }
        /// <summary>
        /// 获取或设置实际支付金额
        /// </summary>
        private double _actualPay;
        /// <summary>
        /// 获取或设置实际支付金额
        /// </summary>
        public double ActualPay
        {
            get { return _actualPay; }
            set
            {
                if (value != _actualPay)
                {
                    _actualPay = value;
                    if (Credit != null)
                    {
                        double chang = value - Credit.GrandTotal;
                        if (chang >= 0)
                        {
                            Change = chang;
                        }
                        else
                        {
                            Change = 0;
                        }
                    }
                    RaisePropertyChanged(() => this.ActualPay);
                }
            }
        }

        /// <summary>
        /// 获取或设置找零
        /// </summary>
        private double _change;
        /// <summary>
        /// 获取或设置找零
        /// </summary>
        public double Change
        {
            get { return _change; }
            set { Set<double>(ref _change, value, "Change"); }
        }
        /// <summary>
        /// 获取或设置
        /// </summary>
        private TaxPayer _taxPayerInfo = new TaxPayer();
        /// <summary>
        /// 获取或设置
        /// </summary>
        public TaxPayer TaxPayerInfo
        {
            get { return _taxPayerInfo; }
            set { Set<TaxPayer>(ref _taxPayerInfo, value, "TaxPayerInfo"); }
        }


        /// <summary>
        /// 获取或设置税款详细列表
        /// </summary>
        private List<InvoiceTax> _taxList;
        /// <summary>
        /// 获取或设置税款详细列表
        /// </summary>
        public List<InvoiceTax> TaxList
        {
            get { return _taxList; }
            set
            {
                if (value != _taxList)
                {
                    _taxList = value;
                    RaisePropertyChanged(() => this.TaxList);
                }
            }
        }
        /// <summary>
        /// 获取或设置税款合计
        /// </summary>
        private double _totalTaxAmount;
        /// <summary>
        /// 获取或设置税款合计
        /// </summary>
        public double TotalTaxAmount
        {
            get { return _totalTaxAmount; }
            set
            {
                if (value != _totalTaxAmount)
                {
                    _totalTaxAmount = value;
                    RaisePropertyChanged(() => this.TotalTaxAmount);
                }
            }
        }

        #endregion

        #region 命令

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
                            _isHasPrint = false;
                            var taxPayer = (from a in Const.dB.TaxpayerJnfo
                                            select a).FirstOrDefault();
                            if (taxPayer != null)
                            {
                                TaxPayerInfo = EntityAdapter.TaxpayerJnfo2TaxPayer(taxPayer);
                            }

                            if (Credit != null && Credit.Productes != null)
                            {
                                TaxList = Credit.Productes.GroupBy(a => a.TaxType.Id).Select(g => new InvoiceTax
                                {
                                    TaxItemCode = g.First().TaxType.Label,
                                    TaxItemDesc = g.First().TaxType.Name,
                                    TaxRate = g.First().TaxType.Rate,
                                    TaxAmount = g.Sum(b =>
                                    {
                                        if (b.TaxType.CalculationMode == "1")
                                        {
                                            return b.Count * b.TaxType.FixTaxAmount;
                                        }
                                        else
                                        {
                                            return b.Count * b.Price * b.TaxType.Rate;
                                        }
                                    })
                                }).ToList();
                                if (TaxList != null && TaxList.Count > 0)
                                {
                                    TotalTaxAmount = TaxList.Sum(a => a.TaxAmount);
                                }
                            }
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

        /// <summary>
        /// 获取或设置
        /// </summary>
        private ICommand _printCommand;
        /// <summary>
        /// 获取或设置
        /// </summary>
        public ICommand PrintCommand
        {
            get
            {
                return _printCommand ?? (_printCommand = new RelayCommand(() =>
                {
                    if (Credit == null)
                    {
                        return;
                    }
                    try
                    {

                        AttentionResponse attentionResponse = ServiceHelper.AttentionRequest();
                        if (attentionResponse.ATT_GSC == "0000")
                        {
                            SignRequest signRequest = new SignRequest
                            {
                                DateAndTimeOfIssue = ServiceHelper.CurrentTime,
                                IT = "0",
                                TT = "0",
                                PaymentType = Credit.SelectedPaymentType.Code,
                                Cashier = Const.CashierId.ToString(),
                                BD = Credit.Buyer.Tin
                            };
                            signRequest.Options = new Dictionary<string, string>();
                            if (Credit.IsMitQr)
                            {
                                signRequest.Options.Add("OmitQRCodeGen", "1");
                            }
                            else
                            {
                                signRequest.Options.Add("OmitQRCodeGen", "0");
                            }
                            if (Credit.IsMitTexTual)
                            {
                                signRequest.Options.Add("OmitTextualRepresentation", "1");
                            }
                            else
                            {
                                signRequest.Options.Add("OmitTextualRepresentation", "0");
                            }
                            signRequest.Items = new List<SignGoodItem>();
                            SignGoodItem signGoodItem;
                            foreach (var item in Credit.Productes)
                            {
                                signGoodItem = new SignGoodItem();
                                signGoodItem.GTIN = item.BarCode;
                                signGoodItem.Name = item.Name;
                                signGoodItem.Quantity = item.Count;
                                signGoodItem.UnitPrice = item.Price;
                                signGoodItem.TotalAmount = item.Amount;
                                signGoodItem.Labels = new string[1] { item.TaxType.Label };
                            }

                            SignResponse signResponse = ServiceHelper.SignRequest(signRequest);
                            if (!string.IsNullOrEmpty(signResponse.Message))
                            {
                                foreach (var item in signResponse.ModelState.Values)
                                {
                                    if (item.Contains("1500"))
                                    {
                                        //校验pin
                                        PinView pinView = new PinView();
                                        pinView.ShowDialog();
                                        return;
                                    }
                                }
                                MessageBoxEx.Show("E-SDC is not available.ATT_GSC=" + attentionResponse.ATT_GSC);
                            }
                        }
                        else
                        {
                            MessageBoxEx.Show("E-SDC is not available.ATT_GSC=" + attentionResponse.ATT_GSC);
                        }


























                        //Print();
                        //保存数据
                        //Save();
                    }
                    catch (Exception ex)
                    {
                        MessageBoxEx.Show(ex.Message);
                    }
                }, () =>
               {
                   return !_isHasPrint;
               }));
            }
        }

        private void Save(SignResponse signResponse)
        {
            //保存销售订单主表
            InvoiceAbbreviation invoiceAbbreviation = new InvoiceAbbreviation
            {
                CashierId = Const.CashierId,
                TaxpayerTin = TaxPayerInfo.Tin,
                TaxpayerName = TaxPayerInfo.Name,//sdc取值
                TaxpayerLocation = "",//sdc取值
                TaxpayerAddress = TaxPayerInfo.Address,//sdc取值
                TaxpayerDistrit = "",//sdc取值
                InvoiceNumber = "",//sdc取值
                TotalTaxAmount = TotalTaxAmount,
                IssueDate = DateTime.Now,//sdc取值
                TenderAmount = ActualPay,
                Change = Change,

                VerificationUrl = "",
                QrcodePath = "",
                HashCode = ""
            };
            if (Credit != null)
            {
                invoiceAbbreviation.SalesorderNum = Credit.OrderNumber;
                if (Credit.SelectedPaymentType != null)
                {
                    invoiceAbbreviation.PaymentType = Credit.SelectedPaymentType.Code;
                }
                invoiceAbbreviation.BuyerTin = Credit.Buyer.Tin;
                invoiceAbbreviation.BuyerName = Credit.Buyer.Name;
                invoiceAbbreviation.TotalAmount = Credit.GrandTotal;
            }
            Const.dB.Insert<InvoiceAbbreviation>(invoiceAbbreviation);
            //保存订单销售字表
            InvoiceItems invoiceItem = null;
            if (Credit != null && Credit.Productes != null)
            {
                foreach (var item in Credit.Productes)
                {
                    invoiceItem = new InvoiceItems();
                    invoiceItem.Sn = null;

                    invoiceItem.TaxtypeId = item.TaxType.Id;
                    invoiceItem.SalesorderNum = Credit.OrderNumber;
                    invoiceItem.GoodsId = item.No;
                    invoiceItem.GoodsGin = item.BarCode;
                    invoiceItem.GoodsDesc = item.Name;
                    invoiceItem.GoodsPrice = item.Price;
                    invoiceItem.GoodsQty = item.Count;
                    invoiceItem.TotalAmount = item.Price * item.Count;

                    invoiceItem.TaxtypeId = item.TaxType.Id;
                    invoiceItem.TaxItem = item.TaxType.Name;
                    invoiceItem.TaxRate = item.TaxType.Rate;
                    invoiceItem.TaxAmount = TaxCalculation.Calculation(item.TaxType.CalculationMode, item.Price, item.Count, item.TaxType.Rate, item.TaxType.FixTaxAmount);

                    Const.dB.Insert<InvoiceItems>(invoiceItem);
                }
            }
        }

        private void Print()
        {
            _isHasPrint = true;
            Printer.Instance.Print(() =>
            {
                Printer.Instance.SetAlign(1);
                Printer.Instance.PrintString(0, 1, 0, 0, 0, string.Format("Order Number:{0}\r\n{1}\r\n", Credit.OrderNumber, CurrentTime));

                Printer.Instance.SetAlign(0);
                SetTwoColumnPrint("POSID", Credit.PosNumber, "Cashier:", Credit.Cashier);
                SetTwoColumnPrint("Buyer TIN", "", "", Credit.Buyer.Tin);
                SetTwoColumnPrint("Buyer Name", "", "", Credit.Buyer.Name);
                SetTwoColumnPrint("Buyer Address", "", "", Credit.Buyer.Address);
                SetTwoColumnPrint("Buyer Contact", "", "", Credit.Buyer.TelPhone);
                Printer.Instance.PrintString(0, 0, 0, 0, 0, "————————————————\r\n");

                Printer.Instance.SetAlign(1);
                Printer.Instance.PrintString(0, 1, 0, 0, 0, "Particular Of Items\r\n");
                //表格字符占用按照7 8 5 3 9来打印
                Printer.Instance.SetAlign(0);
                Printer.Instance.PrintString(0, 1, 0, 0, 0, "  GIN  Item Name Price Qty Value\r\n");
                if (Credit != null && Credit.Productes != null)
                {
                    foreach (var item in Credit.Productes)
                    {
                        Printer.Instance.PrintString(0, 1, 0, 0, 0, string.Format("{0}{1}{2}{3}{4}\r\n",
                            SetCenterPrint(7, item.BarCode),
                            SetCenterPrint(9, item.Name),
                            SetRightPrint(6, item.Price.ToString("0.00")),
                            SetRightPrint(3, item.Count.ToString()),
                            SetRightPrint(7, item.Amount.ToString("0.00"))));
                    }
                }
                SetTwoColumnPrint("Total Value", "", "", Credit.GrandTotal.ToString("0.00"));
                Printer.Instance.PrintString(0, 0, 0, 0, 0, "————————————————\r\n");

                Printer.Instance.SetAlign(1);
                Printer.Instance.PrintString(0, 1, 0, 0, 0, "Tax Amount\r\n");
                //表格字符占用按照8 8 8 8来打印
                Printer.Instance.SetAlign(0);
                Printer.Instance.PrintString(0, 1, 0, 0, 0, "Label  Name   Rate(%) Tax Amount\r\n");
                if (TaxList != null)
                {
                    foreach (var item in TaxList)
                    {
                        Printer.Instance.PrintString(0, 1, 0, 0, 0, string.Format("{0}{1}{2}{3}\r\n",
                            SetCenterPrint(5, item.TaxItemCode),
                            SetCenterPrint(8, item.TaxItemDesc),
                            SetCenterPrint(9, (item.TaxRate * 100).ToString()),
                            SetRightPrint(10, item.TaxAmount.ToString("0.00"))));
                    }
                }
                SetTwoColumnPrint("Total Tax Amount", "", "", Credit.GrandTotal.ToString("0.00"));
                Printer.Instance.PrintString(0, 0, 0, 0, 0, "————————————————\r\n");

                SetTwoColumnPrint("Grand Total", "", "", Credit.GrandTotal.ToString("0.00"));
                SetTwoColumnPrint("Payment Model", "", "", Credit == null ? "" : (Credit.SelectedPaymentType == null ? "" : Credit.SelectedPaymentType.Name));
                SetTwoColumnPrint("Actual Payment", "", "", ActualPay.ToString("0.00"));
                SetTwoColumnPrint("Change", "", "", Change.ToString("0.00"));
                Printer.Instance.PrintString(0, 0, 0, 0, 0, "————————————————\r\n");

                SetTwoColumnPrint("TIN", "", "", TaxPayerInfo.Tin);
                SetTwoColumnPrint("Name", "", "", TaxPayerInfo.Name);
                SetTwoColumnPrint("Address", "", "", TaxPayerInfo.Address);
                SetTwoColumnPrint("Contact", "", "", TaxPayerInfo.Telphone);
                Printer.Instance.PrintString(0, 0, 0, 0, 0, "————————————————\r\n");

                Printer.Instance.PrintString(0, 0, 0, 0, 0, "Dear sir madam,please keep the invoice properly so as to refunds & replaces \r\n\r\n");
                Printer.Instance.SetAlign(1);
                Printer.Instance.PrintString(0, 0, 0, 0, 0, "Thank You & Please Come Again \r\n");

                Printer.Instance.CutPaper(1, 3);
            });
        }

        /// <summary>
        /// 目前在api中没有找到同一行两列的打印方式，先使用此方法
        /// </summary>
        /// <param name="leftName"></param>
        /// <param name="leftValue"></param>
        /// <param name="rightName"></param>
        /// <param name="rightValue"></param>
        private void SetTwoColumnPrint(string leftName, string leftValue, string rightName, string rightValue)
        {
            string left = string.Format("{0}:{1}", leftName, leftValue);
            string right = string.Format("{0}{1}", rightName, rightValue);
            StringBuilder sb = new StringBuilder();
            if (left.Length + right.Length < 32)
            {
                sb.Append(' ', 32 - left.Length - right.Length);
            }
            Printer.Instance.PrintString(0, 1, 0, 0, 0, string.Format("{0}{1}{2}\r\n", left, sb.ToString(), right));
        }

        private string SetLeftPrint(int totalLength, string content)
        {
            StringBuilder result = new StringBuilder();
            int spaceCount = totalLength - content.Length;
            if (spaceCount > 0)
            {
                result.Append(content);
                result.Append(' ', spaceCount);
                return result.ToString();
            }
            return content;
        }
        private string SetRightPrint(int totalLength, string content)
        {
            StringBuilder result = new StringBuilder();
            int spaceCount = totalLength - content.Length;
            if (spaceCount > 0)
            {
                result.Append(' ', spaceCount);
                result.Append(content);
                return result.ToString();
            }
            return content;
        }
        private string SetCenterPrint(int totalLength, string content)
        {
            StringBuilder result = new StringBuilder();
            int spaceCount = totalLength - content.Length;
            int marginSpace = (int)(Math.Floor(spaceCount / 2.0));
            int mode = spaceCount % 2;
            if (spaceCount > 0)
            {
                if (marginSpace > 0)
                {
                    if (mode > 0)
                    {
                        result.Append(' ', marginSpace + 1);
                        result.Append(content);
                        result.Append(' ', marginSpace);
                    }
                    else
                    {
                        result.Append(' ', marginSpace);
                        result.Append(content);
                        result.Append(' ', marginSpace);
                    }
                }
                else
                {
                    result.Append(content);
                    result.Append(" ");
                }
            }
            else
            {
                result.Append(content);
            }
            return result.ToString();
        }
        #endregion
    }
}
