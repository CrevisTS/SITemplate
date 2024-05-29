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
	public class IOViewModel : IDialogAware
	{

		public ICommand CloseCommand => new DelegateCommand(OnDialogClose);
		public ICommand PreviewKeyDownCommand => new DelegateCommand<KeyEventArgs>(OnPreviewKeyDownEvent);
		
		public IOViewModel()
		{

		
		}
		/// <summary>
		/// IP입력 시 숫자만 입력될 수 있게 함
		/// </summary>
		/// <param name="e"></param>
		private void OnPreviewKeyDownEvent(KeyEventArgs e)
		{
			Regexs.AcceptOnlyNumbers(e);
		}

		private void OnDialogClose()
		{
			Application.Current.Dispatcher.Invoke(() =>
			{
				RequestClose?.Invoke(new DialogResult(ButtonResult.Cancel));
			});
		}

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

	}
}
