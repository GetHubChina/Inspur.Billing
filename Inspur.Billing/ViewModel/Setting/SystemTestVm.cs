﻿using ControlLib.Controls.Dialogs;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Inspur.Billing.Model;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Inspur.Billing.ViewModel.Setting
{
    public class SystemTestVm : ViewModelBase
    {
        #region 构造函数
        public SystemTestVm()
        {
            string[] ports = SerialPort.GetPortNames();
            if (ports != null && ports.Count() > 0)
            {
                SerialPorts = ports.ToList();
            }
            ParityList = System.Enum.GetNames(typeof(Parity)).ToList();
            StopBitsList = Enum.GetNames(typeof(StopBits)).ToList();
            _serialPort.DataReceived += _serialPort_DataReceived;
        }
        #endregion

        #region 字段
        /// <summary>
        /// 串行端口通讯对象
        /// </summary>
        SerialPort _serialPort = new SerialPort();
        /// <summary>
        /// 发送按钮是否可用
        /// </summary>
        private bool _isCanSend = true;
        #endregion

        #region 属性
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
        private List<string> _baudRates = new List<string> { "9600", "14400", "19200", "38400", "57600", "115200" };
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
        private List<string> _dataBitsList = new List<string> { "5", "6", "7", "8" };
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
                        if (SelectedModes != null)
                        {
                            switch (SelectedModes.Code)
                            {
                                case "0"://网口通讯
                                    break;
                                case "1"://串口通讯
                                    if (!_serialPort.IsOpen)
                                    {
                                        if (string.IsNullOrWhiteSpace(SelectedPort))
                                        {
                                            throw new Exception("Port can not be null.");
                                        }
                                        if (string.IsNullOrWhiteSpace(SelectedDataBits))
                                        {
                                            throw new Exception("DataBits can not be null.");
                                        }
                                        if (string.IsNullOrWhiteSpace(SelectedBaudRate))
                                        {
                                            throw new Exception("BaudRate can not be null.");
                                        }
                                        if (string.IsNullOrWhiteSpace(SelectedParity))
                                        {
                                            throw new Exception("Parity can not be null.");
                                        }
                                        if (string.IsNullOrWhiteSpace(SelectedStopBits))
                                        {
                                            throw new Exception("StopBits can not be null.");
                                        }
                                        _serialPort.BaudRate = int.Parse(SelectedBaudRate);
                                        _serialPort.PortName = SelectedPort;
                                        _serialPort.Parity = (Parity)Enum.Parse(typeof(Parity), SelectedParity);
                                        _serialPort.DataBits = int.Parse(SelectedDataBits);
                                        _serialPort.StopBits = (StopBits)Enum.Parse(typeof(StopBits), SelectedStopBits);
                                        _serialPort.Open();
                                        
                                    }
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBoxEx.Show(ex.Message, MessageBoxButton.OK);
                    }
                }, a =>
                {
                    return _isCanSend;
                }));
            }
        }

        private void _serialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}