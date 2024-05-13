using LGES_SVA.Core.Events;
using Prism.Events;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace LGES_SVA.Dialogs.Live.ViewModels
{
	public class LiveViewModel : IDialogAware
	{
		#region DialogAware
		public string Title => "Live";

		public event Action<IDialogResult> RequestClose;

		public bool CanCloseDialog() => true;

		private void OnDialogClosing((string, CancelEventArgs) obj)
		{
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

		public LiveViewModel(IEventAggregator eventAggregator)
		{
			try
			{
				_eventAggregator = eventAggregator;
				_eventAggregator.GetEvent<LogoutEvent>().Subscribe(() => OnDialogClosed());
				_eventAggregator.GetEvent<DialogClosingEvent>().Subscribe(OnDialogClosing, ThreadOption.PublisherThread, false, (filter) => filter.Item1.Equals("LiveDialog"));
			}
			catch (Exception)
			{
				throw;
			}
		}

		
	}
}
