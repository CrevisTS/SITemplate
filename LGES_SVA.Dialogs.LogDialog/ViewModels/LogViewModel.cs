﻿using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LGES_SVA.Dialogs.LogDialog.ViewModels
{
	public class LogViewModel : IDialogAware
	{
		public string Title => "Log";

		public event Action<IDialogResult> RequestClose;

		public bool CanCloseDialog()
		{
			return true;
		}

		public void OnDialogClosed()
		{
		}

		public void OnDialogOpened(IDialogParameters parameters)
		{
		}
	}
}