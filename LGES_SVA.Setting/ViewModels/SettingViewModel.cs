using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using LGES_SVA.Core.Datas.Settings;
using LGES_SVA.Core.Events;
using LGES_SVA.Core.Interfaces.Settings;
using System.Windows;

namespace LGES_SVA.Setting.ViewModels
{
    public class SettingViewModel : BindableBase, INavigationAware
    {
        private const string SAVE_CHECK_MESSAGE = "Would you like to save the settings?";
        private readonly ISettingRepository _settingRepo;

        public AppSetting AppSetting { get => _settingRepo.AppSetting; }
        public SettingViewModel(IEventAggregator eventAggregator, ISettingRepository settingRepo)
        {
            _settingRepo = settingRepo;
            
            _ = eventAggregator.GetEvent<MainWindowClosingEvent>().Subscribe(() => SaveCheck());
        }

        private void SaveCheck()
        {
            if (MessageBox.Show(SAVE_CHECK_MESSAGE, "", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                _settingRepo.SaveSetting();
            else
                _settingRepo.LoadSetting();
        }

        public bool IsNavigationTarget(NavigationContext navigationContext) => true;
        public void OnNavigatedFrom(NavigationContext navigationContext) => SaveCheck(); // 다른 View로 바뀔 때
        public void OnNavigatedTo(NavigationContext navigationContext) { } // Region영역에 이 View가 표시될 때
    }
}
