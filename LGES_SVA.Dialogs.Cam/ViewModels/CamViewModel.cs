using CvsService.Camera.CvsGigE.Services;
using Prism.Commands;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace LGES_SVA.Dialogs.Cam.ViewModels
{
	public class CamViewModel : IDialogAware
	{
		private CvsGigEManager _cvsGigEManager;
		public CvsGigEManager CVSGigEManager { get; set; }
		public ICommand CloseCommand => new DelegateCommand(OnDialogClose);
		public CamViewModel(CvsGigEManager cvsGigEManager)
		{
			_cvsGigEManager = cvsGigEManager;
		}

		private void OnDialogClose()
		{
			Application.Current.Dispatcher.Invoke(() =>
			{
				RequestClose?.Invoke(new DialogResult(ButtonResult.OK));
			});
		}

		#region DialogAware
		public string Title => "Camera";

		public event Action<IDialogResult> RequestClose;

		public bool CanCloseDialog() => true;

		public void OnDialogClosed()
		{
		}

		public void OnDialogOpened(IDialogParameters parameters) { }

		#endregion

		

	}
}
