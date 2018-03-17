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
using System.IO;
using System.Drawing;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using GalaSoft.MvvmLight.Messaging;

namespace Inspur.Billing.ViewModel.Issue
{
    public class PrintViewModel : ViewModelBase
    {
        #region 字段
        SignRequest signRequest;
        SignResponse signResponse;
        /// <summary>
        /// 58pos -  32字符，80-
        /// </summary>
        const int PrintCharCount = 47;
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
        /// <summary>
        /// 获取或设置
        /// </summary>
        private ImageSource _qrPath;
        /// <summary>
        /// 获取或设置
        /// </summary>
        public ImageSource QrPath
        {
            get { return _qrPath; }
            set { Set<ImageSource>(ref _qrPath, value, "QrPath"); }
        }



        /// <summary>
        /// 获取或设置是否可以签名，true签名打印sdc格式，false打印自定义
        /// </summary>
        private bool _isCanSign;
        /// <summary>
        /// 获取或设置是否可以签名，true签名打印sdc格式，false打印自定义
        /// </summary>
        public bool IsCanSign
        {
            get { return _isCanSign; }
            set { Set<bool>(ref _isCanSign, value, "IsCanSign"); }
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
                    try
                    {
                        switch (p)
                        {
                            case "Loaded":

                                //if (IsCanSign)
                                //{
                                try
                                {
                                    signResponse = Sign();
                                }
                                catch
                                {
                                    MessageBoxEx.Show("Can not connect E-SDC,We will custom print.");
                                    signResponse = null;
                                    GetTaxPayerInfo();
                                }
                                if (signResponse != null)
                                {
                                    if (!string.IsNullOrWhiteSpace(signResponse.Message))
                                    {
                                        foreach (var item in signResponse.ModelState.Values)
                                        {
                                            if (item.Contains("1500"))
                                            {
                                                //校验pin
                                                PinView pinView = new PinView();
                                                if (pinView.ShowDialog() == true)
                                                {
                                                    Command.Execute("Loaded");
                                                }

                                                //pinView.ShowDialog();
                                                return;
                                            }
                                        }



                                        MessageBoxEx.Show(signResponse.Message);
                                        SignFilureData();
                                    }
                                    else
                                    {
                                        //处理税款数据
                                        SignSuccessData();
                                    }
                                }
                                else
                                {
                                    SignFilureData();
                                }
                                //}
                                //else
                                //{
                                //    SignFilureData();
                                //}
                                break;
                            case "Unloaded":
                                QrPath = null;
                                break;
                            default:
                                break;
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

        private void SignSuccessData()
        {
            SignSuccessDataTaxItems();
            SignSuccessDataTaxPayerInfo();
            //处理二维码
            SignSuccessDataQr();
        }

        private void SignSuccessDataQr()
        {
            if (!Credit.IsMitQr && !string.IsNullOrWhiteSpace(signResponse.VerificationQRCode))
            {
                byte[] qrArr = Convert.FromBase64String(signResponse.VerificationQRCode);
                using (MemoryStream ms = new MemoryStream(qrArr))
                {
                    //先保存，在缩放,保存
                    //Bitmap bmp = new Bitmap(ms);
                    ////bmp.Save(AppDomain.CurrentDomain.BaseDirectory + "qr.bmp", System.Drawing.Imaging.ImageFormat.Bmp);
                    ////保存单色图
                    //Bitmap bitmap = bmp.Clone(new Rectangle(0, 0, bmp.Width, bmp.Height), System.Drawing.Imaging.PixelFormat.Format1bppIndexed);

                    ////缩放图片
                    //Bitmap zoombmp = new Bitmap(QrWidth, QrWidth);
                    //Graphics g = Graphics.FromImage(zoombmp);
                    //// 插值算法的质量
                    //g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    //g.DrawImage(bitmap, new Rectangle(0, 0, QrWidth, QrWidth), new Rectangle(0, 0, bitmap.Width, bitmap.Height), GraphicsUnit.Pixel);
                    //g.Dispose();

                    ////Bitmap bitmap = b.Clone(new Rectangle(0, 0, b.Width, b.Height), System.Drawing.Imaging.PixelFormat.Format1bppIndexed);
                    ////保存单色图
                    //Bitmap bitmap2 = bmp.Clone(new Rectangle(0, 0, zoombmp.Width, zoombmp.Height), System.Drawing.Imaging.PixelFormat.Format1bppIndexed);
                    //bitmap2.Save(AppDomain.CurrentDomain.BaseDirectory + "qr.bmp", System.Drawing.Imaging.ImageFormat.Bmp);

                    ////先缩放，在保存
                    //Bitmap bmp = new Bitmap(ms);
                    //int qrWidth = (int)(bmp.Width * Config.QrMagnification);
                    ////缩放图片
                    //Bitmap zoombmp = new Bitmap(qrWidth, qrWidth);
                    //Graphics g = Graphics.FromImage(zoombmp);
                    //// 插值算法的质量
                    //g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    //g.DrawImage(bmp, new Rectangle(0, 0, qrWidth, qrWidth), new Rectangle(0, 0, bmp.Width, bmp.Height), GraphicsUnit.Pixel);
                    //g.Dispose();
                    ////保存单色图
                    //Bitmap bitmap = zoombmp.Clone(new Rectangle(0, 0, zoombmp.Width, zoombmp.Height), System.Drawing.Imaging.PixelFormat.Format1bppIndexed);
                    //bitmap.Save(Const.QrPath, System.Drawing.Imaging.ImageFormat.Bmp);

                    Bitmap bitmap = new Bitmap(ms);
                    bitmap.Save(Const.QrPath, System.Drawing.Imaging.ImageFormat.Bmp);

                    //防止图片占用
                    QrPath = InitImage(Const.QrPath);
                }
            }
        }

        private void SignSuccessDataTaxPayerInfo()
        {
            TaxPayerInfo.Tin = signResponse.TIN;
            TaxPayerInfo.Name = signResponse.BusinessName;
            TaxPayerInfo.Address = signResponse.Address;
        }

        private void SignSuccessDataTaxItems()
        {
            if (signResponse.TaxItems != null)
            {
                List<InvoiceTax> items = new List<InvoiceTax>();
                InvoiceTax invoiceTax = null;
                foreach (var taxItem in signResponse.TaxItems)
                {
                    invoiceTax = new InvoiceTax
                    {
                        TaxItemCode = taxItem.Label,
                        TaxItemDesc = taxItem.CategoryName,
                        TaxRate = taxItem.Rate,
                        TaxAmount = taxItem.Amount,
                    };
                    items.Add(invoiceTax);
                }
                TaxList = items;
                if (TaxList != null && TaxList.Count > 0)
                {
                    TotalTaxAmount = Math.Round(TaxList.Sum(a => a.TaxAmount), 2);
                };
            }
        }

        private void GetTaxPayerInfo()
        {
            var taxPayer = (from a in Const.dB.TaxpayerJnfo
                            select a).FirstOrDefault();
            if (taxPayer != null)
            {
                TaxPayerInfo = EntityAdapter.TaxpayerJnfo2TaxPayer(taxPayer);
            }
        }

        /// <summary>
        /// 签名失败处理数据
        /// </summary>
        private void SignFilureData()
        {
            //签名失败，客户端处理税款明细
            if (Credit != null && Credit.Productes != null)
            {
                TaxList = Credit.Productes.GroupBy(a => a.TaxType.Id).Select(g => new InvoiceTax
                {
                    TaxItemCode = g.First().TaxType.Label,
                    TaxItemDesc = g.First().TaxType.Name,
                    TaxRate = g.First().TaxType.Rate,

                    TaxAmount = g.Sum(b =>
                    {
                        return TaxCalculation.Calculation(b.TaxInclusive, b.TaxType.CalculationMode, b.Price, b.Count, b.TaxType.Rate, b.TaxType.FixTaxAmount);
                    })
                }).ToList();
                if (TaxList != null && TaxList.Count > 0)
                {
                    TotalTaxAmount = Math.Round(TaxList.Sum(a => a.TaxAmount), 2);
                }
            }
        }

        /// <summary>
        /// 解决不同进程读取同一张图片的问题
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        private BitmapImage InitImage(string filePath)
        {
            BitmapImage bitmapImage;
            using (BinaryReader reader = new BinaryReader(File.Open(filePath, FileMode.Open)))
            {
                FileInfo fi = new FileInfo(filePath);
                byte[] bytes = reader.ReadBytes((int)fi.Length);
                reader.Close();

                //image = new Image();
                bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = new MemoryStream(bytes);
                bitmapImage.EndInit();
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                //image.Source = bitmapImage;
                reader.Dispose();
            }
            return bitmapImage;
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
                        //打印
                        Print();
                        Const.Locator.Main.Message = "Print Success";
                        //保存
                        Save(signRequest, signResponse);
                        //关闭校验窗口
                        Messenger.Default.Send<string>(null, "ClosePrintView");
                        //清空数据
                        ActualPay = 0;
                    }
                    catch (Exception ex)
                    {
                        MessageBoxEx.Show(ex.Message);
                    }
                }, () =>
               {
                   return true;
               }));
            }
        }
        private SignResponse Sign()
        {
            AttentionResponse attentionResponse = ServiceHelper.AttentionRequest();
            if (attentionResponse.ATT_GSC == "0000")
            {
                signRequest = new SignRequest
                {
                    DateAndTimeOfIssue = ServiceHelper.CurrentTime,
                    IT = "Normal",
                    TT = "Sale",
                    PaymentType = Credit.SelectedPaymentType.Name,
                    Cashier = Credit.Cashier,
                    BD = Credit.Buyer.Tin
                };
                //signRequest.BuyerCostCenterId = "1234567890";
                //signRequest.InvoiceNumber = "PNQAVDNX-PNQAVDNX-1";
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
                    signRequest.Items.Add(signGoodItem);
                }

                return ServiceHelper.SignRequest(signRequest);
            }
            else
            {
                MessageBoxEx.Show("E-SDC is not available.ATT_GSC=" + attentionResponse.ATT_GSC);
                return null;
            }
        }
        private void Save(SignRequest request, SignResponse signResponse)
        {
            //保存销售订单主表
            InvoiceAbbreviation invoiceAbbreviation = new InvoiceAbbreviation
            {
                CashierId = Const.CashierId,
                TaxpayerTin = TaxPayerInfo.Tin,
                TotalTaxAmount = TotalTaxAmount,
                TenderAmount = ActualPay,
                Change = Change,
                QrcodePath = "",
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
            if (request != null)
            {
                invoiceAbbreviation.HashCode = request.Hash;
            }
            ///sdc取值
            if (signResponse != null)
            {
                invoiceAbbreviation.TaxpayerName = signResponse.BusinessName;//sdc取值
                invoiceAbbreviation.TaxpayerLocation = signResponse.LocationName;//sdc取值
                invoiceAbbreviation.TaxpayerAddress = signResponse.Address;//sdc取值
                invoiceAbbreviation.TaxpayerDistrit = signResponse.District;//sdc取值
                invoiceAbbreviation.InvoiceNumber = signResponse.IN;//sdc取值
                invoiceAbbreviation.IssueDate = request.DateAndTimeOfIssue;//sdc取值
                invoiceAbbreviation.VerificationUrl = signResponse.VerificationUrl;
            }
            Const.dB.Insert<InvoiceAbbreviation>(invoiceAbbreviation);
            //保存订单销售子表
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
                    invoiceItem.TaxAmount = TaxCalculation.Calculation(item.TaxInclusive, item.TaxType.CalculationMode, item.Price, item.Count, item.TaxType.Rate, item.TaxType.FixTaxAmount);

                    Const.dB.Insert<InvoiceItems>(invoiceItem);
                }
            }
        }

        private void Print()
        {
            //_isHasPrint = true;
            Printer.Instance.Print(() =>
            {
                if (signResponse != null && string.IsNullOrWhiteSpace(signResponse.Message) && !string.IsNullOrWhiteSpace(signResponse.Journal))
                {
                    //打印返回的表
                    Printer.Instance.SetAlign(1);
                    Printer.Instance.PrintString(0, 1, 0, 0, 0, string.Format("{0}\r\n", signResponse.Journal));

                    Printer.Instance.Reset();
                    Printer.Instance.SetAlign(1);
                    if (!Credit.IsMitQr && !string.IsNullOrWhiteSpace(signResponse.VerificationUrl))
                    {
                        Printer.Instance.PrintTwoDimensionalBarcodeA(signResponse.VerificationUrl);
                    }
                    Printer.Instance.CutPaper(1, 5);
                }
                else
                {
                    //打印自定义的表样
                    Printer.Instance.SetAlign(1);
                    Printer.Instance.PrintString(0, 1, 0, 0, 0, string.Format("Order Number:{0}\r\n{1}\r\n", Credit.OrderNumber, CurrentTime));

                    Printer.Instance.SetAlign(0);
                    SetTwoColumnPrint("POSID", Credit.PosNumber, "Cashier:", Credit.Cashier);
                    SetTwoColumnPrint("Buyer TIN", "", "", Credit.Buyer.Tin);
                    SetTwoColumnPrint("Buyer Name", "", "", Credit.Buyer.Name);
                    SetTwoColumnPrint("Buyer Address", "", "", Credit.Buyer.Address);
                    SetTwoColumnPrint("Buyer Tel", "", "", Credit.Buyer.TelPhone);
                    PrintLine();

                    Printer.Instance.SetAlign(1);
                    Printer.Instance.PrintString(0, 1, 0, 0, 0, "Particular Of Items\r\n");
                    //表格字符占用按照7 8 5 3 9来打印
                    //表格字符占用按照11 11 5 3 9来打印
                    Printer.Instance.SetAlign(0);
                    Printer.Instance.PrintString(0, 1, 0, 0, 0, "Name           Price          Qty.       Amount\r\n");
                    if (Credit != null && Credit.Productes != null)
                    {
                        foreach (var item in Credit.Productes)
                        {
                            Printer.Instance.PrintString(0, 1, 0, 0, 0, string.Format("{0}{1}{2}{3}\r\n",
                                SetLeftPrint(15, string.Format("{0} ({1})", item.Name, item.TaxType.Label.ToString())),
                                SetLeftPrint(12, item.Price.ToString("0.00")),
                                SetCenterPrint(8, item.Count.ToString()),
                                SetRightPrint(12, item.Amount.ToString("0.00"))));
                        }
                    }
                    //SetTwoColumnPrint("Total Value", "", "", Credit.GrandTotal.ToString("0.00"));
                    PrintLine();

                    Printer.Instance.SetAlign(1);
                    Printer.Instance.PrintString(0, 1, 0, 0, 0, "Tax Amount\r\n");
                    //表格字符占用按照8 8 8 8来打印
                    Printer.Instance.SetAlign(0);
                    Printer.Instance.PrintString(0, 1, 0, 0, 0, "Label           Name      Rate(%)    Tax Amount\r\n");
                    if (TaxList != null)
                    {
                        foreach (var item in TaxList)
                        {
                            Printer.Instance.PrintString(0, 1, 0, 0, 0, string.Format("{0}{1}{2}{3}\r\n",
                                SetLeftPrint(12, item.TaxItemCode),
                                SetCenterPrint(12, item.TaxItemDesc),
                                SetCenterPrint(11, (item.TaxRate).ToString()),
                                SetRightPrint(12, item.TaxAmount.ToString("0.00"))));
                        }
                    }
                    SetTwoColumnPrint("Total Tax", "", "", TotalTaxAmount.ToString("0.00"));
                    PrintLine();

                    SetTwoColumnPrint("Total Amount", "", "", Credit.GrandTotal.ToString("0.00"));
                    SetTwoColumnPrint("Payment Mode", "", "", Credit == null ? "" : (Credit.SelectedPaymentType == null ? "" : Credit.SelectedPaymentType.Name));
                    SetTwoColumnPrint("Actual Payment", "", "", ActualPay.ToString("0.00"));
                    SetTwoColumnPrint("Change", "", "", Change.ToString("0.00"));
                    PrintLine();

                    SetTwoColumnPrint("TIN", "", "", TaxPayerInfo.Tin);
                    SetTwoColumnPrint("Name", "", "", TaxPayerInfo.Name);
                    SetTwoColumnPrint("Address", "", "", TaxPayerInfo.Address);
                    SetTwoColumnPrint("Tel", "", "", TaxPayerInfo.Telphone);
                    PrintLine();

                    //Printer.Instance.PrintTwoDimensionalBarcodeA(signResponse.VerificationUrl);

                    Printer.Instance.PrintString(0, 0, 0, 0, 0, "Dear sir madam,please keep the invoice properly so as to refunds & replaces \r\n\r\n");
                    Printer.Instance.SetAlign(1);
                    Printer.Instance.PrintString(0, 0, 0, 0, 0, "Thank You & Please Come Again \r\n");
                    Printer.Instance.CutPaper(1, 3);
                }
            });
        }

        public void PrintLine()
        {
            Printer.Instance.PrintString(0, 0, 0, 0, 0, "————————————————————————\r\n");
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
            if (left.Length + right.Length < PrintCharCount)
            {
                sb.Append(' ', PrintCharCount - left.Length - right.Length);
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
