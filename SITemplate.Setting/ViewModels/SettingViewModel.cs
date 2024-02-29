using Prism.Mvvm;
using SITemplate.Core.Interfaces.Windows;

namespace SITemplate.Setting.ViewModels
{
    public class SettingViewModel : BindableBase
    {
        private IMainWindowManager _mainWindowManager;

        public IMainWindowManager MainWindowManager => _mainWindowManager;
        public SettingViewModel(IMainWindowManager mainWindowManager)
        {
            _mainWindowManager = mainWindowManager;
        }
    }
}
