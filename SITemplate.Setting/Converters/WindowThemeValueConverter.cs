using CvsService.Core.Converters;
using CvsService.UI.Windows.Enums;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace SITemplate.Setting.Converters
{
    internal class WindowThemeValueConverter : IValueConverter
    {
        /// <summary>
        /// Enum타입인 Language를 StringArray로 변경하여 바인딩.
        /// </summary>
        public string[] WindowThemeStrings => GetStrings();
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is EWindowTheme format)
            {
                return (int)format;
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (EWindowTheme)value;
        }

        public static string GetString(EWindowTheme value)
        {
            return EnumValueConverter.GetDescription(value);
        }

        public static string[] GetStrings()
        {
            List<string> list = new List<string>();
            foreach (EWindowTheme value in Enum.GetValues(typeof(EWindowTheme)))
            {
                list.Add(GetString(value));
            }

            return list.ToArray();
        }
    }
}
