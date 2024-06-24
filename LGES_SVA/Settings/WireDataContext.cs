using CvsService.Prism.Interfaces;
using LGES_SVA.Regions.ControlBar.ViewModels;
using LGES_SVA.Regions.ControlBar.Views;
using LGES_SVA.Dialogs.Recipe.ViewModels;
using LGES_SVA.Dialogs.Recipe.Views;
using LGES_SVA.Main.ViewModels;
using LGES_SVA.Main.Views;
using LGES_SVA.Splash.ViewModels;
using LGES_SVA.Splash.Views;
using Prism.Mvvm;
using LGSE_SVA.Regions.Inspection.Views;
using LGSE_SVA.Regions.Inspection.ViewModels;

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
            ViewModelLocationProvider.Register<ControlBarView, ControlBarViewModel>();
            ViewModelLocationProvider.Register<RecipeDialog, RecipeDialogViewModel>();
            ViewModelLocationProvider.Register<RecipeBasicSettingView, RecipeBasicSettingViewModel>();
            ViewModelLocationProvider.Register<RecipeLeftSettingView, RecipeLeftSettingViewModel>();
        }
    }
}
