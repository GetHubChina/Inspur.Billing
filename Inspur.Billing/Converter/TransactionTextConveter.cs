using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inspur.Billing.Converter
{
    public class TransactionTextConveter : System.Windows.Data.IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string result = "";
            if (value != null)
            {
                switch (value.ToString())
                {
                    case "0":
                        result = "Normal";
                        break;
                    case "1":
                        result = "Credit";
                        break;
                    case "4":
                        result = "Testing";
                        break;
                    case "5":
                        result = "Seperate";
                        break;
                    default:
                        break;
                }
            }
            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
