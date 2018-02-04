using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Inspur.TaxModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Inspur.Billing.ViewModel.Setting
{
    public class BasicViewModel : ViewModelBase
    {
        #region 属性
        /// <summary>
        /// 获取或设置
        /// </summary>
        private TaxPayer _taxPayerInfo;
        /// <summary>
        /// 获取或设置
        /// </summary>
        public TaxPayer TaxPayerInfo
        {
            get { return _taxPayerInfo; }
            set { Set<TaxPayer>(ref _taxPayerInfo, value, "TaxPayerInfo"); }
        }
        /// <summary>
        /// 获取或设置sdc地址
        /// </summary>
        private string _sdcUrl;
        /// <summary>
        /// 获取或设置sdc地址
        /// </summary>
        public string SdcUrl
        {
            get { return _sdcUrl; }
            set { Set<string>(ref _sdcUrl, value, "SdcUrl"); }
        }
        /// <summary>
        /// 获取或设置输出端口
        /// </summary>
        private string _port;
        /// <summary>
        /// 获取或设置输出端口
        /// </summary>
        public string Port
        {
            get { return _port; }
            set { Set<string>(ref _port, value, "Port"); }
        }
        /// <summary>
        /// 获取或设置软件制造商
        /// </summary>
        private string _make;
        /// <summary>
        /// 获取或设置软件制造商
        /// </summary>
        public string Make
        {
            get { return _make; }
            set { Set<string>(ref _make, value, "Make"); }
        }
        /// <summary>
        /// 获取或设置软件名称
        /// </summary>
        private string _model;
        /// <summary>
        /// 获取或设置软件名称
        /// </summary>
        public string Model
        {
            get { return _model; }
            set { Set<string>(ref _model, value, "Model"); }
        }
        /// <summary>
        /// 获取或设置软件版本
        /// </summary>
        private string _version;
        /// <summary>
        /// 获取或设置软件版本
        /// </summary>
        public string Version
        {
            get { return _version; }
            set { Set<string>(ref _version, value, "Version"); }
        }
        /// <summary>
        /// 获取或设置软件发布时间
        /// </summary>
        private string _releaseTime;
        /// <summary>
        /// 获取或设置软件发布时间
        /// </summary>
        public string ReleaseTime
        {
            get { return _releaseTime; }
            set { Set<string>(ref _releaseTime, value, "ReleaseTime"); }
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
                    switch (p)
                    {
                        case "TaxPayerEdit":
                            break;
                        case "TaxPayerSave":
                            break;
                        case "TaxPayerCancel":
                            break;
                        case "SDCTest":
                            break;
                        case "PrinterPortTest":
                            break;
                        case "NetSettingEdit":
                            break;
                        case "NetSettingSave":
                            break;
                        case "NetSettingCancel":
                            break;
                        case "SoftwareCancel":
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
    }
}
