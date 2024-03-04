using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using SITemplate.Core.Datas;
using SITemplate.Core.Datas.Settings;
using SITemplate.Core.Events;
using SITemplate.Core.Interfaces;
using SITemplate.Core.Interfaces.Settings;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace SITemplate.Main.ViewModels
{
    public class MainViewModel : BindableBase
    {
        #region Fields for property
        private AppSetting _appSetting;
        #endregion

        private readonly IRegionManager _regionManager;
        private readonly IEventAggregator _eventAggregator;
        private readonly IDisposeManager _disposeManager;

        public AppSetting AppSetting { get => _appSetting; set => SetProperty(ref _appSetting, value); }

        public ICommand LoadedCommand => new DelegateCommand(OnLoaded);
        public ICommand ClosingCommand => new DelegateCommand<CancelEventArgs>(OnClosing);
        public ICommand ClosedCommand => new DelegateCommand(OnClosed);

        public MainViewModel(IRegionManager regionManager, IEventAggregator eventAggregator, ISettingRepository settingRepository, IDisposeManager disposeManager)
        {
            _regionManager = regionManager;
            _eventAggregator = eventAggregator;
            AppSetting = settingRepository.AppSetting;
            _disposeManager = disposeManager;
        }

        private void OnLoaded()
        {
            SetDefaultRegion();
        }

        private void SetDefaultRegion()
        {
            _regionManager.RequestNavigate(RegionNames.MainViewRegion, ViewNames.InspectionView);
            _regionManager.RequestNavigate(RegionNames.ControlRegion, ViewNames.ControlBarView);
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
