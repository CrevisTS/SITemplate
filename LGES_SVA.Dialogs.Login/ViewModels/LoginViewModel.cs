using LGES_SVA.Core.Events;
using LGES_SVA.Core.Interfaces.Settings;
using LGES_SVA.Login.Services;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using System.ComponentModel;
using System.Security;
using System.Windows;
using System.Windows.Input;

namespace LGES_SVA.Dialogs.Login.ViewModels
{
	public class LoginViewModel : BindableBase, IDialogAware
	{
		private string _password;
		private string _id;

		private LoginService _loginService;
		private IEventAggregator _eventAggregator;

		public string ID { get => _id; set => SetProperty(ref _id, value); }
		public string Password { private get => _password; set => SetProperty(ref _password, value); }

		public LoginService LoginService { get => _loginService; set => SetProperty(ref _loginService, value); }
		public ICommand LoginBtnClickCommand => new DelegateCommand(OnLoginBtnClick);

		/// <summary>
		/// View에서 Enter KeyDown
		/// </summary>
		public void EnterDown()
		{
			OnLoginBtnClick();
		}

		public LoginViewModel(LoginService loginService, IEventAggregator ea)
		{
			_loginService = loginService;
			_eventAggregator = ea;
			_eventAggregator.GetEvent<DialogClosingEvent>().Subscribe(OnDialogClosing, ThreadOption.PublisherThread, false, (filter) => filter.Item1.Equals("LoginDialog"));
		}

		private void OnLoginBtnClick()
		{
			if (_loginService.Login(ID, Password))
			{
				RequestClose?.Invoke(new DialogResult(ButtonResult.OK));
			}
			else
			{
				MessageBox.Show("비밀번호가 다릅니다.");
				Password = "";
			}
		}

		#region DialogAware
		public string Title => "Login";

		public event Action<IDialogResult> RequestClose;

		public bool CanCloseDialog()
		{
			return true;
		}
		private void OnDialogClosing((string, CancelEventArgs) obj)
		{
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
