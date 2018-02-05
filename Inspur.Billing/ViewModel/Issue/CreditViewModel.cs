using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Inspur.Billing.Model;
using Inspur.Billing.View.Issue;
using Inspur.TaxModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Inspur.Billing.ViewModel.Issue
{
    public class CreditViewModel : ViewModelBase
    {
        #region 属性
        /// <summary>
        /// 获取或设置
        /// </summary>
        private string _orderNumber;
        /// <summary>
        /// 获取或设置
        /// </summary>
        public string OrderNumber
        {
            get { return _orderNumber; }
            set { Set<string>(ref _orderNumber, value, "OrderNumber"); }
        }
        /// <summary>
        /// 获取或设置
        /// </summary>
        private string _buyerTin;
        /// <summary>
        /// 获取或设置
        /// </summary>
        public string BuyerTin
        {
            get { return _buyerTin; }
            set { Set<string>(ref _buyerTin, value, "BuyerTin"); }
        }
        /// <summary>
        /// 获取或设置
        /// </summary>
        private string _buyerName;
        /// <summary>
        /// 获取或设置
        /// </summary>
        public string BuyerName
        {
            get { return _buyerName; }
            set { Set<string>(ref _buyerName, value, "BuyerName"); }
        }
        /// <summary>
        /// 获取或设置
        /// </summary>
        private string _buyerAdress;
        /// <summary>
        /// 获取或设置
        /// </summary>
        public string BuyerAdress
        {
            get { return _buyerAdress; }
            set { Set<string>(ref _buyerAdress, value, "BuyerAdress"); }
        }
        /// <summary>
        /// 获取或设置
        /// </summary>
        private string _buyerContact;
        /// <summary>
        /// 获取或设置
        /// </summary>
        public string BuyerContact
        {
            get { return _buyerContact; }
            set { Set<string>(ref _buyerContact, value, "BuyerContact"); }
        }
        /// <summary>
        /// 获取或设置
        /// </summary>
        private List<CodeTable> _transactionType;
        /// <summary>
        /// 获取或设置
        /// </summary>
        public List<CodeTable> TransactionType
        {
            get { return _transactionType; }
            set { Set<List<CodeTable>>(ref _transactionType, value, "TransactionType"); }
        }
        /// <summary>
        /// 获取或设置
        /// </summary>
        private List<CodeTable> _paymentType;
        /// <summary>
        /// 获取或设置
        /// </summary>
        public List<CodeTable> PaymentType
        {
            get { return _paymentType; }
            set { Set<List<CodeTable>>(ref _paymentType, value, "PaymentType"); }
        }
        /// <summary>
        /// 获取或设置
        /// </summary>
        private ObservableCollection<ProductItem> _productes = new ObservableCollection<ProductItem>();
        /// <summary>
        /// 获取或设置
        /// </summary>
        public ObservableCollection<ProductItem> Productes
        {
            get { return _productes; }
            set { Set<ObservableCollection<ProductItem>>(ref _productes, value, "Productes"); }
        }
        /// <summary>
        /// 获取或设置
        /// </summary>
        private ProductItem _selectedItem;
        /// <summary>
        /// 获取或设置
        /// </summary>
        public ProductItem SelectedItem
        {
            get { return _selectedItem; }
            set { Set<ProductItem>(ref _selectedItem, value, "SelectedItem"); }
        }

        /// <summary>
        /// 获取或设置
        /// </summary>
        private double _grandTotal;
        /// <summary>
        /// 获取或设置
        /// </summary>
        public double GrandTotal
        {
            get { return _grandTotal; }
            set { Set<double>(ref _grandTotal, value, "GrandTotal"); }
        }
        /// <summary>
        /// 获取或设置
        /// </summary>
        private string _posNumber;
        /// <summary>
        /// 获取或设置
        /// </summary>
        public string PosNumber
        {
            get { return _posNumber; }
            set { Set<string>(ref _posNumber, value, "PosNumber"); }
        }
        /// <summary>
        /// 获取或设置
        /// </summary>
        private string _casher;
        /// <summary>
        /// 获取或设置
        /// </summary>
        public string Casher
        {
            get { return _casher; }
            set { Set<string>(ref _casher, value, "Casher"); }
        }

        #endregion

        #region 命令
        /// <summary>
        /// 获取或设置
        /// </summary>
        private ICommand _command;
        /// <summary>
        /// 获取或设置
        /// </summary>
        public ICommand Command
        {
            get
            {
                return _command ?? (_command = new RelayCommand<string>(p =>
                {
                    switch (p)
                    {
                        case "OrderNumberCopy":
                            break;
                        case "Print":
                            PrintView printView = new PrintView();
                            printView.ShowDialog();
                            break;
                        case "ProductAdd":
                            Productes.Add(new ProductItem());
                            break;
                        case "ProductDelete":
                            if (_selectedItem == null)
                            {
                                System.Windows.MessageBox.Show("请选择删除行。");
                            }
                            else
                            {
                                Productes.Remove(SelectedItem);
                            }
                            break;
                        case "ProductCopy":
                            break;
                        default:
                            break;
                    }
                }, a =>
                {
                    return true;
                }));
            }
        }

        #endregion
    }
}
