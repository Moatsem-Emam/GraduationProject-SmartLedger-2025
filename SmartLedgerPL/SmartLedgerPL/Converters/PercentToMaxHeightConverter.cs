using Microsoft.UI.Xaml.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartLedgerPL.Converters
{
    public class PercentToMaxHeightConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is double height && parameter is string percentStr && double.TryParse(percentStr.TrimEnd('%'), out double percent))
            {
                return height * (percent / 100);
            }
            return 650.0; // Default max height
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
