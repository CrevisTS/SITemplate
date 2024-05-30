using Prism.Commands;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace LGES_SVA.Dialogs.Recipe.ViewModels
{
	public class NewRecipeDialogViewModel : IDialogAware
	{
		private IDialogParameters _parameters;
		public ICommand CancelBtnCommand => new DelegateCommand<string>(OnDialogClose);
		public ICommand ConfirmBtnCommand => new DelegateCommand<string>(OnDialogClose);

		private void OnDialogClose(string name)
		{
			if(name == "")
			{
				MessageBox.Show("Wirte Model Name");
				return;
			}

			_parameters.Add("Name", name);
			RequestClose?.Invoke(new DialogResult(ButtonResult.OK, _parameters));
		}

		public string Title => "New Recipe";

		public event Action<IDialogResult> RequestClose;

		public bool CanCloseDialog() => true;

		public void OnDialogClosed()
		{
		}

		public void OnDialogOpened(IDialogParameters parameters)
		{
			_parameters = parameters;
		}
	}
}
