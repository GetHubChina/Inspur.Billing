using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inspur.TaxModel
{
    /// <summary>
    /// 模块编号：实体类
    /// 作用：商品条目实体
    /// 作者：丁纪名
    /// 编写日期：2018-01-22
    /// </summary>
    public class ProductItem : ViewModelBase
    {
        /// <summary>
        /// 条目序列号
        /// </summary>
        public string No { get; set; }
        /// <summary>
        /// 获取或设置税种税目代码
        /// </summary>
        private string _categoryCode;
        /// <summary>
        /// 获取或设置税种税目代码
        /// </summary>
        public string CategroyCode
        {
            get { return _categoryCode; }
            set { Set<string>(ref _categoryCode, value, "CategroyCode"); }
        }
        /// <summary>
        /// 税种税目名称
        /// </summary>
        public string CategroyName { get; set; }
        /// <summary>
        /// 商品名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 条形码
        /// </summary>
        public string BarCode { get; set; }
        /// <summary>
        /// 获取或设置商品数量
        /// </summary>
        private double _count;
        /// <summary>
        /// 获取或设置商品数量
        /// </summary>
        public double Count
        {
            get { return _count; }
            set { Set<double>(ref _count, value, "Count"); }
        }
        /// <summary>
        /// 获取或设置支付金额
        /// </summary>
        private double _amount;
        /// <summary>
        /// 获取或设置支付金额
        /// </summary>
        public double Amount
        {
            get { return _amount; }
            set { Set<double>(ref _amount, value, "Amount"); }
        }
        /// <summary>
        /// 获取或设置商品折扣金额
        /// </summary>
        private string _discount;
        /// <summary>
        /// 获取或设置商品折扣金额
        /// </summary>
        public string Discount
        {
            get { return _discount; }
            set { Set<string>(ref _discount, value, "Discount"); }
        }
        /// <summary>
        /// 获取或设置商品单价
        /// </summary>
        private double _price;
        /// <summary>
        /// 获取或设置商品单价
        /// </summary>
        public double Price
        {
            get { return _price; }
            set { Set<double>(ref _price, value, "Price"); }
        }
        /// <summary>
        /// 获取或设置税率
        /// </summary>
        private string _rate;
        /// <summary>
        /// 获取或设置税率
        /// </summary>
        public string Rate
        {
            get { return _rate; }
            set { Set<string>(ref _rate, value, "Rate"); }
        }
    }
}
