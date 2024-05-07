using LGES_SVA.Core.Enums;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace LGES_SVA.ControlBar.Converters
{
	public class SimulationIconConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			EUserLevelType userLevel = (EUserLevelType)value;

			string enable = "";

			switch (userLevel)
			{
				case EUserLevelType.None:
					enable = "false";

					break;

				case EUserLevelType.Operator:
				case EUserLevelType.Engineer:
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
			EUserLevelType userLevel = (EUserLevelType)value;

			string enable = "";

			switch (userLevel)
			{
				case EUserLevelType.None:
					enable = "false";

					break;

				case EUserLevelType.Operator:
				case EUserLevelType.Engineer:
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
			EUserLevelType userLevel = (EUserLevelType)value;

			string enable = "";

			switch (userLevel)
			{
				case EUserLevelType.None:
					enable = "false";

					break;

				case EUserLevelType.Operator:
				case EUserLevelType.Engineer:
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
			EUserLevelType userLevel = (EUserLevelType)value;

			string enable = "";

			switch (userLevel)
			{
				case EUserLevelType.None:
					enable = "false";

					break;

				case EUserLevelType.Operator:
				case EUserLevelType.Engineer:
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
			EUserLevelType userLevel = (EUserLevelType)value;

			string enable = "";

			switch (userLevel)
			{
				case EUserLevelType.None:
					enable = "false";

					break;

				case EUserLevelType.Operator:
				case EUserLevelType.Engineer:
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
			EUserLevelType userLevel = (EUserLevelType)value;

			string enable = "";

			switch (userLevel)
			{
				case EUserLevelType.None:
				case EUserLevelType.Operator:
					enable = "false";

					break;

				case EUserLevelType.Engineer:
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
