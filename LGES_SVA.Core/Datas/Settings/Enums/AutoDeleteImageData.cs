using Prism.Mvvm;

namespace LGES_SVA.Core.Datas.Settings.Enums
{
	public class AutoDeleteImageData : BindableBase
	{
		private bool _isEnabled;

		public bool IsEnabled { get => _isEnabled; set => SetProperty(ref _isEnabled, value); }

		public int[] ImageDeletePeriodArray = new int[] { 6, 7, 8, 9, 10, 11, 12 };

		public AutoDeleteImageData()
		{

		}
	}
}