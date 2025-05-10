using Microsoft.UI.Xaml.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartLedgerPL.Converters
{
    public class InverseBooleanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return !(value is bool b) || !b;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return !(value is bool b) || !b;
        }
    }

}
