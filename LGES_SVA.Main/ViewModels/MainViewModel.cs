﻿using CvsService.Log.Write.Interfaces;
using CvsService.Log.Write.Models;
using CvsService.Log.Write.Services;
using LGES_SVA.Core.Datas;
using LGES_SVA.Core.Datas.Settings;
using LGES_SVA.Core.Events;
using LGES_SVA.Core.Interfaces;
using LGES_SVA.Core.Interfaces.Settings;
using LGES_SVA.Recipe.Services;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace LGES_SVA.Main.ViewModels
{
	public class MainViewModel : BindableBase
    {
        #region Fields for property
        private AppSetting _appSetting;
        private readonly IRegionManager _regionManager;
        private readonly IEventAggregator _eventAggregator;
        private readonly IDisposeManager _disposeManager;
        private readonly ILogWriteManager _logWirteManager = LogWriteManager.Instance;
        private ISettingRepository _settingRepo;
		private RecipeService _recipeService;
        private IInspectionManager _inspectionManager;
		#endregion

		public AppSetting AppSetting { get => _appSetting; set => SetProperty(ref _appSetting, value); }
        public RecipeService RecipeService { get => _recipeService; }
        public IInspectionManager InspectionManager { get => _inspectionManager;  }
        public ISettingRepository SettingRepository { get => _settingRepo; set => SetProperty(ref _settingRepo, value); }
        public ICommand LoadedCommand => new DelegateCommand(OnLoaded);
        public ICommand ClosingCommand => new DelegateCommand<CancelEventArgs>(OnClosing);
        public ICommand ClosedCommand => new DelegateCommand(OnClosed);
        public ICommand MouseMoveCommand => new DelegateCommand(OnMouseMove);
        public ICommand RunAndStopCommand => new DelegateCommand(OnRunAndStop);

		

        public MainViewModel(IRegionManager rm, IEventAggregator ea, ISettingRepository sr, IDisposeManager dm, RecipeService rs, IInspectionManager im)
        {
            _regionManager = rm;
            _eventAggregator = ea;
            AppSetting = sr.AppSetting;
            SettingRepository = sr;
            _disposeManager = dm;
            _recipeService = rs;
            _inspectionManager = im;

            _logWirteManager.Info(new InfoLogData("프로그램 시작",$"{AppSetting.ProgramVersion}"));
        }

        private void OnRunAndStop()
        {
            if(InspectionManager.InspectionState == Core.Enums.EInspectionState.Stop)
			{
                InspectionManager.InspectionStartAsync();
			}
			else
			{
                InspectionManager.InspectionStopAsync();
            }
        }

        private void OnMouseMove()
        {
            _eventAggregator.GetEvent<MouseMoveEvent>().Publish();
        }

        private void OnLoaded()
        {
            SetDefaultRegion();
        }

        /// <summary>
        /// 초기 Region은 여기 등록해야됨
        /// </summary>
        private void SetDefaultRegion()
        {
            // TODO : 초기 Region은 여기 등록해야함

            _regionManager.RequestNavigate(RegionNames.MainViewRegion, ViewNames.InspectionView);
            _regionManager.RequestNavigate(RegionNames.ControlRegion, ViewNames.ControlBarView);
            _regionManager.RequestNavigate(RegionNames.TabViewRegion, ViewNames.TabView);
            _regionManager.RequestNavigate(RegionNames.YieldRegion, ViewNames.YieldView);

            //
            _regionManager.RequestNavigate(RegionNames.TabInnerRegion, ViewNames.TabGraphView);
            _regionManager.RequestNavigate(RegionNames.RecipeSettingRegion, ViewNames.RecipeBasicSettingView);

        }
        private void OnClosing(CancelEventArgs e)
        {
            if (MessageBox.Show("종료하시겠습니까?", "", MessageBoxButton.YesNo) != MessageBoxResult.Yes)
            {
                e.Cancel = true;
                return;
            }
            _eventAggregator.GetEvent<MainWindowClosingEvent>().Publish();

            _disposeManager.DisposeResources();
            _eventAggregator.GetEvent<MainWindowClosedEvent>().Publish();
        }
        private void OnClosed()
        {
        }
    }
}
