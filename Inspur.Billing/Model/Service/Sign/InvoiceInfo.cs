using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inspur.Billing.Model.Service.Sign
{
    public class InvoiceInfo : ViewModelBase
    {
        /// <summary>
        /// 获取或设置
        /// </summary>
        private string _tpin;
        /// <summary>
        /// 获取或设置
        /// </summary>
        public string TPin
        {
            get { return _tpin; }
            set { Set<string>(ref _tpin, value, "TPin"); }
        }
        /// <summary>
        /// 获取或设置
        /// </summary>
        private string issueTime;
        /// <summary>
        /// 获取或设置
        /// </summary>
        public string IssueTime
        {
            get { return issueTime; }
            set { Set<string>(ref issueTime, value, "IssueTime"); }
        }
        /// <summary>
        /// 获取或设置
        /// </summary>
        private string _taxpayerName;
        /// <summary>
        /// 获取或设置
        /// </summary>
        public string TaxpayerName
        {
            get { return _taxpayerName; }
            set { Set<string>(ref _taxpayerName, value, "TaxpayerName"); }
        }
        /// <summary>
        /// 获取或设置
        /// </summary>
        private string _terminalID;
        /// <summary>
        /// 获取或设置
        /// </summary>
        public string TerminalID
        {
            get { return _terminalID; }
            set { Set<string>(ref _terminalID, value, "TerminalID"); }
        }
        /// <summary>
        /// 获取或设置
        /// </summary>
        private string _invoiceCode;
        /// <summary>
        /// 获取或设置
        /// </summary>
        public string InvoiceCode
        {
            get { return _invoiceCode; }
            set { Set<string>(ref _invoiceCode, value, "InvoiceCode"); }
        }
        /// <summary>
        /// 获取或设置
        /// </summary>
        private string _invoiceNumber;
        /// <summary>
        /// 获取或设置
        /// </summary>
        public string InvoiceNumber
        {
            get { return _invoiceNumber; }
            set { Set<string>(ref _invoiceNumber, value, "InvoiceNumber"); }
        }
        /// <summary>
        /// 获取或设置
        /// </summary>
        private double? _totalAmount;
        /// <summary>
        /// 获取或设置
        /// </summary>
        public double? TotalAmount
        {
            get { return _totalAmount; }
            set { Set<double?>(ref _totalAmount, value, "TotalAmount"); }
        }
        /// <summary>
        /// 获取或设置
        /// </summary>
        private string _creditFlag;
        /// <summary>
        /// 获取或设置
        /// </summary>
        public string CreditFlag
        {
            get { return _creditFlag; }
            set { Set<string>(ref _creditFlag, value, "CreditFlag"); }
        }

        public string Cashier { get; set; }
        public string BuyerTPIN { get; set; }
        public string BuyerName { get; set; }
        public string BuyerAddress { get; set; }
        public string BuyerTel { get; set; }
        public string SalesOrderNum { get; set; }

        /// <summary>
        /// 获取或设置
        /// </summary>
        private string _transactionType;
        /// <summary>
        /// 获取或设置
        /// </summary>
        public string TransactionType
        {
            get { return _transactionType; }
            set { Set<string>(ref _transactionType, value, "TransactionType"); }
        }

        public int PaymentMode { get; set; }
        public int SaleType { get; set; }
    }
}
