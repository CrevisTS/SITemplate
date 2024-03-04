using System;
using System.Globalization;
using System.Windows.Data;

namespace SITemplate.Core.Converters
{
    public class ToStringEqualCheckConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value.ToString() == parameter.ToString();

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException("ConvertBack method is not supported in ToStringEqualCheckConverter.");
        }
    }
}
