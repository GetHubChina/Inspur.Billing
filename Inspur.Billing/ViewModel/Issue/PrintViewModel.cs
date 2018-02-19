using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using Inspur.Billing.Commom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Inspur.Billing.ViewModel.Issue
{
    public class PrintViewModel : ViewModelBase
    {
        #region 属性
        /// <summary>
        /// 获取或设置
        /// </summary>
        private CreditViewModel _credit;
        /// <summary>
        /// 获取或设置
        /// </summary>
        public CreditViewModel Credit
        {
            get { return _credit; }
            set { Set<CreditViewModel>(ref _credit, value, "Credit"); }
        }
        /// <summary>
        /// 获取或设置当前时间
        /// </summary>
        private string _currentTime = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
        /// <summary>
        /// 获取或设置当前时间
        /// </summary>
        public string CurrentTime
        {
            get { return _currentTime; }
            set { Set<string>(ref _currentTime, value, "CurrentTime"); }
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
                   if (Credit == null)
                   {
                       return;
                   }
                   Printer.Instance.Print(() =>
                   {
                       Printer.Instance.SetAlign(1);
                       Printer.Instance.PrintString(0, 1, 0, 0, 0, string.Format("Order Number:{0}\r\n{1}\r\n", Credit.OrderNumber, CurrentTime));

                       Printer.Instance.SetAlign(0);
                       SetTwoColumnPrint("POSID", Credit.PosNumber, "Cashier:", Credit.Cashier);
                       SetTwoColumnPrint("Buyer TIN", "", "", Credit.Buyer.Tin);
                       SetTwoColumnPrint("Buyer Name", "", "", Credit.Buyer.Name);
                       SetTwoColumnPrint("Buyer Address", "", "", Credit.Buyer.Address);
                       SetTwoColumnPrint("Buyer Contact", "", "", Credit.Buyer.TelPhone);
                       Printer.Instance.PrintString(0, 0, 0, 0, 0, "———————————————— \r\n");

                       Printer.Instance.SetAlign(1);
                       Printer.Instance.PrintString(0, 1, 0, 0, 0, "Particular Of Items\r\n");
                       //表格字符占用按照7 8 7 3 7来打印
                       Printer.Instance.PrintString(0, 1, 0, 0, 0, "  GIN  Item Name Price Qty Value \r\n");

                       //Printer.Instance.PrintString(0, 1, 0, 0, 0, string.Format("POSID:{0} ", Credit.PosNumber));

                       //Printer.Instance.SetAlign(2);

                       //Printer.Instance.PrintString(0, 1, 0, 0, 0, string.Format("Cashier:{0} \r\n", Credit.Cashier));

                       //Printer.Instance.SetAlign(0);
                       //Printer.Instance.PrintString(0, 1, 0, 0, 0, "Buyer TIN:");
                       //Printer.Instance.SetAlign(2);
                       //Printer.Instance.PrintString(0, 1, 0, 0, 0, string.Format("{0}\r\n", Credit.Buyer != null ? Credit.Buyer.Tin : ""));


                       //Printer.Instance.PrintString(0, 0, 0, 0, 0, "———————————————— \r\n");
                       //Printer.Instance.CutPaper(1, 3);
                   });
               }, () =>
               {
                   return true;
               }));
            }
        }
        /// <summary>
        /// 目前在api中没有找到同一行两列的打印方式，先使用此方法
        /// </summary>
        /// <param name="leftName"></param>
        /// <param name="leftValue"></param>
        /// <param name="rightName"></param>
        /// <param name="rightValue"></param>
        private void SetTwoColumnPrint(string leftName, string leftValue, string rightName, string rightValue)
        {
            string left = string.Format("{0}:{1}", leftName, leftValue);
            string right = string.Format("{0}{1}", rightName, rightValue);
            StringBuilder sb = new StringBuilder();
            if (left.Length + right.Length < 32)
            {
                sb.Append(' ', 32 - left.Length - right.Length);
            }
            Printer.Instance.PrintString(0, 1, 0, 0, 0, string.Format("{0}{1}{2}\r\n", left, sb.ToString(), right));
        }

        #endregion
    }
}
