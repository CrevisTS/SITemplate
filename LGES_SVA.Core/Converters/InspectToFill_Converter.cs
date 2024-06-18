using LGES_SVA.Core.Enums;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace LGES_SVA.Core.Converters
{
	public class InspectToFill_Converter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if((EInspectionState)value == EInspectionState.Start)
			{
				return "Green";
			}
			else if((EInspectionState)value == EInspectionState.Stop)
			{
				return "Red";
			}

			return "Yellow";
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
