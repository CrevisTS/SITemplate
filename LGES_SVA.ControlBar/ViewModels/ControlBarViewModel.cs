using CvsService.Core.Interfaces;
using LGES_SVA.Core.Datas;
using LGES_SVA.Core.Enums;
using LGES_SVA.Core.Interfaces;
using LGES_SVA.Core.Interfaces.Settings;
using LGES_SVA.Core.Interfaces.Communicate;
using LGES_SVA.Login.Services;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using Prism.Services.Dialogs;
using System;
using System.Windows.Input;

namespace LGES_SVA.ControlBar.ViewModels
{
    public class ControlBarViewModel : BindableBase
    {
        #region Fields for property
        private EViewType _mainRegionContent = EViewType.InspectionView;
        #endregion

        private readonly IRegionManager _regionManager;
        private readonly IInspectionManager _inspectionManager;
        private readonly IDialogService _dialogService;
        private ISettingRepository _settingRepository;
        private ICommunicateRepository _communicateRepository;

        private LoginService _loginService;

        public IInspectionStateProvider InsepctionStateProvider => _inspectionManager;
        public ISettingRepository SettingRepository { get => _settingRepository; set => SetProperty(ref _settingRepository, value); }
        public ICommunicateRepository ComunicateRepository { get => _communicateRepository; set => SetProperty(ref _communicateRepository, value); }
        public EViewType MainRegionContent { get => _mainRegionContent; set => SetProperty(ref _mainRegionContent, value); }

        // Main Menu 
        public ICommand LoginBtnClickCommand => new DelegateCommand(LoginToggleClick);
        public ICommand SimulationBtnClickCommand => new DelegateCommand(SimulationClick);
        public ICommand LogBtnClickCommand => new DelegateCommand(LogClick);
        public ICommand RecipeBtnClickCommand => new DelegateCommand(RecipeClick);
        public ICommand SearchBtnClickCommand => new DelegateCommand(SearchClick);
        public ICommand LiveBtnClickCommand => new DelegateCommand(LiveClick);
        public ICommand SettingBtnClickCommand => new DelegateCommand(SettingClick);

        // Communicate Menu
        public ICommand IOBtnClickCommand => new DelegateCommand(IOClick);
        public ICommand PLCBtnClickCommand => new DelegateCommand(OnPLCDialogShow);
        public ICommand DBBtnClickCommand => new DelegateCommand(OnDBDialogShow);
        public ICommand LightBtnClickCommand => new DelegateCommand(OnLightDialogShow);
        public ICommand CamBtnClickCommand => new DelegateCommand(OnCamDialogShow);

		

		public ControlBarViewModel(IRegionManager regionManager, IInspectionManager inspectionManager, IDialogService dialogService, ISettingRepository settingRepository, LoginService loginService, ICommunicateRepository communicateRepository)
        {
            _regionManager = regionManager;
            _inspectionManager = inspectionManager;
            _dialogService = dialogService;
            _settingRepository = settingRepository;
            _loginService = loginService;
            _communicateRepository = communicateRepository;
        }

		#region Main Menu 
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
			try
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
			catch (Exception)
			{

				throw;
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
		#endregion

		#region Communicate Menu
		private void IOClick()
        {
            _dialogService.ShowDialog(DialogNames.IODialog);
        }

        private void OnPLCDialogShow()
        {
            _dialogService.ShowDialog(DialogNames.PLCDialog);
        }
        private void OnCamDialogShow()
        {
            _dialogService.ShowDialog(DialogNames.CamDialog);
        }

        private void OnLightDialogShow()
        {
            _dialogService.ShowDialog(DialogNames.LightDialog);
        }

        private void OnDBDialogShow()
        {
            _dialogService.ShowDialog(DialogNames.DBDialog);
        }

        #endregion













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