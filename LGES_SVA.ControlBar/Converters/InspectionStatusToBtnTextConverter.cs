using LGES_SVA.Core.Enums;
using System;
using System.Globalization;
using System.Windows.Data;

namespace LGES_SVA.ControlBar.Converters
{
    class InspectionStatusToBtnTextConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is EInspectionState status))
                return null;

            switch (status)
            {
                case EInspectionState.Start:
                    return "Stop";
                case EInspectionState.Stop:
                    return "Start";
                case EInspectionState.Starting:
                    return "Starting";
                case EInspectionState.Stopping:
                    return "Stopping";
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
