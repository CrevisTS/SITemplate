using LGES_SVA.Core.Enums.Login;
using System;
using System.Globalization;
using System.Windows.Data;

namespace LGES_SVA.ControlBar.Converters
{
	public class SimulationIconConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			ELevel userLevel = (ELevel)value;

			string enable = "";

			switch (userLevel)
			{
				case ELevel.None:
					enable = "false";

					break;

				case ELevel.Operator:
				case ELevel.Engineer:
				case ELevel.God:
					enable = "true";

					break;
			}

			return enable;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}

	public class LogIconConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			ELevel userLevel = (ELevel)value;

			string enable = "";

			switch (userLevel)
			{
				case ELevel.None:
					enable = "false";

					break;

				case ELevel.Operator:
				case ELevel.Engineer:
				case ELevel.God:

					enable = "true";

					break;
			}

			return enable;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}

	public class RecipeIconConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			ELevel userLevel = (ELevel)value;

			string enable = "";

			switch (userLevel)
			{
				case ELevel.None:
					enable = "false";

					break;

				case ELevel.Operator:
				case ELevel.Engineer:
				case ELevel.God:

					enable = "true";

					break;
			}

			return enable;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}

	public class SearchIconConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			ELevel userLevel = (ELevel)value;

			string enable = "";

			switch (userLevel)
			{
				case ELevel.None:
					enable = "false";

					break;

				case ELevel.Operator:
				case ELevel.Engineer:
				case ELevel.God:

					enable = "true";

					break;
			}

			return enable;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}

	public class LiveIconConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			ELevel userLevel = (ELevel)value;

			string enable = "";

			switch (userLevel)
			{
				case ELevel.None:
					enable = "false";

					break;

				case ELevel.Operator:
				case ELevel.Engineer:
				case ELevel.God:

					enable = "true";

					break;
			}

			return enable;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}

	public class SettingIconConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			ELevel userLevel = (ELevel)value;

			string enable = "";

			switch (userLevel)
			{
				case ELevel.None:
				case ELevel.Operator:
					enable = "false";

					break;

				case ELevel.Engineer:
				case ELevel.God:

					enable = "true";

					break;
			}

			return enable;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
