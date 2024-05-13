using LGES_SVA.Core.Enums.Login;
using System;
using System.Globalization;
using System.Windows.Data;

namespace LGES_SVA.ControlBar.Converters
{
	public class LoginIconConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			ELevel userLevel = (ELevel)value;

			string source = "";
			switch (userLevel)
			{
				case ELevel.None:
					source = $@"pack://application:,,,/LGES_SVA.ControlBar;component/Resources/Icons/Login.png";

					break;

				case ELevel.Operator:
				case ELevel.Engineer:
				case ELevel.God:
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

			ELevel userLevel = (ELevel)value;
			string text = "";

			switch (userLevel)
			{
				case ELevel.None:
					text = "Login";

					break;

				case ELevel.Operator:
				case ELevel.Engineer:
				case ELevel.God:
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
