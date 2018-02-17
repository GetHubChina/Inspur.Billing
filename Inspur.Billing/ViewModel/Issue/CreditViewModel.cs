using DataModels;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Inspur.Billing.Commom;
using Inspur.Billing.Model;
using Inspur.Billing.View.Issue;
using Inspur.TaxModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Inspur.Billing.ViewModel.Issue
{
    public class CreditViewModel : ViewModelBase
    {
        public CreditViewModel()
        {
            ViewModelLocator locator = (ViewModelLocator)Application.Current.Resources["Locator"];
            if (locator != null)
            {
                Cashier = locator.Login.UserName;
            }
        }

        #region 属性
        /// <summary>
        /// 获取或设置开票编号
        /// </summary>
        private string _orderNumber;
        /// <summary>
        /// 获取或设置开票编号
        /// </summary>
        public string OrderNumber
        {
            get { return _orderNumber; }
            set { Set<string>(ref _orderNumber, value, "OrderNumber"); }
        }
        /// <summary>
        /// 获取或设置买方信息
        /// </summary>
        private Buyer _buyer = new Buyer();
        /// <summary>
        /// 获取或设置买方信息
        /// </summary>
        public Buyer Buyer
        {
            get { return _buyer; }
            set { Set<Buyer>(ref _buyer, value, "Buyer"); }
        }

        /// <summary>
        /// 获取或设置交易类型
        /// </summary>
        private List<CodeTable> _transactionType;
        /// <summary>
        /// 获取或设置交易类型
        /// </summary>
        public List<CodeTable> TransactionType
        {
            get { return _transactionType; }
            set { Set<List<CodeTable>>(ref _transactionType, value, "TransactionType"); }
        }
        /// <summary>
        /// 获取或设置支付类型
        /// </summary>
        private List<CodeTable> _paymentType = new List<CodeTable>
        {
            new CodeTable{ Name="Other",Code="0"},
            new CodeTable{ Name="Cash",Code="1"},
            new CodeTable{ Name="Card",Code="2"},
            new CodeTable{ Name="Check",Code="3"},
            new CodeTable{ Name="Wiretransfer",Code="4"},
            new CodeTable{ Name="Voucher",Code="5"},
            new CodeTable{ Name="MobileMoney",Code="6"}
        };
        /// <summary>
        /// 获取或设置支付类型
        /// </summary>
        public List<CodeTable> PaymentType
        {
            get { return _paymentType; }
            set { Set<List<CodeTable>>(ref _paymentType, value, "PaymentType"); }
        }
        /// <summary>
        /// 获取或设置商品集合
        /// </summary>
        private ObservableCollection<ProductItem> _productes = new ObservableCollection<ProductItem>();
        /// <summary>
        /// 获取或设置商品集合
        /// </summary>
        public ObservableCollection<ProductItem> Productes
        {
            get { return _productes; }
            set { Set<ObservableCollection<ProductItem>>(ref _productes, value, "Productes"); }
        }
        /// <summary>
        /// 获取或设置选中的商品
        /// </summary>
        private ProductItem _selectedItem;
        /// <summary>
        /// 获取或设置选中的商品
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
        /// 获取或设置收银员姓名
        /// </summary>
        private string _cashier;
        /// <summary>
        /// 获取或设置收银员姓名
        /// </summary>
        public string Cashier
        {
            get { return _cashier; }
            set { Set<string>(ref _cashier, value, "Casher"); }
        }
        /// <summary>
        /// 获取或设置数据库中的商品
        /// </summary>
        private List<GoodsInfo> _goods;
        /// <summary>
        /// 获取或设置数据库中的商品
        /// </summary>
        public List<GoodsInfo> Goods
        {
            get { return _goods; }
            set { Set<List<GoodsInfo>>(ref _goods, value, "Goods"); }
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
                        case "Loaded":
                            Goods = (from a in Const.dB.GoodsInfo
                                     select a).ToList();
                            break;
                        case "OrderNumberCopy":
                            break;
                        case "Print":
                            PrintView printView = new PrintView();
                            printView.ShowDialog();
                            break;
                        case "BuyerTinLostFocus":
                            LoadBuyerInfo();
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

        private void LoadBuyerInfo()
        {
            if (!string.IsNullOrWhiteSpace(_buyer.Tin))
            {
                var buyers = (from a in Const.dB.BuyerInfo
                              where a.BuyerTin == _buyer.Tin
                              select a).ToList();
                if (buyers != null && buyers.Count > 0)
                {
                    EntityAdapter.BuyerInfo2Buyer(buyers[0], Buyer);
                }
            }
        }

        #endregion
    }
}
