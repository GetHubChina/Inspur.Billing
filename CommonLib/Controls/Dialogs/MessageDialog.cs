using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CommonLib.Controls.Dialogs
{
    public class MessageDialog : System.Windows.Window
    {
        public MessageBoxButton MessageBoxButton
        {
            get { return (MessageBoxButton)GetValue(MessageBoxButtonProperty); }
            set { SetValue(MessageBoxButtonProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MessageBoxButton.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MessageBoxButtonProperty =
            DependencyProperty.Register("MessageBoxButton", typeof(MessageBoxButton), typeof(MessageDialog), new PropertyMetadata(MessageBoxButton.OK));


        public static void Show(string caption, string content, MessageBoxButton messageBoxButton)
        {
            MessageDialog messageDialog = new MessageDialog();
            messageDialog.Content = content;
            messageDialog.Title = caption;
            messageDialog.MessageBoxButton = messageBoxButton;
            messageDialog.ShowDialog();
        }
        public static void Show(string content, MessageBoxButton messageBoxButton)
        {
            Show("System Prompt", content, messageBoxButton);
        }
    }
}
