using LGES_SVA.Core.Events;
using Prism.Commands;
using Prism.Events;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace LGES_SVA.Dialogs.Live.ViewModels
{
	public class LiveViewModel : IDialogAware, IDisposable
	{
		private IEventAggregator _eventAggregator;

		public ICommand CloseCommand => new DelegateCommand(OnDialogClose);
		public LiveViewModel(IEventAggregator eventAggregator)
		{
			try
			{
				_eventAggregator = eventAggregator;
				_eventAggregator.GetEvent<LogoutEvent>().Subscribe(() => OnDialogClosed());
				_eventAggregator.GetEvent<DialogClosingEvent>().Subscribe(OnDialogClosing);
			}
			catch (Exception)
			{
				throw;
			}
		}
		private void LogoutDialogClosed() => OnDialogClose();

		private void OnDialogClose()
		{
			Application.Current.Dispatcher.Invoke(() =>
			{
				RequestClose?.Invoke(new DialogResult(ButtonResult.OK));
			});
		}

		#region DialogAware
		public string Title => "Live";
		public event Action<IDialogResult> RequestClose;
		public bool CanCloseDialog() => true;
		private void OnDialogClosing((string, CancelEventArgs) obj) { }

		public void OnDialogClosed()
		{
			Dispose();
		}

		public void OnDialogOpened(IDialogParameters parameters) { }
		#endregion
		public void Dispose()
		{
			_eventAggregator.GetEvent<DialogClosingEvent>().Unsubscribe(OnDialogClosing);
			_eventAggregator.GetEvent<LogoutEvent>().Unsubscribe(LogoutDialogClosed);
		}

	}
}
