﻿using LGES_SVA.Regions.ControlBar.Views;
using LGES_SVA.Core.Datas;
using LGES_SVA.Dialog;
using LGES_SVA.Dialogs.Cam.ViewModels;
using LGES_SVA.Dialogs.Cam.Views;
using LGES_SVA.Dialogs.IO.ViewModels;
using LGES_SVA.Dialogs.IO.Views;
using LGES_SVA.Dialogs.Light.ViewModels;
using LGES_SVA.Dialogs.Light.Views;
using LGES_SVA.Dialogs.LogDialog.ViewModels;
using LGES_SVA.Dialogs.LogDialog.Views;
using LGES_SVA.Dialogs.Login.ViewModels;
using LGES_SVA.Dialogs.Login.Views;
using LGES_SVA.Dialogs.PLC.ViewModels;
using LGES_SVA.Dialogs.PLC.Views;
using LGES_SVA.Dialogs.Recipe.ViewModels;
using LGES_SVA.Dialogs.Recipe.Views;
using LGES_SVA.Dialogs.Search.ViewModels;
using LGES_SVA.Dialogs.Search.Views;
using LGES_SVA.Dialogs.Setting.ViewModels;
using LGES_SVA.Dialogs.Setting.Views;
using LGES_SVA.Dialogs.Simulation.ViewModels;
using LGES_SVA.Dialogs.Simulation.Views;
using LGES_SVA.Regions.TabView.Views;
using LGES_SVA.Regions.Yield.Views;
using LGSE_SVA.Regions.Inspection.Views;
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
            containerRegistry.RegisterForNavigation<TabGraphView>(ViewNames.TabGraphView);
            containerRegistry.RegisterForNavigation<RecipeBasicSettingView>(ViewNames.RecipeBasicSettingView);
            containerRegistry.RegisterForNavigation<RecipeLeftSettingView>(ViewNames.RecipeLeftSettingView);
            containerRegistry.RegisterForNavigation<YieldView>(ViewNames.YieldView);

            containerRegistry.RegisterDialogWindow<DialogWindow>();

			// TODO : Dialog 선언하는 부분
			containerRegistry.RegisterDialog<LoginDialog, LoginViewModel>();
            containerRegistry.RegisterDialog<SimulationDialog, SimulationViewModel>();
            containerRegistry.RegisterDialog<LogDialog, LogViewModel>();
            containerRegistry.RegisterDialog<RecipeDialog, RecipeDialogViewModel>();

            containerRegistry.RegisterDialog<SearchDialog, SearchViewModel>();
            containerRegistry.RegisterDialog<LiveDialog, LiveDialogViewModel>(DialogNames.CamLiveDialog);
            containerRegistry.RegisterDialog<SettingDialog, SettingViewModel>();
            
            containerRegistry.RegisterDialog<PLCDialog, PLCViewModel>();
            containerRegistry.RegisterDialog<IODialog, IOViewModel>();
            containerRegistry.RegisterDialog<LightDialog, LightViewModel>();
            containerRegistry.RegisterDialog<CamDialog, CamViewModel>();

            containerRegistry.RegisterDialog<NewRecipeDialog, NewRecipeDialogViewModel>();


        }

    }
}
