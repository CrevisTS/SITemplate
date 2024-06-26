﻿using LGES_SVA.Core.Events;
using Prism.Commands;
using Prism.Events;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace LGES_SVA.Dialogs.LogDialog.ViewModels
{
	public class LogViewModel : IDialogAware, IDisposable
	{
		#region DialogAware
		public string Title => "Log";

		public event Action<IDialogResult> RequestClose;

		public bool CanCloseDialog()
		{
			return true;
		}

		public void OnDialogClosed()
		{
			Application.Current.Dispatcher.Invoke(() =>
			{
				RequestClose?.Invoke(new DialogResult(ButtonResult.Cancel));
			});
		}

		public void OnDialogOpened(IDialogParameters parameters)
		{
		}
		#endregion

		private IEventAggregator _eventAggregator;

		public ICommand SystemLogCommand => new DelegateCommand(ShowSystemLog);

		private void ShowSystemLog()
		{
			string folderPath = @"D:\DAT";

			// 폴더 열기
			Process.Start(folderPath);
		}

		public LogViewModel(IEventAggregator eventAggregator)
		{
			_eventAggregator = eventAggregator;
			_eventAggregator.GetEvent<LogoutEvent>().Subscribe(OnDialogClosed);
			_eventAggregator.GetEvent<DialogClosingEvent>().Subscribe(OnDialogClosing);


		}


		private void OnDialogClosing((string, CancelEventArgs) obj) { }

		public void Dispose()
		{
			_eventAggregator.GetEvent<DialogClosingEvent>().Unsubscribe(OnDialogClosing);
			_eventAggregator.GetEvent<LogoutEvent>().Unsubscribe(LogoutDialogClosed);
		}

		private void LogoutDialogClosed() { }
	}
}
