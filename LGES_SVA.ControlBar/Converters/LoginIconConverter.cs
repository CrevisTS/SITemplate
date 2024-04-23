using LGES_SVA.Core.Enums;
using System;
using System.Globalization;
using System.Windows.Data;

namespace LGES_SVA.ControlBar.Converters
{
	public class LoginIconConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			EUserLevelType userLevel = (EUserLevelType)value;

			string source = "";
			switch (userLevel)
			{
				case EUserLevelType.None:
					source = $@"pack://application:,,,/LGES_SVA.ControlBar;component/Resources/Icons/Login.png";

					break;

				case EUserLevelType.Operator:
				case EUserLevelType.Engineer:
					source = $@"pack://application:,,,/LGES_SVA.ControlBar;component/Resources/Icons/Login.png";

					break;
			}

			return source;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}

	public class LoginDescriptionConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{

			EUserLevelType userLevel = (EUserLevelType)value;
			string text = "";

			switch (userLevel)
			{
				case EUserLevelType.None:
					text = "Login";

					break;

				case EUserLevelType.Operator:
				case EUserLevelType.Engineer:
					text = "Logout";

					break;

			}
			return text;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}

}
