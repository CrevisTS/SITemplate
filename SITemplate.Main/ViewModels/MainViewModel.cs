using CvsService.UI.Windows.Enums;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using SITemplate.Core.Datas;
using SITemplate.Core.Events;
using SITemplate.Core.Interfaces;
using SITemplate.Core.Interfaces.Windows;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace SITemplate.Main.ViewModels
{
    public class MainViewModel : BindableBase
    {
        #region Fields for property
        private EWindowTheme _windowTheme;
        private IMainWindowManager _mainWindowManager;
        #endregion

        private readonly IRegionManager _regionManager;
        private readonly IEventAggregator _eventAggregator;
        private readonly IDisposeManager _disposeManager;

        public IMainWindowManager MainWindowManager => _mainWindowManager;

        public ICommand LoadedCommand => new DelegateCommand(OnLoaded);
        public ICommand ClosingCommand => new DelegateCommand<CancelEventArgs>(OnClosing);
        public ICommand ClosedCommand => new DelegateCommand(OnClosed);

        public MainViewModel(IRegionManager regionManager, IEventAggregator eventAggregator, IMainWindowManager mainWindowManager, IDisposeManager disposeManager)
        {
            _mainWindowManager = mainWindowManager;
            _regionManager = regionManager;
            _eventAggregator = eventAggregator;
            _disposeManager = disposeManager;
        }
        private void OnLoaded()
        {
            SetDefaultRegion();
            SubscribeEvent();
        }
        private void SetWindowTheme(EWindowTheme windowTheme)
        {
            _mainWindowManager.WindowTheme = windowTheme;
        }
        private void SetDefaultRegion()
        {
            _regionManager.RequestNavigate(RegionNames.MainViewRegion, ViewNames.InspectionView);
            _regionManager.RequestNavigate(RegionNames.ControlRegion, ViewNames.ControlBarView);
        }
        private void SubscribeEvent()
        {
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
