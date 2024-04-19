using System;
using System.Globalization;
using System.Windows.Data;

namespace LGES_SVA.Core.Converters
{
    public class ToStringEqualToOpacityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value.ToString() == parameter.ToString() ? 0.3 : 1;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException("ConvertBack method is not supported in ToStringEqualToOpacityConverter.");
        }
    }
}
