using SITemplate.Core.Enums;
using System;
using System.Globalization;
using System.Windows.Data;

namespace SITemplate.Core.Converters
{
    public class ViewTypeOpacityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is EViewType mainView)
            {
                if (parameter is EViewType myView)
                {
                    if (mainView == myView)
                    {
                        return 0.3;
                    }
                }
            }

            return 1;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException("ConvertBack method is not supported in ViewTypeOpacityConverter.");
        }
    }
}
