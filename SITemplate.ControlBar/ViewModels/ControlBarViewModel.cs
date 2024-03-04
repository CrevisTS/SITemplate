using CvsService.Core.Interfaces;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using SITemplate.Core.Datas;
using SITemplate.Core.Enums;
using SITemplate.Core.Interfaces;
using System;
using System.Windows.Input;

namespace SITemplate.ControlBar.ViewModels
{
    // TODO : 연결 상태 표시 UI 예시용. 삭제 필요. 
    public sealed class FakeConncetion : BindableBase, IConnectionMonitor
    {
        private string _name;
        private bool _isConnected;
        public string Name { get => _name; set => SetProperty(ref _name, value); }
        public bool IsConnected { get => _isConnected; set => SetProperty(ref _isConnected, value); }
    }

    public class ControlBarViewModel : BindableBase
    {
        #region Fields for property
        private IConnectionMonitor _cameraConnection;
        private IConnectionMonitor _ioConnection;
        private EViewType _mainRegionContent = EViewType.InspectionView;
        #endregion

        private readonly IRegionManager _regionManager;
        private readonly IInspectionManager _inspectionManager;

        public IInspectionStateProvider InsepctionStateProvider => _inspectionManager;
        public IConnectionMonitor CameraConnection { get => _cameraConnection; set => SetProperty(ref _cameraConnection, value); }
        public IConnectionMonitor IOConnection { get => _ioConnection; set => SetProperty(ref _ioConnection, value); }
        public EViewType MainRegionContent { get => _mainRegionContent; set => SetProperty(ref _mainRegionContent, value); }

        public ICommand MainRegionChangeClickCommand => new DelegateCommand<object>(OnMainRegionChangeClick);
        public ICommand BtnStartStopClickCommand => new DelegateCommand(OnBtnStartStopClick);

        public ControlBarViewModel(IRegionManager regionManager, IInspectionManager inspectionManager)
        {
            _regionManager = regionManager;
            _inspectionManager = inspectionManager;
            
            CameraConnection = new FakeConncetion() { Name = "Cam", IsConnected = true };
            IOConnection = new FakeConncetion() { Name = "IO", IsConnected = false };
        }

        private void OnMainRegionChangeClick(object parameter)
        {
            EViewType newContent = (EViewType)Enum.Parse(typeof(EViewType), parameter.ToString());
            MainRegionContent = newContent;
            _regionManager.RequestNavigate(RegionNames.MainViewRegion, newContent.ToString());
        }

        private void OnBtnStartStopClick()
        {
            switch (_inspectionManager.InspectionState)
            {
                case EInspectionState.Stop:
                    _inspectionManager.InspectionStartAsync();
                    break;
                case EInspectionState.Start:
                    _inspectionManager.InspectionStopAsync();
                    break;
            }
        }
    }
}