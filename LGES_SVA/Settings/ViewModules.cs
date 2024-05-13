using LGES_SVA.ControlBar.Views;
using LGES_SVA.Core.Datas;
using LGES_SVA.Dialogs.Live.ViewModels;
using LGES_SVA.Dialogs.Live.Views;
using LGES_SVA.Dialogs.LogDialog.ViewModels;
using LGES_SVA.Dialogs.LogDialog.Views;
using LGES_SVA.Dialogs.Login.ViewModels;
using LGES_SVA.Dialogs.Login.Views;
using LGES_SVA.Dialogs.Recipe.ViewModels;
using LGES_SVA.Dialogs.Recipe.Views;
using LGES_SVA.Dialogs.Search.ViewModels;
using LGES_SVA.Dialogs.Search.Views;
using LGES_SVA.Dialogs.Setting.ViewModels;
using LGES_SVA.Dialogs.Setting.Views;
using LGES_SVA.Dialogs.Simulation.ViewModels;
using LGES_SVA.Dialogs.Simulation.Views;
using LGES_SVA.Dialog;
using LGES_SVA.Inspection.Views;
using LGES_SVA.Regions.TabView.Views;
using Prism.Ioc;
using Prism.Modularity;

namespace LGES_SVA.Settings
{
	public sealed class ViewModules : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            // TODO : Region영역에 사용할 UserControl View 선언 하는 부분
            containerRegistry.RegisterForNavigation<ControlBarView>(ViewNames.ControlBarView);
            containerRegistry.RegisterForNavigation<InspectionView>(ViewNames.InspectionView);
            containerRegistry.RegisterForNavigation<TabView>(ViewNames.TabView);

			containerRegistry.RegisterDialogWindow<DialogWindow>();

			// TODO : Dialog 선언하는 부분
			containerRegistry.RegisterDialog<LoginDialog, LoginViewModel>();
            containerRegistry.RegisterDialog<SimulationDialog, SimulationViewModel>();
            containerRegistry.RegisterDialog<LogDialog, LogViewModel>();
            containerRegistry.RegisterDialog<RecipeDialog, RecipeViewModel>();
            containerRegistry.RegisterDialog<SearchDialog, SearchViewModel>();
            containerRegistry.RegisterDialog<LiveDialog, LiveViewModel>();
            containerRegistry.RegisterDialog<SettingDialog, SettingViewModel>();


        }

    }
}
