using Prism.Commands;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace LGES_SVA.Dialogs.LogDialog.ViewModels
{
	public class LogViewModel : IDialogAware
	{
		public string Title => "Log";

		public event Action<IDialogResult> RequestClose;

		public ICommand SystemLogCommand => new DelegateCommand(ShowSystemLog);

		private void ShowSystemLog()
		{
			string folderPath = @"D:\DAT";

			// 폴더 열기
			Process.Start(folderPath);
		}

		public LogViewModel()
		{

		}

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
