using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ControlLib.Controls.Dialogs
{
    /// <summary>
    /// MessageBoxEx.xaml 的交互逻辑
    /// </summary>
    public partial class MessageBoxEx : Window
    {
        public MessageBoxEx()
        {
            InitializeComponent();
        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
        private void ShowButton(MessageBoxButton messageBoxButton)
        {
            switch (messageBoxButton)
            {
                case MessageBoxButton.OK:
                    btnOk.Visibility = Visibility.Visible;
                    gdOkCancel.Visibility = Visibility.Collapsed;
                    break;
                case MessageBoxButton.OKCancel:
                    btnOk.Visibility = Visibility.Collapsed;
                    gdOkCancel.Visibility = Visibility.Visible;
                    break;
                case MessageBoxButton.YesNoCancel:
                    break;
                case MessageBoxButton.YesNo:
                    break;
                default:
                    btnOk.Visibility = Visibility.Visible;
                    gdOkCancel.Visibility = Visibility.Collapsed;
                    break;
            }
        }
        private void ShowText(string content)
        {
            tbkContent.Text = content;
        }
        public static bool? Show(string caption, string content, MessageBoxButton messageBoxButton)
        {
            MessageBoxEx messageDialog = new MessageBoxEx();
            messageDialog.Title = caption;
            messageDialog.ShowButton(messageBoxButton);
            messageDialog.ShowText(content);
            return messageDialog.ShowDialog();
        }
        public static bool? Show(string content, MessageBoxButton messageBoxButton)
        {
            return Show("System Prompt", content, messageBoxButton);
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }
    }
}
