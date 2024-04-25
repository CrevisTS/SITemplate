using Newtonsoft.Json;
using Prism.Mvvm;

namespace LGES_SVA.Core.Datas.Settings
{
	public class AlarmConfigurationData : BindableBase
	{
		private bool _isEnabled;
		private int _NGCount;

		public bool IsEnabled { get => _isEnabled; set => SetProperty(ref _isEnabled, value); }

		public AlarmConfigurationData()
		{

		}
	}
}