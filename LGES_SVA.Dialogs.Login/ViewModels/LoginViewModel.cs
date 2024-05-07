using LGES_SVA.Core.Interfaces.Settings;
using LGES_SVA.Login.Services;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using System.Windows;
using System.Windows.Input;

namespace LGES_SVA.Dialogs.Login.ViewModels
{
	public class LoginViewModel : BindableBase, IDialogAware
	{
		private string _password;
		private LoginService _loginService;

		public string Password { get => _password; set => SetProperty(ref _password, value); }
		public LoginService LoginService { get => _loginService; set => SetProperty(ref _loginService, value); }

		public ICommand LoginBtnClickCommand => new DelegateCommand<string>(OnLoginBtnClick);

		/// <summary>
		/// View에서 Enter KeyDown
		/// </summary>
		public void EnterDown()
		{
			OnLoginBtnClick(Password);
		}

		public LoginViewModel(LoginService loginService)
		{
			_loginService = loginService;
		}

		private void OnLoginBtnClick(string password)
		{
			if (_loginService.Login(password))
			{
				RequestClose?.Invoke(new DialogResult(ButtonResult.OK));
			}
			else
			{
				MessageBox.Show("비밀번호가 다릅니다.");
			}
		}

		#region DialogAware
		public string Title => "Login";

		public event Action<IDialogResult> RequestClose;

		public bool CanCloseDialog()
		{
			return true;
		}

		public void OnDialogClosed()
		{
			//RequestClose?.Invoke(new DialogResult(ButtonResult.Cancel));
		}

		public void OnDialogOpened(IDialogParameters parameters)
		{
	
		}

		#endregion

	}
}
