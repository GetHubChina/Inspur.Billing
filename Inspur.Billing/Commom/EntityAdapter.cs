using DataModels;
using Inspur.TaxModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inspur.Billing.Commom
{
    public class EntityAdapter
    {
        public static TaxPayer TaxpayerJnfo2TaxPayer(TaxpayerJnfo taxpayerJnfo)
        {
            TaxPayer taxPayer = new TaxPayer
            {
                Tin = taxpayerJnfo.TaxpayerTin,
                Id = taxpayerJnfo.TaxpayerId.ToString(),
                Name = taxpayerJnfo.ShopName,
                Adress = taxpayerJnfo.TaxpayerAddress,
                Telphone = taxpayerJnfo.TaxpayerTel,
                BankAccount = taxpayerJnfo.TaxpayerAccount,
            };
            return taxPayer;
        }
        public static void TaxpayerJnfo2TaxPayer(TaxpayerJnfo source, TaxPayer target)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source can not null.");
            }
            if (target == null)
            {
                throw new ArgumentNullException("target can not null.");
            }
            target.Tin = source.TaxpayerTin;
            target.Id = source.TaxpayerId.ToString();
            target.Name = source.ShopName;
            target.Adress = source.TaxpayerAddress;
            target.Telphone = source.TaxpayerTel;
            target.BankAccount = source.TaxpayerAccount;
        }
        public static TaxpayerJnfo TaxPayer2TaxpayerJnfo(TaxPayer taxPayer)
        {
            TaxpayerJnfo taxpayerJnfo = new TaxpayerJnfo
            {
                TaxpayerTin = taxPayer.Tin,
                ShopName = taxPayer.Name,
                TaxpayerAddress = taxPayer.Adress,
                TaxpayerTel = taxPayer.Telphone,
                TaxpayerAccount = taxPayer.BankAccount,
            };
            return taxpayerJnfo;
        }
        public static void BuyerInfo2Buyer(BuyerInfo source, Buyer target)
        {
            if (source==null)
            {
                throw new ArgumentNullException("source can not null.");
            }
            if (target==null)
            {
                target = new Buyer();
            }
            target.Id = source.BuyerId.ToString();
            target.Tin = source.BuyerTin;
            target.Name = source.BuyerName;
            target.Address = source.BuyerAddress;
            target.TelPhone = source.BuyerTel;
        }
    }
}
