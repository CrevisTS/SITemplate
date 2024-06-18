﻿using LGES_SVA.Camera.Services;
using LGES_SVA.Camera.Datas;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using System.Windows;
using System.Windows.Input;

namespace LGES_SVA.Dialogs.Cam.ViewModels
{
	public class LiveDialogViewModel : BindableBase,IDialogAware,IDisposable
	{
		private CameraManager _cameraManager;
		public CameraManager CameraManager { get => _cameraManager; set => SetProperty(ref _cameraManager, value); }
		
		public ICommand CloseCommand => new DelegateCommand(OnDialogClose);
		public ICommand SWTriggerCommand => new DelegateCommand(OnSWTriggerCmd);

		private void OnSWTriggerCmd()
		{
			_cameraManager.AllSWTriggerCmd();
		}

		public LiveDialogViewModel(CameraManager cameraManager)
		{
			_cameraManager = cameraManager;

			_cameraManager.AllFreeRunSet();
			_cameraManager.AllAcqStart();
		}



		#region DialogAware
		public string Title => "LiveDialog";

		public event Action<IDialogResult> RequestClose;

		public bool CanCloseDialog() => true;
		public void OnDialogClosed() { }
		private void OnDialogClose()
		{
			Dispose();


			Application.Current.Dispatcher.Invoke(() =>
			{
				RequestClose?.Invoke(new DialogResult(ButtonResult.OK));
			});
		}
		public void OnDialogOpened(IDialogParameters parameters) { }

		public void Dispose()
		{
			_cameraManager.AllAcqStop();
		}
		#endregion
	}
}