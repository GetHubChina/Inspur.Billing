﻿using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Inspur.Billing.View;
using System;
using System.Collections.Generic;
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
                        MessageBox.Show("用户名称不能为空。");
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
