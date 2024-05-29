using LGES_SVA.Core.Datas.Settings;
using LGES_SVA.Core.Events;
using LGES_SVA.Core.Interfaces.Settings;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace LGES_SVA.Dialogs.Setting.ViewModels
{
	public class SettingViewModel : BindableBase, IDialogAware, IDisposable
	{
		private ISettingRepository _settingRepository;
		private IEventAggregator _eventAggregator;

		private AppSetting _appSettingClone;

		public AppSetting AppSetting { get => _settingRepository.AppSetting; }
		public AppSetting AppSettingClone { get => _appSettingClone; set => SetProperty(ref _appSettingClone, value); }
		public ICommand CancelCommand => new DelegateCommand(OnDialogClose);
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

		public SettingViewModel(ISettingRepository sr, IEventAggregator ea)
		{
			_settingRepository = sr;
			_eventAggregator = ea;

			AppSettingClone = _settingRepository.AppSetting.Clone();

			_eventAggregator.GetEvent<LogoutEvent>().Subscribe(() => LogoutDialogClosed());
			_eventAggregator.GetEvent<DialogClosingEvent>().Subscribe(OnDialogClosing, ThreadOption.PublisherThread, false);

		}

		private void OnDialogClose()
		{
			Application.Current.Dispatcher.Invoke(() =>
			{
				RequestClose?.Invoke(new DialogResult(ButtonResult.Cancel));
			});
		}

		#region DialogAware
		public string Title => "Setting";

		public event Action<IDialogResult> RequestClose;

		public bool CanCloseDialog() => true;
		private void OnDialogClosing(CancelEventArgs e) { }

		private void LogoutDialogClosed()
		{
			Application.Current.Dispatcher.Invoke(() =>
			{
				RequestClose?.Invoke(new DialogResult(ButtonResult.Cancel));
			});
		}

		public void OnDialogClosed() 
		{
			Dispose();
		}

		public void OnDialogOpened(IDialogParameters parameters)
		{
		}

		public void Dispose()
		{
			_eventAggregator.GetEvent<DialogClosingEvent>().Unsubscribe(OnDialogClosing);
			_eventAggregator.GetEvent<LogoutEvent>().Unsubscribe(LogoutDialogClosed);
		}

		#endregion
	}
}
