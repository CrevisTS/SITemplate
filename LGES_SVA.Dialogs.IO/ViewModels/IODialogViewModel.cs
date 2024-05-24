using LGES_SVA.Core.Utils;
using Prism.Commands;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace LGES_SVA.Dialogs.IO.ViewModels
{
	public class IODialogViewModel : IDialogAware
	{
		#region DialogAware
		public string Title => "IO";

		public event Action<IDialogResult> RequestClose;

		public bool CanCloseDialog() => true;

		public void OnDialogClosed()
		{
		}

		public void OnDialogOpened(IDialogParameters parameters)
		{
		}

		#endregion

		public ICommand CancelCommand => new DelegateCommand(OnCanceled);

		public ICommand PreviewKeyDownCommand => new DelegateCommand<KeyEventArgs>(OnPreviewKeyDownEvent);

		private void OnPreviewKeyDownEvent(KeyEventArgs e)
		{
			Regexs.AcceptOnlyNumbers(e);
		}

		public IODialogViewModel()
		{

		}

		private void OnCanceled()
		{
			Application.Current.Dispatcher.Invoke(() =>
			{
				RequestClose?.Invoke(new DialogResult(ButtonResult.Cancel));
			});
		}
	}
}
