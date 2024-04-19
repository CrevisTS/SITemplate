using CvsService.Prism.Interfaces;
using Prism.Mvvm;
using LGES_SVA.ControlBar.ViewModels;
using LGES_SVA.ControlBar.Views;
using LGES_SVA.Inspection.ViewModels;
using LGES_SVA.Inspection.Views;
using LGES_SVA.Main.ViewModels;
using LGES_SVA.Main.Views;
using LGES_SVA.Setting.ViewModels;
using LGES_SVA.Setting.Views;
using LGES_SVA.Splash.ViewModels;
using LGES_SVA.Splash.Views;

namespace LGES_SVA.Settings
{
    public class WireDataContext : IViewViewModelWire
    {
        public void WireVVM()
        {
            // TODO : View - ViewModel 연결해주는 부분.
            ViewModelLocationProvider.Register<MainWindow, MainViewModel>();
            ViewModelLocationProvider.Register<SplashWindow, SplashViewModel>();
            ViewModelLocationProvider.Register<InspectionView, InspectionViewModel>();
            ViewModelLocationProvider.Register<SettingView, SettingViewModel>();
            ViewModelLocationProvider.Register<ControlBarView, ControlBarViewModel>();
        }
    }
}
