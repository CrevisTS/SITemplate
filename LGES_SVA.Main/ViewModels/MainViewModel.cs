using CvsService.Log.Write.Interfaces;
using CvsService.Log.Write.Models;
using CvsService.Log.Write.Services;
using LGES_SVA.Core.Datas;
using LGES_SVA.Core.Datas.Settings;
using LGES_SVA.Core.Events;
using LGES_SVA.Core.Interfaces;
using LGES_SVA.Repository.Services.Interface;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
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
        #endregion


        //private readonly ILogDisplayManager _logDisplayManager;

        public AppSetting AppSetting { get => _appSetting; set => SetProperty(ref _appSetting, value); }

        public ISettingRepository SettingRepository { get => _settingRepo; set => SetProperty(ref _settingRepo, value); }
        public ICommand LoadedCommand => new DelegateCommand(OnLoaded);
        public ICommand ClosingCommand => new DelegateCommand<CancelEventArgs>(OnClosing);
        public ICommand ClosedCommand => new DelegateCommand(OnClosed);
        public ICommand MouseMoveCommand => new DelegateCommand(OnMouseMove);

		private void OnMouseMove()
		{
            _eventAggregator.GetEvent<MouseMoveEvent>().Publish();
        }

        public MainViewModel(IRegionManager regionManager, IEventAggregator eventAggregator, ISettingRepository settingRepository, IDisposeManager disposeManager)
        {
            _regionManager = regionManager;
            _eventAggregator = eventAggregator;
            AppSetting = settingRepository.AppSetting;
            SettingRepository = settingRepository;
            _disposeManager = disposeManager;

            _logWirteManager.Info(new InfoLogData("프로그램 시작",$"{AppSetting.ProgramVersion}"));
        }

        private void OnLoaded()
        {
            SetDefaultRegion();
        }

        private void SetDefaultRegion()
        {
            _regionManager.RequestNavigate(RegionNames.MainViewRegion, ViewNames.InspectionView);
            _regionManager.RequestNavigate(RegionNames.ControlRegion, ViewNames.ControlBarView);
            _regionManager.RequestNavigate(RegionNames.TabViewRegion, ViewNames.TabView);
        }
        private void OnClosing(CancelEventArgs e)
        {
            if (MessageBox.Show("종료하시겠습니까?", "", MessageBoxButton.YesNo) != MessageBoxResult.Yes)
            {
                e.Cancel = true;
                return;
            }
            _eventAggregator.GetEvent<MainWindowClosingEvent>().Publish();
        }
        private void OnClosed()
        {
            _disposeManager.DisposeResources();
            _eventAggregator.GetEvent<MainWindowClosedEvent>().Publish();
        }
    }
}
