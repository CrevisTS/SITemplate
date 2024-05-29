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

namespace LGES_SVA.Dialogs.Simulation.ViewModels
{
	public class SimulationViewModel : IDialogAware, IDisposable
	{
		private IEventAggregator _eventAggregator;

		public ICommand CloseCommand => new DelegateCommand(OnDialogClose);

		private void OnDialogClose()
		{
			Application.Current.Dispatcher.Invoke(() =>
			{
				RequestClose?.Invoke(new DialogResult(ButtonResult.OK));
			});
		}

		public SimulationViewModel(IEventAggregator eventAggregator)
		{
			_eventAggregator = eventAggregator;
			_eventAggregator.GetEvent<LogoutEvent>().Subscribe(() => OnDialogClosed());
			_eventAggregator.GetEvent<DialogClosingEvent>().Subscribe(OnDialogClosing, ThreadOption.PublisherThread);

		}

		private void LogoutDialogClosed() => OnDialogClose();


		#region DialogAware
		public string Title => "Simulation";

		public event Action<IDialogResult> RequestClose;

		public bool CanCloseDialog() => true;
		public void OnDialogOpened(IDialogParameters parameters)
		{
		}

		private void OnDialogClosing(CancelEventArgs e)
		{
		}

		public void OnDialogClosed()
		{
			Dispose();
		}

		
		#endregion

		public void Dispose()
		{
			_eventAggregator.GetEvent<DialogClosingEvent>().Unsubscribe(OnDialogClosing);
			_eventAggregator.GetEvent<LogoutEvent>().Unsubscribe(LogoutDialogClosed);
		}
	}
}
