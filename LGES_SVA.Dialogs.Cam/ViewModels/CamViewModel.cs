using LGES_SVA.Camera.Services;
using LGES_SVA.Core.Datas;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using System.Windows;
using System.Windows.Input;

namespace LGES_SVA.Dialogs.Cam.ViewModels
{
	public class CamViewModel : BindableBase, IDialogAware
	{
		private IDialogService _dialogService;

		private CameraManager _cameraManager;

		public CameraManager CameraManager { get => _cameraManager; set => SetProperty(ref _cameraManager, value); }
		public ICommand CloseCommand => new DelegateCommand(OnDialogClose);
		public ICommand ReConnectCommand => new DelegateCommand(OnReConnect);
		public ICommand LiveCommand => new DelegateCommand(OnLive);

		
		public CamViewModel(CameraManager cameraManager , IDialogService ds)
		{
			_cameraManager = cameraManager;
			_dialogService = ds;
		}


		private void OnLive()
		{
			_dialogService.ShowDialog(DialogNames.CamLiveDialog);
		}

		private void OnReConnect()
		{
			_cameraManager.AllReConnect();
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

		public void OnDialogClosed() { }

		public void OnDialogOpened(IDialogParameters parameters) { }

		#endregion

		

	}
}
