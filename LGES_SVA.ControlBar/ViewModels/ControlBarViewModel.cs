using CvsService.Core.Interfaces;
using LGES_SVA.Core.Datas;
using LGES_SVA.Core.Enums;
using LGES_SVA.Core.Interfaces;
using LGES_SVA.Core.Interfaces.Settings;
using LGES_SVA.Login.Services;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using Prism.Services.Dialogs;
using System;
using System.Windows.Input;

namespace LGES_SVA.ControlBar.ViewModels
{
	/////////////////////////////////////////////////////////
	// TODO : 연결 상태 표시 UI 예시용. 삭제 필요. 
	public sealed class FakeConncetion : BindableBase, IConnectionMonitor
    {
        private string _name;
        private bool _isConnected;
        public string Name { get => _name; set => SetProperty(ref _name, value); }
        public bool IsConnected { get => _isConnected; set => SetProperty(ref _isConnected, value); }
	}
    //////////////////////////////////////////////////////////

    public class ControlBarViewModel : BindableBase
    {
        #region Fields for property
        private IConnectionMonitor _cameraConnection;
        private IConnectionMonitor _ioConnection;
        private IConnectionMonitor _PLCConnection;

        private EViewType _mainRegionContent = EViewType.InspectionView;
        #endregion

        private readonly IRegionManager _regionManager;
        private readonly IInspectionManager _inspectionManager;
        private readonly IDialogService _dialogService;
        private ISettingRepository _settingRepository;
        private LoginService _loginService;

        public IInspectionStateProvider InsepctionStateProvider => _inspectionManager;
        public ISettingRepository SettingRepository { get => _settingRepository; set => SetProperty(ref _settingRepository, value); }
        public IConnectionMonitor CameraConnection { get => _cameraConnection; set => SetProperty(ref _cameraConnection, value); }
        public IConnectionMonitor IOConnection { get => _ioConnection; set => SetProperty(ref _ioConnection, value); }
        public IConnectionMonitor PLCConnection { get => _PLCConnection; set => SetProperty(ref _PLCConnection, value); }
        public EViewType MainRegionContent { get => _mainRegionContent; set => SetProperty(ref _mainRegionContent, value); }

        // MainMenu 
        public ICommand LoginBtnClickCommand => new DelegateCommand(LoginToggleClick);
        public ICommand SimulationBtnClickCommand => new DelegateCommand(SimulationClick);
        public ICommand LogBtnClickCommand => new DelegateCommand(LogClick);
        public ICommand RecipeBtnClickCommand => new DelegateCommand(RecipeClick);
        public ICommand SearchBtnClickCommand => new DelegateCommand(SearchClick);
        public ICommand LiveBtnClickCommand => new DelegateCommand(LiveClick);
        public ICommand SettingBtnClickCommand => new DelegateCommand(SettingClick);
		public ICommand MainRegionChangeClickCommand => new DelegateCommand<object>(OnMainRegionChangeClick);
        public ICommand BtnStartStopClickCommand => new DelegateCommand(OnBtnStartStopClick);

        public ControlBarViewModel(IRegionManager regionManager, IInspectionManager inspectionManager, IDialogService dialogService, ISettingRepository settingRepository, LoginService loginService)
        {
            _regionManager = regionManager;
            _inspectionManager = inspectionManager;
            _dialogService = dialogService;
            _settingRepository = settingRepository;
            _loginService = loginService;

            CameraConnection = new FakeConncetion() { Name = "Cam", IsConnected = true };
            IOConnection = new FakeConncetion() { Name = "IO", IsConnected = false };
            PLCConnection = new FakeConncetion() { Name = "PLC", IsConnected = true };

        }
        
        public void LoginToggleClick()
        {
            // 로그아웃 상태 -> 로그인 창 띄움
            if(!_loginService.IsLogin)
			{
                _dialogService.ShowDialog(DialogNames.LoginDialog);
            }
			// 로그인 상태 -> 로그아웃 시킴
            else
			{
                _loginService.Logout();
            }
        }

        public void SimulationClick()
        {
            // 로그아웃 상태 -> 로그인 창 띄움
            if (!_loginService.IsLogin)
            {
                _dialogService.ShowDialog(DialogNames.LoginDialog);
            }
            else
            {
                _dialogService.ShowDialog(DialogNames.SimulationDialog);
            }
        }

        public void LogClick()
        {
            // 로그아웃 상태 -> 로그인 창 띄움
            if (!_loginService.IsLogin)
            {
                _dialogService.ShowDialog(DialogNames.LoginDialog);
            }
            else
            {
                _dialogService.ShowDialog(DialogNames.LogDialog);
            }
        }

        public void RecipeClick()
        {
            // 로그아웃 상태 -> 로그인 창 띄움
            if (!_loginService.IsLogin)
            {
                _dialogService.ShowDialog(DialogNames.LoginDialog);
            }
            else
            {
                _dialogService.ShowDialog(DialogNames.RecipeDialog);
            }
        }

        public void SearchClick()
        {
            // 로그아웃 상태 -> 로그인 창 띄움
            if (!_loginService.IsLogin)
            {
                _dialogService.ShowDialog(DialogNames.LoginDialog);
            }
            else
            {
                _dialogService.ShowDialog(DialogNames.SearchDialog);
            }
        }
        public void LiveClick()
        {
            // 로그아웃 상태 -> 로그인 창 띄움
            if (!_loginService.IsLogin)
            {
                _dialogService.ShowDialog(DialogNames.LoginDialog);
            }
            else
            {
                _dialogService.ShowDialog(DialogNames.LiveDialog);
            }
        }
        public void SettingClick()
        {
            // 로그아웃 상태 -> 로그인 창 띄움
            if (!_loginService.IsLogin)
            {
                _dialogService.ShowDialog(DialogNames.LoginDialog);
            }
            else
            {
                _dialogService.ShowDialog(DialogNames.SettingDialog);
            }
        }
        private void OnMainRegionChangeClick(object parameter)
        {
            EViewType newContent = (EViewType)Enum.Parse(typeof(EViewType), parameter.ToString());
            if (MainRegionContent == newContent) return;

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