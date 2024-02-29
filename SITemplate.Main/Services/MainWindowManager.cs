using CvsService.UI.Windows.Enums;
using Prism.Mvvm;
using SITemplate.Core.Interfaces.Windows;

namespace SITemplate.Main.Services
{
    public class MainWindowManager : BindableBase, IMainWindowManager
    {
        private EWindowTheme _windowTheme = EWindowTheme.Dark;
        public EWindowTheme WindowTheme { get => _windowTheme; set => SetProperty(ref _windowTheme, value); }
        
        // TODO : Language같은 내용들 필요하다면 이곳에 추가
    }
}
