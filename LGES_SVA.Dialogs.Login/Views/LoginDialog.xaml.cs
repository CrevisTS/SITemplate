using CvsService.UI.Windows.UI.Units;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LGES_SVA.Dialogs.Login.Views
{
	/// <summary>
	/// LoginDialog.xaml에 대한 상호 작용 논리
	/// </summary>
	public partial class LoginDialog : UserControl
	{
		public LoginDialog()
		{
			InitializeComponent();
			// 테마 Merge
			CustomWindow.MergeThemeToUserControl(this, CvsService.UI.Windows.Enums.EWindowTheme.LGES);

			ID_TextBox.Focus();
		}

	}
}
