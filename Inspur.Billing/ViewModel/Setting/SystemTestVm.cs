using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Inspur.Billing.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Inspur.Billing.ViewModel.Setting
{
    public class SystemTestVm : ViewModelBase
    {
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

                }, a =>
                {
                    return true;
                }));
            }
        }

        #endregion
    }
}
