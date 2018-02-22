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

namespace Inspur.Billing.Commom
{
    class ServiceHelper
    {
        public static string CurrentTime = DateTime.UtcNow.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ssZ");

        public static StatusResponse StatueRequest()
        {
            StatusRequest statusRequest = new StatusRequest() { GS = "GetStatus" };
            string requestString = JsonConvert.SerializeObject(statusRequest);

            HttpHelper httpHelper = new HttpHelper();
            HttpItem httpItem = new HttpItem();
            httpItem.Method = "POST";
            httpItem.URL = Const.GetStatusUri;
            httpItem.Postdata = Convert.ToString(requestString);


            httpItem.ResultType = ResultType.String;
            HttpResult html = httpHelper.GetHtml(httpItem);
            if (html.StatusCode != HttpStatusCode.OK)
            {
                throw new Exception(string.IsNullOrEmpty(html.Html) ? (string.IsNullOrEmpty(html.StatusDescription) ? "Post Data Error!" : html.StatusDescription) : html.Html);
            }
            return JsonConvert.DeserializeObject<StatusResponse>(html.Html);
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
        public static SignResponse SignRequest(SignRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException("SignRequest data can not be null.");
            }
            string requestString = JsonConvert.SerializeObject(request);
            request.Hash = CaclBase64Md5Hash(requestString);
            requestString = JsonConvert.SerializeObject(request);

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
            StatusResponse statusResponse = StatueRequest();
            if (statusResponse.GSC.Contains("0000") && statusResponse.MSSC.Contains("0000"))
            {
                //保存软件信息--此处处理未分开（每次都保存），正式使用的时候请
                var info = (from a in Const.dB.PosInfo
                            select a).FirstOrDefault();
                if (info != null)
                {
                    Const.dB.Update<PosInfo>(new PosInfo { Id = info.Id, CompanyName = statusResponse.Make, Desc = statusResponse.Model, Version = statusResponse.SoftwareVersion, IssueDate = info.IssueDate });
                }
                if (statusResponse.IsPinRequired)
                {
                    //Attention
                    AttentionResponse attentionResponse = ServiceHelper.AttentionRequest();
                    if (attentionResponse.ATT_GSC == "0000")
                    {
                        //校验pin
                        PinView pinView = new PinView();
                        result = pinView.ShowDialog().Value;
                    }
                    else
                    {
                        ShowMessageBegin("E-SDC is not available");
                    }
                }
                else
                {
                    result = true;
                }
            }
            else
            {
                List<string> list = new List<string>();
                if (!statusResponse.GSC.Contains("0000"))
                {
                    foreach (var item in statusResponse.GSC)
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
                }
                if (!statusResponse.MSSC.Contains("0000"))
                {
                    foreach (var item in statusResponse.MSSC)
                    {
                        list.Add(item);
                    }
                }
                if (list.Count > 0)
                {
                    ShowMessageBegin(string.Join(",", list.ToArray()));
                }
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
