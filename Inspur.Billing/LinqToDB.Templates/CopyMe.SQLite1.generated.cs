//---------------------------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated by T4Model template for T4 (https://github.com/linq2db/t4models).
//    Changes to this file may cause incorrect behavior and will be lost if the code is regenerated.
// </auto-generated>
//---------------------------------------------------------------------------------------------------
using System;
using System.Linq;

using LinqToDB;
using LinqToDB.Mapping;

namespace DataModels
{
    /// <summary>
    /// Database       : Billing
    /// Data Source    : Billing
    /// Server Version : 3.14.2
    /// </summary>
    public partial class BillingDB : LinqToDB.Data.DataConnection
    {
        public ITable<BuyerInfo> BuyerInfo { get { return this.GetTable<BuyerInfo>(); } }
        public ITable<Cashier> Cashiers { get { return this.GetTable<Cashier>(); } }
        public ITable<CodeTaxtype> CodeTaxtype { get { return this.GetTable<CodeTaxtype>(); } }
        public ITable<GoodsInfo> GoodsInfo { get { return this.GetTable<GoodsInfo>(); } }
        public ITable<GoodsTaxtype> GoodsTaxtype { get { return this.GetTable<GoodsTaxtype>(); } }
        public ITable<InvoiceAbbreviation> InvoiceAbbreviation { get { return this.GetTable<InvoiceAbbreviation>(); } }
        public ITable<InvoiceItems> InvoiceItems { get { return this.GetTable<InvoiceItems>(); } }
        public ITable<InvoiceTax> InvoiceTax { get { return this.GetTable<InvoiceTax>(); } }
        public ITable<PosInfo> PosInfo { get { return this.GetTable<PosInfo>(); } }
        public ITable<SdcInfo> SdcInfo { get { return this.GetTable<SdcInfo>(); } }
        public ITable<SystemStatu> SystemStatu { get { return this.GetTable<SystemStatu>(); } }
        public ITable<TaxpayerJnfo> TaxpayerJnfo { get { return this.GetTable<TaxpayerJnfo>(); } }

        public BillingDB()
        {
            InitDataContext();
        }

        public BillingDB(string configuration)
            : base(configuration)
        {
            InitDataContext();
        }

        partial void InitDataContext();
    }

    [Table("buyer_info")]
    public partial class BuyerInfo
    {
        [Column("buyer_id"), PrimaryKey, Identity] public long BuyerId { get; set; } // integer
        [Column("buyer_tin"), Nullable] public string BuyerTin { get; set; } // varchar(100)
        [Column("buyer_name"), Nullable] public string BuyerName { get; set; } // varchar(100)
        [Column("buyer_address"), Nullable] public string BuyerAddress { get; set; } // varchar(200)
        [Column("buyer_tel"), Nullable] public string BuyerTel { get; set; } // varchar(100)
        [Column("registrant"), Nullable] public string Registrant { get; set; } // varchar(20)
        [Column("date"), Nullable] public DateTime? Date { get; set; } // date
        [Column("editor"), Nullable] public string Editor { get; set; } // varchar(20)
        [Column("edate"), Nullable] public DateTime? Edate { get; set; } // date
        [Column("valid"), Nullable] public char? Valid { get; set; } // varchar(1)
    }

    [Table("cashier")]
    public partial class Cashier
    {
        [Column("cashier_id"), PrimaryKey, NotNull] public long CashierId { get; set; } // integer
        [Column("name"), Nullable] public string Name { get; set; } // varchar(20)
        [Column("password"), Nullable] public string Password { get; set; } // varchar(20)
        [Column("registrant"), Nullable] public string Registrant { get; set; } // varchar(20)
        [Column("rdate"), Nullable] public DateTime? Rdate { get; set; } // date
        [Column("editor"), Nullable] public string Editor { get; set; } // varchar(20)
        [Column("edate"), Nullable] public DateTime? Edate { get; set; } // date
    }

    [Table("code_taxtype")]
    public partial class CodeTaxtype
    {
        [Column("taxtype_id"), PrimaryKey, NotNull] public long TaxtypeId { get; set; } // integer
        [Column("tax type_name"), Nullable] public string TaxTypeName { get; set; } // varchar(100)
        [Column("tax_type_code"), Nullable] public string TaxTypeCode { get; set; } // varchar(10)
        [Column("tax_item_name"), Nullable] public string TaxItemName { get; set; } // varchar(100)
        [Column("tax_item_code"), Nullable] public string TaxItemCode { get; set; } // varchar(10)
        [Column("tax_item_lable"), Nullable] public string TaxItemLable { get; set; } // varchar(10)
        [Column("tax_rate"), Nullable] public double? TaxRate { get; set; } // double
        [Column("calculation_mode"), Nullable] public string CalculationMode { get; set; } // varchar(1)
        [Column("fixed_tax_amount"), Nullable] public double? FixedTaxAmount { get; set; } // varchar(1)
        [Column("registrant"), Nullable] public string Registrant { get; set; } // varchar(20)
        [Column("rdate"), Nullable] public DateTime? Rdate { get; set; } // date
        [Column("editor"), Nullable] public string Editor { get; set; } // varchar(20)
        [Column("edate"), Nullable] public DateTime? Edate { get; set; } // date
        [Column("valid"), Nullable] public char? Valid { get; set; } // varchar(1)
        [Column("effect _date"), Nullable] public DateTime? EffectDate { get; set; } // date
        [Column("expire_date"), Nullable] public DateTime? ExpireDate { get; set; } // date
    }

    [Table("goods_info")]
    public partial class GoodsInfo
    {
        [Column("goods_id"), PrimaryKey, Identity] public long GoodsId { get; set; } // integer
        [Column("barcode"), Nullable] public string Barcode { get; set; } // varchar(100)
        [Column("description"), Nullable] public string Description { get; set; } // varchar(100)
        [Column("tax_inclusive"), Nullable] public string TaxInclusive { get; set; } // integer
        [Column("price"), Nullable] public double? Price { get; set; } // double
        [Column("quantity"), Nullable] public long? Quantity { get; set; } // integer
        [Column("registrant"), Nullable] public string Registrant { get; set; } // varchar(20)
        [Column("rdate"), Nullable] public DateTime? Rdate { get; set; } // date
        [Column("editor"), Nullable] public string Editor { get; set; } // varchar(20)
        [Column("edate"), Nullable] public DateTime? Edate { get; set; } // date
        [Column("valid"), Nullable] public char? Valid { get; set; } // varchar(1)
    }

    [Table("goods_taxtype")]
    public partial class GoodsTaxtype
    {
        [Column("goods_id"), PrimaryKey(0), NotNull] public long GoodsId { get; set; } // integer
        [Column("taxtype_id"), PrimaryKey(1), NotNull] public long TaxtypeId { get; set; } // integer
        [Column("registrant"), Nullable] public string Registrant { get; set; } // varchar(20)
        [Column("rdate"), Nullable] public DateTime? Rdate { get; set; } // date
        [Column("editor"), Nullable] public string Editor { get; set; } // varchar(20)
        [Column("edate"), Nullable] public DateTime? Edate { get; set; } // date
        [Column("valid"), Nullable] public char? Valid { get; set; } // varchar(1)
    }

    [Table("invoice_abbreviation")]
    public partial class InvoiceAbbreviation
    {
        [Column("cashier_id"), Nullable] public long CashierId { get; set; } // integer
        [Column("salesorder_num"), PrimaryKey, NotNull] public long SalesorderNum { get; set; } // integer
        [Column("payment_type"), Nullable] public string PaymentType { get; set; } // varchar(2)
        [Column("taxpayer_tin"), Nullable] public string TaxpayerTin { get; set; } // varchar(20)
        [Column("taxpayer_name"), Nullable] public string TaxpayerName { get; set; } // varchar(200)
        [Column("taxpayer_location"), Nullable] public string TaxpayerLocation { get; set; } // varchar(200)
        [Column("taxpayer_address"), Nullable] public string TaxpayerAddress { get; set; } // varchar(200)
        [Column("taxpayer_distrit"), Nullable] public string TaxpayerDistrit { get; set; } // varchar(200)
        [Column("buyer_tin"), Nullable] public string BuyerTin { get; set; } // varchar(20)
        [Column("buyer_name"), Nullable] public string BuyerName { get; set; } // varchar(200)
        [Column("invoice_number"), Nullable] public string InvoiceNumber { get; set; } // varchar(20)
        [Column("total_tax_amount"), Nullable] public double? TotalTaxAmount { get; set; } // double
        [Column("total_amount"), Nullable] public double? TotalAmount { get; set; } // double
        [Column("qrcode_path"), Nullable] public string QrcodePath { get; set; } // varchar(200)
        [Column("verification_url"), Nullable] public string VerificationUrl { get; set; } // varchar(1000)
        [Column("issue_date"), Nullable] public DateTime? IssueDate { get; set; } // date
        [Column("hash_code"), Nullable] public string HashCode { get; set; } // varchar(100)
        [Column("tender amount"), Nullable] public double? TenderAmount { get; set; } // double
        [Column("change"), Nullable] public double? Change { get; set; } // double
    }

    [Table("invoice_items")]
    public partial class InvoiceItems
    {
        [Column("taxtype_id"), PrimaryKey, NotNull] public long TaxtypeId { get; set; } // integer
        [Column("goods_id"), Nullable] public long? GoodsId { get; set; } // integer
        [Column("salesorder_num"), NotNull] public long SalesorderNum { get; set; } // integer
        [Column("sn"), NotNull] public long? Sn { get; set; } // integer
        [Column("goods_gin"), Nullable] public string GoodsGin { get; set; } // varchar(20)
        [Column("goods_desc"), Nullable] public string GoodsDesc { get; set; } // varchar(100)
        [Column("goods_qty"), Nullable] public double? GoodsQty { get; set; } // double
        [Column("goods_price"), Nullable] public double? GoodsPrice { get; set; } // double
        [Column("total_amount"), Nullable] public double? TotalAmount { get; set; } // double
        [Column("tax_item"), Nullable] public string TaxItem { get; set; } // char(10)
        [Column("tax_rate"), Nullable] public double? TaxRate { get; set; } // double
        [Column("tax_amount"), Nullable] public double? TaxAmount { get; set; } // double
    }

    [Table("invoice_tax")]
    public partial class InvoiceTax
    {
        [Column("invoice_number"), Nullable] public string InvoiceNumber { get; set; } // varchar(50)
        [Column("tax_item_code"), Nullable] public string TaxItemCode { get; set; } // varchar(10)
        [Column("tax_item_desc"), Nullable] public string TaxItemDesc { get; set; } // varchar(100)
        [Column("tax_rate"), Nullable] public double TaxRate { get; set; } // double precision
        [Column("tax_amount"), Nullable] public double TaxAmount { get; set; } // double precision
    }

    [Table("pos_info")]
    public partial class PosInfo
    {
        [Column("id"), PrimaryKey, NotNull] public long Id { get; set; } // integer
        [Column("version"), Nullable] public string Version { get; set; } // varchar(100)
        [Column("issue_date"), Nullable] public DateTime? IssueDate { get; set; } // date
        [Column("desc"), Nullable] public string Desc { get; set; } // varchar(1000)
        [Column("company_name"), Nullable] public string CompanyName { get; set; } // varchar(100)
        [Column("company_address"), Nullable] public string CompanyAddress { get; set; } // varchar(100)
        [Column("company_url"), Nullable] public string CompanyUrl { get; set; } // varchar(100)
        [Column("company_tel"), Nullable] public string CompanyTel { get; set; } // varchar(100)
        [Column("company_email"), Nullable] public string CompanyEmail { get; set; } // varchar(100)
        [Column("rdate"), Nullable] public DateTime? Rdate { get; set; } // date
    }

    [Table("sdc_info")]
    public partial class SdcInfo
    {
        [Column("sdc_id"), PrimaryKey, NotNull] public long SdcId { get; set; } // integer
        [Column("sdc_ip"), Nullable] public string SdcIp { get; set; } // varchar(20)
        [Column("sdc_port"), Nullable] public string SdcPort { get; set; } // varchar(10)
        [Column("registrant"), Nullable] public string Registrant { get; set; } // varchar(20)
        [Column("date"), Nullable] public DateTime? Date { get; set; } // date
        [Column("editor"), Nullable] public string Editor { get; set; } // varchar(20)
        [Column("edate"), Nullable] public DateTime? Edate { get; set; } // date
        [Column("valid"), Nullable] public char? Valid { get; set; } // varchar(1)
    }

    [Table("system_statu")]
    public partial class SystemStatu
    {
        [Column("code"), PrimaryKey, NotNull] public string Code { get; set; } // varchar(10)
        [Column("name"), Nullable] public string Name { get; set; } // varchar(50)
        [Column("type"), Nullable] public char? Type { get; set; } // varchar(1)
        [Column("desc"), Nullable] public string Desc { get; set; } // varchar(100)
    }

    [Table("taxpayer_jnfo")]
    public partial class TaxpayerJnfo
    {
        [Column("taxpayer_id"), PrimaryKey, Identity] public long TaxpayerId { get; set; } // integer
        [Column("taxpayer_tin"), Nullable] public string TaxpayerTin { get; set; } // varchar(100)
        [Column("taxpayer_ name"), Nullable] public string TaxpayerName { get; set; } // varchar(100)
        [Column("shop_name"), Nullable] public string ShopName { get; set; } // varchar(100)
        [Column("taxpayer_address"), Nullable] public string TaxpayerAddress { get; set; } // varchar(100)
        [Column("taxpayer_tel"), Nullable] public string TaxpayerTel { get; set; } // varchar(100)
        [Column("taxpayer_state"), Nullable] public string TaxpayerState { get; set; } // varchar(100)
        [Column("taxpayer_country"), Nullable] public string TaxpayerCountry { get; set; } // varchar(20)
        [Column("rdate"), Nullable] public DateTime? Rdate { get; set; } // date
        [Column("registrant"), Nullable] public string Registrant { get; set; } // varchar(20)
        [Column("editor"), Nullable] public string Editor { get; set; } // varchar(20)
        [Column("edate"), Nullable] public DateTime? Edate { get; set; } // date
        [Column("logo_path"), Nullable] public string LogoPath { get; set; } // varchar(200)
        [Column("valid"), Nullable] public char? Valid { get; set; } // varchar(1)
        [Column("taxpayer_account"), Nullable] public string TaxpayerAccount { get; set; } // varchar(20)
    }

    public static partial class TableExtensions
    {
        public static BuyerInfo Find(this ITable<BuyerInfo> table, long BuyerId)
        {
            return table.FirstOrDefault(t =>
                t.BuyerId == BuyerId);
        }

        public static Cashier Find(this ITable<Cashier> table, long CashierId)
        {
            return table.FirstOrDefault(t =>
                t.CashierId == CashierId);
        }

        public static CodeTaxtype Find(this ITable<CodeTaxtype> table, long TaxtypeId)
        {
            return table.FirstOrDefault(t =>
                t.TaxtypeId == TaxtypeId);
        }

        public static GoodsInfo Find(this ITable<GoodsInfo> table, long GoodsId)
        {
            return table.FirstOrDefault(t =>
                t.GoodsId == GoodsId);
        }

        public static GoodsTaxtype Find(this ITable<GoodsTaxtype> table, long GoodsId, long TaxtypeId)
        {
            return table.FirstOrDefault(t =>
                t.GoodsId == GoodsId &&
                t.TaxtypeId == TaxtypeId);
        }

        public static InvoiceAbbreviation Find(this ITable<InvoiceAbbreviation> table, long SalesorderNum)
        {
            return table.FirstOrDefault(t =>
                t.SalesorderNum == SalesorderNum);
        }

        public static InvoiceItems Find(this ITable<InvoiceItems> table, long TaxtypeId)
        {
            return table.FirstOrDefault(t =>
                t.TaxtypeId == TaxtypeId);
        }

        public static PosInfo Find(this ITable<PosInfo> table, long Id)
        {
            return table.FirstOrDefault(t =>
                t.Id == Id);
        }

        public static SdcInfo Find(this ITable<SdcInfo> table, long SdcId)
        {
            return table.FirstOrDefault(t =>
                t.SdcId == SdcId);
        }

        public static SystemStatu Find(this ITable<SystemStatu> table, string Code)
        {
            return table.FirstOrDefault(t =>
                t.Code == Code);
        }

        public static TaxpayerJnfo Find(this ITable<TaxpayerJnfo> table, long TaxpayerId)
        {
            return table.FirstOrDefault(t =>
                t.TaxpayerId == TaxpayerId);
        }
    }
}
