using Prism.Mvvm;

namespace LGES_SVA.Core.Datas.Settings
{
	public class AutoDeleteImageData : BindableBase
	{
		private bool _isEnabled;
		private string _deleteCycle;

		public bool IsEnabled { get => _isEnabled; set => SetProperty(ref _isEnabled, value); }


		public AutoDeleteImageData()
		{

		}
	}
}