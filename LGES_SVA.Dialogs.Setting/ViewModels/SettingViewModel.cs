using LGES_SVA.Core.Datas.Settings;
using LGES_SVA.Core.Interfaces.Settings;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using System.Windows;

namespace LGES_SVA.Dialogs.Setting.ViewModels
{
	public class SettingViewModel : BindableBase, IDialogAware
	{
		private ISettingRepository _settingRepository;
		private AppSetting _appSettingClone;

		public string Title => "Setting";
		public AppSetting AppSetting { get => _settingRepository.AppSetting; }
		public AppSetting AppSettingClone { get => _appSettingClone; set => SetProperty(ref _appSettingClone, value); }

		public event Action<IDialogResult> RequestClose;

		public SettingViewModel(ISettingRepository settingRepository)
		{
			_settingRepository = settingRepository;
			AppSettingClone = _settingRepository.AppSetting.Clone() as AppSetting;
		}

		public bool CanCloseDialog()
		{
			return true;
		}


		public void OnDialogClosed()
		{
			// 저장하시겠습니까?
			var result = MessageBox.Show("현재 설정을 저장하시겠습니까?","Save",MessageBoxButton.YesNo);
			if(result == MessageBoxResult.Yes)
			{
				_settingRepository.AppSetting = AppSettingClone;
				_settingRepository.SaveSetting();
			}
			else
			{
				//_settingRepository.LoadSetting();
			}
		}

		public void OnDialogOpened(IDialogParameters parameters)
		{
		}
	}
}
