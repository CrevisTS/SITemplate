using LGES_SVA.Core.Events;
using Prism.Events;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LGES_SVA.Dialogs.Live.ViewModels
{
	public class LiveViewModel : IDialogAware
	{
		#region DialogAware
		public string Title => "Live";

		public event Action<IDialogResult> RequestClose;

		public bool CanCloseDialog()
		{
			return true;
		}

		public void OnDialogClosed()
		{
			RequestClose?.Invoke(new DialogResult(ButtonResult.OK));
		}

		public void OnDialogOpened(IDialogParameters parameters)
		{
		}
		#endregion

		private IEventAggregator _eventAggregator;

		public LiveViewModel(IEventAggregator eventAggregator)
		{
			_eventAggregator = eventAggregator;
			_eventAggregator.GetEvent<LogoutEvent>().Subscribe(() => OnDialogClosed());

		}

	}
}
