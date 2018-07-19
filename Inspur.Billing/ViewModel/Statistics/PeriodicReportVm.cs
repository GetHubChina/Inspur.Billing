using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using Inspur.Billing.Model.Service.Statistics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Inspur.Billing.ViewModel.Statistics
{
    public class PeriodicReportVm : ViewModelBase
    {
        #region 属性
        /// <summary>
        /// 获取或设置
        /// </summary>
        private PeriodicModel _periodic;
        /// <summary>
        /// 获取或设置
        /// </summary>
        public PeriodicModel Periodic
        {
            get { return _periodic; }
            set { Set<PeriodicModel>(ref _periodic, value, "Periodic"); }
        }

        #endregion

        #region 命令
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
                }, () =>
                {
                    return true;
                }));
            }
        }

        #endregion
    }
}
