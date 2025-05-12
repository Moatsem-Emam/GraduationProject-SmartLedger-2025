using Microsoft.UI.Xaml.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartLedgerPL.Converters
{
    public class PercentToMaxWidthConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is double width && parameter is string percentStr && double.TryParse(percentStr.TrimEnd('%'), out double percent))
            {
                return width * (percent / 100);
            }
            return 500.0; // Default max width
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
