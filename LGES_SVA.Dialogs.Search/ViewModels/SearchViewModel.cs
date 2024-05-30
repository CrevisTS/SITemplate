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

namespace LGES_SVA.Dialogs.Search.ViewModels
{
	public class SearchViewModel : IDialogAware, IDisposable
	{
		private IEventAggregator _eventAggregator;

		/// <summary>
		/// Close 버튼 Command
		/// </summary>
		public ICommand CloseCommand => new DelegateCommand(OnDialogClose);

		public SearchViewModel(IEventAggregator eventAggregator)
		{
			_eventAggregator = eventAggregator;
			_eventAggregator.GetEvent<LogoutEvent>().Subscribe(() => LogoutDialogClosed());
			_eventAggregator.GetEvent<DialogClosingEvent>().Subscribe(OnDialogClosing, ThreadOption.PublisherThread, false);
		}

		private void LogoutDialogClosed() => OnDialogClose();

		private void OnDialogClose()
		{
			// TODO : 저장하지 않고 Close 해야함

			Application.Current.Dispatcher.Invoke(() =>
			{
				RequestClose?.Invoke(new DialogResult(ButtonResult.OK));
			});
		}

		#region DialogAware
		public string Title => "Search";
		public event Action<IDialogResult> RequestClose;
		public bool CanCloseDialog() => true;
		private void OnDialogClosing((string, CancelEventArgs) obj) {  }

		public void OnDialogClosed()
		{
			Dispose();
		}

		public void OnDialogOpened(IDialogParameters parameters)
		{
		}


		#endregion

		public void Dispose()
		{
			_eventAggregator.GetEvent<DialogClosingEvent>().Unsubscribe(OnDialogClosing);
			_eventAggregator.GetEvent<LogoutEvent>().Unsubscribe(LogoutDialogClosed);
		}

	}
}
