﻿using LGES_SVA.Core.Events;
using Prism.Events;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LGES_SVA.Dialogs.Simulation.ViewModels
{
	public class SimulationViewModel : IDialogAware
	{
		private IEventAggregator _eventAggregator;

		public SimulationViewModel(IEventAggregator eventAggregator)
		{
			_eventAggregator = eventAggregator;
			_eventAggregator.GetEvent<LogoutEvent>().Subscribe(() => OnDialogClosed());
		}

		#region DialogAware
		public string Title => "Simulation";

		public event Action<IDialogResult> RequestClose;

		public bool CanCloseDialog() => true;

		public void OnDialogClosed()
		{
			RequestClose?.Invoke(new DialogResult(ButtonResult.OK));
		}

		public void OnDialogOpened(IDialogParameters parameters)
		{
		}
		#endregion
	}
}
