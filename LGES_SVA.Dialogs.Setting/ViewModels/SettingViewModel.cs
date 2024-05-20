using LGES_SVA.Core.Datas.Settings;
using LGES_SVA.Core.Events;
using LGES_SVA.Core.Interfaces.Settings;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using System.Windows;
using System.Windows.Input;

namespace LGES_SVA.Dialogs.Setting.ViewModels
{
	public class SettingViewModel : BindableBase, IDialogAware
	{
		private ISettingRepository _settingRepository;
		private IEventAggregator _eventAggregator;

		private AppSetting _appSettingClone;

		public AppSetting AppSetting { get => _settingRepository.AppSetting; }
		public AppSetting AppSettingClone { get => _appSettingClone; set => SetProperty(ref _appSettingClone, value); }

		public ICommand CancelCommand => new DelegateCommand(OnCanceled);

		private void OnCanceled()
		{
			Application.Current.Dispatcher.Invoke(() =>
			{
				RequestClose?.Invoke(new DialogResult(ButtonResult.Cancel));
			});
		}

		public ICommand SaveCommand => new DelegateCommand(OnSaved);

		private void OnSaved()
		{
			// 저장하시겠습니까?
			var result = MessageBox.Show("현재 설정을 저장하시겠습니까?", "Save", MessageBoxButton.YesNo);
			if (result == MessageBoxResult.Yes)
			{
				_settingRepository.AppSetting = AppSettingClone;
				_settingRepository.SaveSetting();

				RequestClose?.Invoke(new DialogResult(ButtonResult.OK));
			}
		}

		public SettingViewModel(ISettingRepository settingRepository, IEventAggregator eventAggregator)
		{
			_settingRepository = settingRepository;
			AppSettingClone = _settingRepository.AppSetting.Clone();

			_eventAggregator = eventAggregator;
			_eventAggregator.GetEvent<LogoutEvent>().Subscribe(() => LogoutDialogClosed());
		}

		#region DialogAware
		public string Title => "Setting";

		public event Action<IDialogResult> RequestClose;

		public bool CanCloseDialog() => true;

		private void LogoutDialogClosed()
		{
			Application.Current.Dispatcher.Invoke(() =>
			{
				RequestClose?.Invoke(new DialogResult(ButtonResult.Cancel));
			});
		}

		public void OnDialogClosed()
		{
			
		}

		public void OnDialogOpened(IDialogParameters parameters)
		{
		}

		#endregion
	}
}
