using CvsService.UI.Windows.UI.Units;
using System.Windows;
namespace LGES_SVA.Main.Views
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : CustomWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

		private void Minimize_Button_Click(object sender, System.Windows.RoutedEventArgs e)
		{
            WindowState = WindowState.Minimized;
		}

		private void Maximize_Button_Click(object sender, RoutedEventArgs e)
		{
            WindowState = WindowState.Maximized;

        }
    }
}
