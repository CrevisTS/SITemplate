using CvsService.UI.Windows.Enums;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using SITemplate.Core.Datas;
using SITemplate.Core.Events;
using SITemplate.Core.Interfaces;
using SITemplate.Core.Interfaces.Inspections;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace SITemplate.Main.ViewModels
{
    public class MainViewModel : BindableBase
    {
        #region Fields for property
        private EWindowTheme _windowTheme;
        #endregion

        private readonly IRegionManager _regionManager;
        private readonly IEventAggregator _eventAggregator;
        private readonly IDisposeManager _disposeManager;

        public EWindowTheme WindowTheme { get => _windowTheme; set => SetProperty(ref _windowTheme, value); }

        public ICommand LoadedCommand => new DelegateCommand(OnLoaded);
        public ICommand ClosingCommand => new DelegateCommand<CancelEventArgs>(OnClosing);
        public ICommand ClosedCommand => new DelegateCommand(OnClosed);

        public MainViewModel(IRegionManager regionManager, IEventAggregator eventAggregator, IDisposeManager disposeManager)
        {
            _regionManager = regionManager;
            _eventAggregator = eventAggregator;
            _disposeManager = disposeManager;
        }
        private void OnLoaded()
        {
            SetWindowTheme(EWindowTheme.Dark);
            SetDefaultRegion();
            SubscribeEvent();
        }
        private void SetWindowTheme(EWindowTheme windowTheme)
        {
            WindowTheme = windowTheme;
        }
        private void SetDefaultRegion()
        {
            _regionManager.RequestNavigate(RegionNames.MainViewRegion, ViewNames.InspectionView);
            _regionManager.RequestNavigate(RegionNames.ControlRegion, ViewNames.ControlBarView);
        }
        private void SubscribeEvent()
        {
            _eventAggregator.GetEvent<WindowThemeChangeEvent>().Subscribe(SetWindowTheme);
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
