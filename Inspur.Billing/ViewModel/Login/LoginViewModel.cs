using CommonLib.Crypt;
using CommonLib.Helper;
using ControlLib.Controls.Dialogs;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Inspur.Billing.Commom;
using Inspur.Billing.View;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Inspur.Billing.ViewModel.Login
{
    public class LoginViewModel : ViewModelBase
    {
        #region 字段
        Window _loginView;
        #endregion

        #region 属性
        /// <summary>
        /// 获取或设置
        /// </summary>
        private string _userName;
        /// <summary>
        /// 获取或设置
        /// </summary>
        public string UserName
        {
            get { return _userName; }
            set { Set<string>(ref _userName, value, "UserName"); }
        }
        /// <summary>
        /// 获取或设置
        /// </summary>
        private string _password;
        /// <summary>
        /// 获取或设置
        /// </summary>
        public string Password
        {
            get { return _password; }
            set { Set<string>(ref _password, value, "Password"); }
        }

        #endregion

        #region Command
        /// <summary>
        /// 获取或设置
        /// </summary>
        private ICommand _loadedCommand;
        /// <summary>
        /// 获取或设置
        /// </summary>
        public ICommand LoadedCommand
        {
            get
            {
                return _loadedCommand ?? (_loadedCommand = new RelayCommand<Window>(p =>
                {
                    _loginView = p;
                }, a =>
                {
                    return true;
                }));
            }
        }

        /// <summary>
        /// 获取或设置登录命令
        /// </summary>
        private ICommand _loginCommand;
        /// <summary>
        /// 获取或设置登录命令
        /// </summary>
        public ICommand LoginCommand
        {
            get
            {
                return _loginCommand ?? (_loginCommand = new RelayCommand(() =>
                {
                    if (string.IsNullOrEmpty(_userName))
                    {
                        MessageBoxEx.Show("用户名称不能为空。", MessageBoxButton.OK);
                        return;
                    }
                    if (string.IsNullOrWhiteSpace(_password))
                    {
                        MessageBoxEx.Show("用户密码不能为空。", MessageBoxButton.OK);
                        return;
                    }
                    string sql = string.Format("SELECT * FROM cashier t WHERE t.name='{0}';",_userName);
                    DataSet ds = SQLiteHelper.ExecuteDataSet(Const.ConnectString, sql, null);
                    if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows != null && ds.Tables[0].Rows.Count > 0)
                    {
                        string password = ds.Tables[0].Rows[0]["password"].ToString();
                        string p = Md5Crypt.MD5Encrypt32(_password);
                        if (!Md5Crypt.MD5Encrypt32(_password).ToLower().Equals(password))
                        {
                            MessageBoxEx.Show("密码不正确。", MessageBoxButton.OK);
                            return;
                        }
                    }
                    else
                    {
                        MessageBoxEx.Show("用户不存在。", MessageBoxButton.OK);
                        return;
                    }
                    MainWindow mainWindow = new MainWindow();
                    Application.Current.MainWindow = mainWindow;
                    if (_loginView != null)
                    {
                        _loginView.Close();
                    }
                    mainWindow.ShowDialog();
                }, () =>
                {
                    return true;
                }));
            }
        }

        #endregion
    }
}
