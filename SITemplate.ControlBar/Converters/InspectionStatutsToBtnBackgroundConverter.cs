using SITemplate.Core.Enums;
using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace SITemplate.ControlBar.Converters
{
    public class InspectionStatusToBtnBackgroundConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is EInspectionState state))
                return null;

            switch (state)
            {
                case EInspectionState.Start:
                    return new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF6347"));
                case EInspectionState.Stop:
                    return new SolidColorBrush((Color)ColorConverter.ConvertFromString("#32CD32"));
                case EInspectionState.Stopping:
                case EInspectionState.Starting:
                    return new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFD700"));
                default:
                    return null;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // TwoWay 또는 OneWayToSource가 아니기 때문에 실행될 가능성 없음.
            return null;
        }
    }
}
