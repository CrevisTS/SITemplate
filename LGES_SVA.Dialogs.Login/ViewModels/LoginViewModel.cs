using LGES_SVA.Core.Enums;
using LGES_SVA.Core.Interfaces.Settings;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace LGES_SVA.Dialogs.Login.ViewModels
{
	public class LoginViewModel : BindableBase, IDialogAware
	{
		private readonly ISettingRepository _settingRepository;
		private string _password;

		public string Title => "Login";
		public string Password { get => _password; set => SetProperty(ref _password, value); }

		public event Action<IDialogResult> RequestClose;

		public ICommand LoginBtnClickCommand => new DelegateCommand<string>(OnLoginBtnClick);

		public LoginViewModel(ISettingRepository settingRepository)
		{
			_settingRepository = settingRepository;

		}

		private void OnLoginBtnClick(string obj)
		{

			
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
