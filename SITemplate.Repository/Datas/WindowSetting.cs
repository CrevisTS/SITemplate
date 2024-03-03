using CvsService.UI.Windows.Enums;
using Prism.Mvvm;

namespace SITemplate.Repository.Datas
{
    public class WindowSetting : BindableBase
    {
        private EWindowTheme _windowTheme = EWindowTheme.Dark;
        public EWindowTheme WindowTheme { get => _windowTheme; set => SetProperty(ref _windowTheme, value); }
    }
}
