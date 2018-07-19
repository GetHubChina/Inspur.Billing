﻿using ControlLib.Controls.Dialogs;
using DataModels;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using Inspur.Billing.Commom;
using Inspur.Billing.Model;
using Inspur.TaxModel;
using LinqToDB;
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
    public class ParameterSettingVm : ViewModelBase
    {
        #region 构造函数
        public ParameterSettingVm()
        {
            Printer.Instance.PrintPort = PrintPort;

            string[] ports = SerialPort.GetPortNames();
            if (ports != null && ports.Count() > 0)
            {
                SerialPorts = ports.ToList();
            }
            ParityList = System.Enum.GetNames(typeof(Parity)).ToList();
            StopBitsList = Enum.GetNames(typeof(StopBits)).ToList();
        }
        #endregion

        #region 字段
        private string _sdcId = null;
        #endregion

        #region 属性
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
        /// 获取或设置打印端口
        /// </summary>
        private string _printPort = "SP-USB1";
        /// <summary>
        /// 获取或设置打印端口
        /// </summary>
        public string PrintPort
        {
            get { return _printPort; }
            set { Set<string>(ref _printPort, value, "Port"); }
        }

        /// <summary>
        /// 获取或设置pos软件信息
        /// </summary>
        private PosInfo _posInfo;
        /// <summary>
        /// 获取或设置pos软件信息
        /// </summary>
        public PosInfo PosInfo
        {
            get { return _posInfo; }
            set { Set<PosInfo>(ref _posInfo, value, "PosInfo"); }
        }
        /// <summary>
        /// 获取或设置参数设置是否可用
        /// </summary>
        private bool _isParameterEnable = false;
        /// <summary>
        /// 获取或设置参数设置是否可用
        /// </summary>
        public bool IsParameterEnable
        {
            get { return _isParameterEnable; }
            set { Set<bool>(ref _isParameterEnable, value, "IsParameterEnable"); }
        }
        /// <summary>
        /// 获取或设置纳税人信息是否可以编辑
        /// </summary>
        private bool _isTaxPayerEnable = false;
        /// <summary>
        /// 获取或设置纳税人信息是否可以编辑
        /// </summary>
        public bool IsTaxPayerEnable
        {
            get { return _isTaxPayerEnable; }
            set { Set<bool>(ref _isTaxPayerEnable, value, "IsTaxPayerEnable"); }
        }


        /// <summary>
        /// 获取或设置通讯模式列表
        /// </summary>
        private List<CodeTable> _commModes = new List<CodeTable> { new CodeTable { Name = "Network", Code = "0" }, new CodeTable { Name = "Serial", Code = "1" } };
        /// <summary>
        /// 获取或设置通讯模式列表
        /// </summary>
        public List<CodeTable> CommModes
        {
            get { return _commModes; }
            set { Set<List<CodeTable>>(ref _commModes, value, "CommModes"); }
        }
        /// <summary>
        /// 获取或设置选择的通讯模式
        /// </summary>
        private CodeTable _selectModes;
        /// <summary>
        /// 获取或设置选择的通讯模式
        /// </summary>
        public CodeTable SelectedModes
        {
            get { return _selectModes; }
            set
            {
                if (value != _selectModes)
                {
                    _selectModes = value;
                    if (_selectModes != null && _selectModes.Code == "1")
                    {
                        SerialParamVis = Visibility.Visible;
                    }
                    else
                    {
                        SerialParamVis = Visibility.Collapsed;
                    }
                    RaisePropertyChanged(() => this.SelectedModes);
                }
            }
        }
        /// <summary>
        /// 获取或设置
        /// </summary>
        private Visibility _serialParamVis;
        /// <summary>
        /// 获取或设置
        /// </summary>
        public Visibility SerialParamVis
        {
            get { return _serialParamVis; }
            set { Set<Visibility>(ref _serialParamVis, value, "SerialParamVis"); }
        }
        /// <summary>
        /// 获取或设置
        /// </summary>
        private List<string> _serialPorts;
        /// <summary>
        /// 获取或设置
        /// </summary>
        public List<string> SerialPorts
        {
            get { return _serialPorts; }
            set
            {
                if (value != _serialPorts)
                {
                    _serialPorts = value;
                    RaisePropertyChanged(() => this.SerialPorts);
                }
            }
        }
        /// <summary>
        /// 获取或设置
        /// </summary>
        private string _selectedPort;
        /// <summary>
        /// 获取或设置
        /// </summary>
        public string SelectedPort
        {
            get { return _selectedPort; }
            set
            {
                if (value != _selectedPort)
                {
                    _selectedPort = value;
                    RaisePropertyChanged(() => this.SelectedPort);
                }
            }
        }
        /// <summary>
        /// 获取或设置波特率集合
        /// </summary>
        private List<string> _baudRates = new List<string> { "115200", "57600", "38400", "19200", "14400", "9600" };
        /// <summary>
        /// 获取或设置波特率集合
        /// </summary>
        public List<string> BaudRates
        {
            get { return _baudRates; }
            set { Set<List<string>>(ref _baudRates, value, "BaudRates"); }
        }
        /// <summary>
        /// 获取或设置波特率
        /// </summary>
        private string _selectedBaudRate;
        /// <summary>
        /// 获取或设置波特率
        /// </summary>
        public string SelectedBaudRate
        {
            get { return _selectedBaudRate; }
            set { Set<string>(ref _selectedBaudRate, value, "SelectedBaudRate"); }
        }
        /// <summary>
        /// 获取或设置奇偶校验位
        /// </summary>
        private List<string> _parityList;
        /// <summary>
        /// 获取或设置奇偶校验位
        /// </summary>
        public List<string> ParityList
        {
            get { return _parityList; }
            set { Set<List<string>>(ref _parityList, value, "ParityList"); }
        }
        /// <summary>
        /// 获取或设置选中的奇偶校验位
        /// </summary>
        private string _selectedParity;
        /// <summary>
        /// 获取或设置选中的奇偶校验位
        /// </summary>
        public string SelectedParity
        {
            get { return _selectedParity; }
            set { Set<string>(ref _selectedParity, value, "SelectedParity"); }
        }
        /// <summary>
        /// 获取或设置数据位集合
        /// </summary>
        private List<string> _dataBitsList = new List<string> { "8", "7", "6", "5" };
        /// <summary>
        /// 获取或设置数据位集合
        /// </summary>
        public List<string> DataBitsList
        {
            get { return _dataBitsList; }
            set { Set<List<string>>(ref _dataBitsList, value, "DataBitsList"); }
        }
        /// <summary>
        /// 获取或设置选择的数据位
        /// </summary>
        private string _selectedDataBits;
        /// <summary>
        /// 获取或设置选择的数据位
        /// </summary>
        public string SelectedDataBits
        {
            get { return _selectedDataBits; }
            set { Set<string>(ref _selectedDataBits, value, "SelectedDataBits"); }
        }
        /// <summary>
        /// 获取或设置停止位集合
        /// </summary>
        private List<string> _stopBitsList;
        /// <summary>
        /// 获取或设置停止位集合
        /// </summary>
        public List<string> StopBitsList
        {
            get { return _stopBitsList; }
            set { Set<List<string>>(ref _stopBitsList, value, "StopBitsList"); }
        }
        /// <summary>
        /// 获取或设置选择的停止位
        /// </summary>
        private string _selectedStopBits;
        /// <summary>
        /// 获取或设置选择的停止位
        /// </summary>
        public string SelectedStopBits
        {
            get { return _selectedStopBits; }
            set { Set<string>(ref _selectedStopBits, value, "SelectedStopBits"); }
        }


        public Commom.CommModel CommModel
        {
            get
            {
                return (SelectedModes == null || SelectedModes.Code == "1") ? Commom.CommModel.SerialPort : Commom.CommModel.NetPort;
            }
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
                    try
                    {
                        switch (p)
                        {
                            case "Loaded":
                                LoadTaxpayerInfo();
                                LoadSDCInfo();
                                //验证sdc
                                Application.Current.Dispatcher.BeginInvoke(new Action(() =>
                                {
                                    //ServiceHelper.CheckStatue();
                                    //防止请求报错，软件信息为空
                                    LoadSoftwareInfo();
                                }));
                                break;
                            case "SDCTest":
                                ServiceHelper.CheckStatue();
                                break;
                            case "PrinterPortTest":
                                Printer.Instance.PrintPort = PrintPort;
                                Printer.Instance.PrintTestPaper();
                                break;
                            default:
                                break;
                        }
                    }
                    catch (Exception ex)
                    {
                        switch (p)
                        {
                            case "Loaded":
                                LoadSoftwareInfo();
                                break;
                            default:
                                break;
                        }

                        MessageBoxEx.Show(ex.Message, MessageBoxButton.OK);
                    }
                }, a =>
                {
                    return true;
                }));
            }
        }
        /// <summary>
        /// 获取或设置纳税人信息编辑命令
        /// </summary>
        private ICommand _taxPayerEditCommand;
        /// <summary>
        /// 获取或设置纳税人信息编辑命令
        /// </summary>
        public ICommand TaxPayerEditCommand
        {
            get
            {
                return _taxPayerEditCommand ?? (_taxPayerEditCommand = new RelayCommand(() =>
                {
                    IsTaxPayerEnable = true;
                }, () =>
                {
                    return !IsTaxPayerEnable;
                }));
            }
        }
        /// <summary>
        /// 获取或设置纳税人信息保存命令
        /// </summary>
        private ICommand taxPayerSaveCommand;
        /// <summary>
        /// 获取或设置纳税人信息保存命令
        /// </summary>
        public ICommand TaxPayerSaveCommand
        {
            get
            {
                return taxPayerSaveCommand ?? (taxPayerSaveCommand = new RelayCommand(() =>
                {
                    TaxPayerSave();
                }, () =>
                {
                    return IsTaxPayerEnable;
                }));
            }
        }
        /// <summary>
        /// 获取或设置纳税人信息设置取消命令
        /// </summary>
        private ICommand taxPayerCancelCommand;
        /// <summary>
        /// 获取或设置纳税人信息设置取消命令
        /// </summary>
        public ICommand TaxPayerCancelCommand
        {
            get
            {
                return taxPayerCancelCommand ?? (taxPayerCancelCommand = new RelayCommand(() =>
                {
                    LoadTaxpayerInfo();
                }, () =>
                {
                    return IsTaxPayerEnable;
                }));
            }
        }
        /// <summary>
        /// 获取或设置网络设置编辑命令
        /// </summary>
        private ICommand _netSettingEditCommand;
        /// <summary>
        /// 获取或设置网络设置编辑命令
        /// </summary>
        public ICommand NetSettingEditCommand
        {
            get
            {
                return _netSettingEditCommand ?? (_netSettingEditCommand = new RelayCommand(() =>
                {
                    IsParameterEnable = true;
                }, () =>
                {
                    return !IsParameterEnable;
                }));
            }
        }
        /// <summary>
        /// 获取或设置网络设置保存命令
        /// </summary>
        private ICommand _netSettingSaveCommand;
        /// <summary>
        /// 获取或设置网络设置保存命令
        /// </summary>
        public ICommand NetSettingSaveCommand
        {
            get
            {
                return _netSettingSaveCommand ?? (_netSettingSaveCommand = new RelayCommand(() =>
                {
                    if (string.IsNullOrEmpty(SdcUrl))
                    {
                        MessageBoxEx.Show("E-SDC URL can not null.", MessageBoxButton.OK);
                        return;
                    }
                    string[] sdc = SdcUrl.Split(':');
                    if (sdc != null && sdc.Count() != 2)
                    {
                        MessageBoxEx.Show("E-SDC URL is not in the right format.", MessageBoxButton.OK);
                        return;
                    }
                    if (string.IsNullOrEmpty(_sdcId))
                    {
                        //insert
                        Const.dB.Insert<SdcInfo>(new SdcInfo
                        {
                            SdcIp = sdc[0],
                            SdcPort = sdc[1]
                        });
                        LoadSDCInfo();
                    }
                    else
                    {
                        //updata
                        long id;
                        long.TryParse(_sdcId, out id);
                        Const.dB.Update<SdcInfo>(new SdcInfo
                        {
                            SdcId = id,
                            SdcIp = sdc[0],
                            SdcPort = sdc[1]
                        });
                        IsParameterEnable = false;
                        Printer.Instance.PrintPort = PrintPort;
                    }
                }, () =>
                {
                    return IsParameterEnable;
                }));
            }
        }
        /// <summary>
        /// 获取或设置网络设置取消命令
        /// </summary>
        private ICommand _netSettingCancelCommand;
        /// <summary>
        /// 获取或设置网络设置取消命令
        /// </summary>
        public ICommand NetSettingCancelCommand
        {
            get
            {
                return _netSettingCancelCommand ?? (_netSettingCancelCommand = new RelayCommand(() =>
                {
                    LoadSDCInfo();
                }, () =>
                {
                    return IsParameterEnable;
                }));
            }
        }
        #endregion

        #region 方法
        private void LoadTaxpayerInfo()
        {
            var taxpayer = (from b in Const.dB.TaxpayerJnfo
                            select b).ToList();
            if (taxpayer != null && taxpayer.Count > 0)
            {
                EntityAdapter.TaxpayerJnfo2TaxPayer(taxpayer[0], TaxPayerInfo);
            }
            IsTaxPayerEnable = false;
        }
        private void LoadSDCInfo()
        {
            var sdcInfoes = (from a in Const.dB.SdcInfo
                             select a).ToList();
            if (sdcInfoes != null && sdcInfoes.Count() > 0)
            {
                _sdcId = sdcInfoes[0].SdcId.ToString();
                SdcUrl = string.Format("{0}:{1}", sdcInfoes[0].SdcIp, sdcInfoes[0].SdcPort);
            }
            IsParameterEnable = false;
        }
        private void LoadSoftwareInfo()
        {
            var posInfoes = (from a in Const.dB.PosInfo
                             select a).ToList();
            if (posInfoes != null && posInfoes.Count() > 0)
            {
                PosInfo = posInfoes[0];
            }
        }
        private void TaxPayerSave()
        {
            if (_taxPayerInfo != null)
            {
                if (string.IsNullOrWhiteSpace(_taxPayerInfo.Tin))
                {
                    MessageBoxEx.Show("请输入纳税人识别号。", MessageBoxButton.OK);
                    return;
                }
                if (string.IsNullOrWhiteSpace(_taxPayerInfo.Name))
                {
                    MessageBoxEx.Show("请输入纳税人名称。", MessageBoxButton.OK);
                    return;
                }
                if (string.IsNullOrWhiteSpace(_taxPayerInfo.Address))
                {
                    MessageBoxEx.Show("请输入纳税人地址。", MessageBoxButton.OK);
                    return;
                }
                if (string.IsNullOrWhiteSpace(_taxPayerInfo.Telphone))
                {
                    MessageBoxEx.Show("请输入纳税人联系电话。", MessageBoxButton.OK);
                    return;
                }
                if (string.IsNullOrWhiteSpace(_taxPayerInfo.BankAccount))
                {
                    MessageBoxEx.Show("请输入纳税人银行账号。", MessageBoxButton.OK);
                    return;
                }
                int result;
                if (string.IsNullOrWhiteSpace(TaxPayerInfo.Id))
                {
                    //insert
                    result = Const.dB.Insert<TaxpayerJnfo>(EntityAdapter.TaxPayer2TaxpayerJnfo(TaxPayerInfo));
                }
                else
                {
                    //update
                    int id = 0;
                    int.TryParse(TaxPayerInfo.Id, out id);
                    TaxpayerJnfo info = EntityAdapter.TaxPayer2TaxpayerJnfo(TaxPayerInfo);
                    info.TaxpayerId = id;
                    result = Const.dB.Update<TaxpayerJnfo>(info);
                }
                if (result > 0)
                {
                    IsTaxPayerEnable = false;
                }
            }
        }
        #endregion
    }
}
