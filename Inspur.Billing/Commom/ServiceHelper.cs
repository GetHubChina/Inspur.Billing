using CommonLib.Net;
using ControlLib.Controls.Dialogs;
using DataModels;
using Inspur.Billing.Model.Service.Attention;
using Inspur.Billing.Model.Service.Pin;
using Inspur.Billing.Model.Service.Status;
using Inspur.Billing.View.Setting;
using JumpKick.HttpLib;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using LinqToDB;
using Inspur.Billing.Model.Service.Sign;
using System.Security.Cryptography;
using Inspur.Billing.Model.Service.LastSign;
using System.Net.Sockets;

namespace Inspur.Billing.Commom
{
    class ServiceHelper
    {
        public static string CurrentTime = DateTime.UtcNow.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ssZ");
        /// <summary>
        /// socket连接对象
        /// </summary>
        public static TcpHelper TcpClient = new TcpHelper();

        //public static StatusResponse StatueRequest()
        //{
        //    StatusRequest statusRequest = new StatusRequest() { GS = "GetStatus" };
        //    string requestString = JsonConvert.SerializeObject(statusRequest);

        //    HttpHelper httpHelper = new HttpHelper();
        //    HttpItem httpItem = new HttpItem();
        //    httpItem.Method = "POST";
        //    httpItem.URL = Const.GetStatusUri;
        //    httpItem.Postdata = Convert.ToString(requestString);


        //    httpItem.ResultType = ResultType.String;
        //    HttpResult html = httpHelper.GetHtml(httpItem);
        //    if (html.StatusCode != HttpStatusCode.OK)
        //    {
        //        throw new Exception(string.IsNullOrEmpty(html.Html) ? (string.IsNullOrEmpty(html.StatusDescription) ? "Post Data Error!" : html.StatusDescription) : html.Html);
        //    }
        //    return JsonConvert.DeserializeObject<StatusResponse>(html.Html);
        //}

        public static void StatueRequest()
        {
            StatusRequest statusRequest = new StatusRequest() { PosSerialNumber = Config.PosSerialNumber, PosVendor = Config.PosVendor };
            string requestString = JsonConvert.SerializeObject(statusRequest);

            if (!TcpClient.IsConnected)
            {
                if (string.IsNullOrWhiteSpace(Const.Locator.ParameterSetting.SdcUrl))
                {
                    MessageBoxEx.Show("E-SDC URL can not be null.", MessageBoxButton.OK);
                    //return null;
                    return;
                }
                string[] sdc = Const.Locator.ParameterSetting.SdcUrl.Split(':');
                if (sdc != null && sdc.Count() != 2)
                {
                    MessageBoxEx.Show("E-SDC URL is not in the right format.", MessageBoxButton.OK);
                    //return null;
                    return;
                }
                TcpClient.Connect(IPAddress.Parse(sdc[0]), int.Parse(sdc[1]));
            }
            TcpClient.Complated -= TcpClient_Complated;
            TcpClient.Complated += TcpClient_Complated;
            TcpClient.Send(0x01, requestString);

            //MessageModel messageModel = TcpClient.Recive();
            TcpClient.ReciveAsync();

            //return JsonConvert.DeserializeObject<StatusResponse>(messageModel.Message);
        }

        private static void TcpClient_Complated(object sender, MessageModel e)
        {
            try
            {
                if (e.MessageId != 0x01)
                {
                    return;
                }
                TcpClient.Complated -= TcpClient_Complated;
                StatusResponse statusResponse = JsonConvert.DeserializeObject<StatusResponse>(e.Message);
                if (statusResponse != null)
                {
                    //
                    Const.IsHasGetStatus = true;
                    //保存软件信息--此处处理未分开（每次都保存），正式使用的时候请
                    var info = (from a in Const.dB.PosInfo
                                select a).FirstOrDefault();
                    if (info != null)
                    {
                        Const.dB.Update<PosInfo>(new PosInfo { Id = info.Id, CompanyName = statusResponse.Manufacture, Desc = statusResponse.Model, Version = statusResponse.SoftwareVersion, IssueDate = info.IssueDate });
                    }
                    //记录税种信息
                    if (statusResponse.TaxInfo != null && statusResponse.TaxInfo.Count > 0)
                    {
                        Const.dB.CodeTaxtype.Delete();
                        foreach (var item in statusResponse.TaxInfo)
                        {
                            if (item.Category != null && item.Category.Count > 0)
                            {
                                foreach (var itm in item.Category)
                                {
                                    Const.dB.Insert<CodeTaxtype>(new CodeTaxtype
                                    {
                                        TaxTypeName = item.TaxTpye,
                                        TaxTypeCode = item.TaxTpye,
                                        TaxItemName = itm.TaxName,
                                        TaxItemCode = itm.CategoryId.ToString(),
                                        TaxRate = itm.TaxRate,
                                        EffectDate = itm.EffectiveDate,
                                        ExpireDate = itm.ExpiredDate
                                    });
                                }
                            }
                        }
                    }
                    //记录monitor信息


                    if (!statusResponse.isInitialized)
                    {
                        AttentionResponse attentionResponse = ServiceHelper.AttentionRequest();
                        if (attentionResponse.ATT_GSC == "0000")
                        {
                            //校验pin
                            PinView pinView = new PinView();
                        }
                        else
                        {
                            ShowMessageBegin("E-SDC is not available");
                        }
                    }
                    else
                    {
                        if (statusResponse.isLocked)
                        {
                            ShowMessageBegin("E-SDC is locked.");
                        }
                        else
                        {
                            ShowMessageBegin("E-SDC is available");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ShowMessageBegin(ex.Message);
            }
        }

        public static PinResponse VertifyPin(string pin)
        {
            PinRequest request = new PinRequest { VPIN = pin };
            string requestString = JsonConvert.SerializeObject(request);

            HttpHelper httpHelper = new HttpHelper();
            HttpItem httpItem = new HttpItem();
            httpItem.Method = "POST";
            httpItem.URL = Const.VerifyPinUri;
            httpItem.Postdata = Convert.ToString(requestString);


            httpItem.ResultType = ResultType.String;
            HttpResult html = httpHelper.GetHtml(httpItem);
            if (html.StatusCode != HttpStatusCode.OK)
            {
                throw new Exception(string.IsNullOrEmpty(html.Html) ? (string.IsNullOrEmpty(html.StatusDescription) ? "Post Data Error!" : html.StatusDescription) : html.Html);
            }
            return JsonConvert.DeserializeObject<PinResponse>(html.Html);
        }
        public static AttentionResponse AttentionRequest()
        {
            string requestString = JsonConvert.SerializeObject(new AttentionRequest { ATT = "Attention" });

            HttpHelper httpHelper = new HttpHelper();
            HttpItem httpItem = new HttpItem();
            httpItem.Method = "POST";
            httpItem.URL = Const.AttentionUri;
            httpItem.Postdata = Convert.ToString(requestString);


            httpItem.ResultType = ResultType.String;
            HttpResult html = httpHelper.GetHtml(httpItem);
            if (html.StatusCode != HttpStatusCode.OK)
            {
                throw new Exception(string.IsNullOrEmpty(html.Html) ? (string.IsNullOrEmpty(html.StatusDescription) ? "Post Data Error!" : html.StatusDescription) : html.Html);
            }
            return JsonConvert.DeserializeObject<AttentionResponse>(html.Html);
        }
        public static void SignRequest(SignRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException("SignRequest data can not be null.");
            }
            string requestString = JsonConvert.SerializeObject(request);


            if (!TcpClient.IsConnected)
            {
                if (string.IsNullOrWhiteSpace(Const.Locator.ParameterSetting.SdcUrl))
                {
                    MessageBoxEx.Show("E-SDC URL can not be null.", MessageBoxButton.OK);
                    return ;
                }
                string[] sdc = Const.Locator.ParameterSetting.SdcUrl.Split(':');
                if (sdc != null && sdc.Count() != 2)
                {
                    MessageBoxEx.Show("E-SDC URL is not in the right format.", MessageBoxButton.OK);
                    return ;
                }
                TcpClient.Connect(IPAddress.Parse(sdc[0]), int.Parse(sdc[1]));
            }
            TcpClient.Send(0x02, requestString);

            //MessageModel messageModel = TcpClient.Recive();
            TcpClient.Recive();

            //request.Hash = CaclBase64Md5Hash(requestString);
            //requestString = JsonConvert.SerializeObject(request);

            //HttpHelper httpHelper = new HttpHelper();
            //HttpItem httpItem = new HttpItem();
            //httpItem.Method = "POST";
            //httpItem.URL = Const.SignUri;
            //httpItem.Postdata = Convert.ToString(requestString);


            //httpItem.ResultType = ResultType.String;
            //HttpResult html = httpHelper.GetHtml(httpItem);
            //if (html.StatusCode != HttpStatusCode.OK)
            //{
            //    throw new Exception(string.IsNullOrEmpty(html.Html) ? (string.IsNullOrEmpty(html.StatusDescription) ? "Post Data Error!" : html.StatusDescription) : html.Html);
            //}

            //return JsonConvert.DeserializeObject<SignResponse>(messageModel.Message);
        }

        public static SignResponse LastSignRequest(LastSignRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException("SignRequest data can not be null.");
            }
            string requestString = JsonConvert.SerializeObject(request);

            HttpHelper httpHelper = new HttpHelper();
            HttpItem httpItem = new HttpItem();
            httpItem.Method = "POST";
            httpItem.URL = Const.SignUri;
            httpItem.Postdata = Convert.ToString(requestString);


            httpItem.ResultType = ResultType.String;
            HttpResult html = httpHelper.GetHtml(httpItem);
            if (html.StatusCode != HttpStatusCode.OK)
            {
                throw new Exception(string.IsNullOrEmpty(html.Html) ? (string.IsNullOrEmpty(html.StatusDescription) ? "Post Data Error!" : html.StatusDescription) : html.Html);
            }
            return JsonConvert.DeserializeObject<SignResponse>(html.Html);
        }

        public static bool CheckStatue()
        {
            bool result = false;
            try
            {
                StatueRequest();
                //StatusResponse statusResponse = StatueRequest();
                //if (statusResponse != null)
                //{
                //    //
                //    Const.IsHasGetStatus = true;
                //    //保存软件信息--此处处理未分开（每次都保存），正式使用的时候请
                //    var info = (from a in Const.dB.PosInfo
                //                select a).FirstOrDefault();
                //    if (info != null)
                //    {
                //        Const.dB.Update<PosInfo>(new PosInfo { Id = info.Id, CompanyName = statusResponse.Manufacture, Desc = statusResponse.Model, Version = statusResponse.SoftwareVersion, IssueDate = info.IssueDate });
                //    }
                //    //记录税种信息
                //    if (statusResponse.TaxInfo != null && statusResponse.TaxInfo.Count > 0)
                //    {
                //        Const.dB.CodeTaxtype.Delete();
                //        foreach (var item in statusResponse.TaxInfo)
                //        {
                //            if (item.Category != null && item.Category.Count > 0)
                //            {
                //                foreach (var itm in item.Category)
                //                {
                //                    Const.dB.Insert<CodeTaxtype>(new CodeTaxtype
                //                    {
                //                        TaxTypeName = item.TaxTpye,
                //                        TaxTypeCode = item.TaxTpye,
                //                        TaxItemName = itm.TaxName,
                //                        TaxItemCode = itm.CategoryId.ToString(),
                //                        TaxRate = itm.TaxRate,
                //                        EffectDate = itm.EffectiveDate,
                //                        ExpireDate = itm.ExpiredDate
                //                    });
                //                }
                //            }
                //        }
                //    }
                //    //记录monitor信息


                //    if (!statusResponse.isInitialized)
                //    {
                //        AttentionResponse attentionResponse = ServiceHelper.AttentionRequest();
                //        if (attentionResponse.ATT_GSC == "0000")
                //        {
                //            //校验pin
                //            PinView pinView = new PinView();
                //            result = pinView.ShowDialog().Value;
                //        }
                //        else
                //        {
                //            ShowMessageBegin("E-SDC is not available");
                //        }
                //    }
                //    else
                //    {
                //        if (statusResponse.isLocked)
                //        {
                //            ShowMessageBegin("E-SDC is locked.");
                //        }
                //        else
                //        {
                //            ShowMessageBegin("E-SDC is available");
                //            result = true;
                //        }
                //    }
                //}
            }
            catch (SocketException e)
            {
                ShowMessageBegin("Pos can not connect with ESDC.");
            }
            catch (Exception ex)
            {
                ShowMessageBegin(ex.Message);
                result = false;
            }
            return result;
        }

        public static void ShowMessage(string[] codes)
        {
            List<string> list = new List<string>();
            foreach (var item in codes)
            {
                if (Const.Statues != null)
                {
                    SystemStatu statu = Const.Statues.FirstOrDefault(a => a.Code == item);
                    if (statu != null)
                    {
                        list.Add(statu.Name);
                    }
                }
            }
            if (list.Count > 0)
            {
                MessageBoxEx.Show(string.Join(",", list.ToArray()));
            }
        }

        public static void ShowMessageBegin(string message)
        {
            Application.Current.Dispatcher.BeginInvoke(new Action(() =>
            {
                MessageBoxEx.Show(message);
            }));
        }

        public static string CaclBase64Md5Hash(string data)
        {
            return Convert.ToBase64String(new MD5CryptoServiceProvider().ComputeHash(Encoding.Unicode.GetBytes(data)));
        }
    }
}
