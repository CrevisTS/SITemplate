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
		private readonly ISettingRepository _settingRepository;

		private string _password;
		private LoginService _loginService;
		public string Title => "Login";
		public string Password { get => _password; set => SetProperty(ref _password, value); }

		public event Action<IDialogResult> RequestClose;

		public ICommand LoginBtnClickCommand => new DelegateCommand<string>(OnLoginBtnClick);

		/// <summary>
		/// View에서 Enter KeyDown
		/// </summary>
		public void EnterDown()
		{
			OnLoginBtnClick(Password);
		}

		public LoginViewModel(ISettingRepository settingRepository, LoginService loginService)
		{
			_settingRepository = settingRepository;
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

		#endregion

	}
}
