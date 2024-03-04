using CvsService.UI.Windows.Enums;
using Prism.Mvvm;
using System.Xml.Serialization;

namespace SITemplate.Core.Datas.Settings
{
    public class WindowSetting : BindableBase
    {
        private EWindowTheme _windowTheme = EWindowTheme.Dark;

        [XmlElement]
        public EWindowTheme WindowTheme { get => _windowTheme; set => SetProperty(ref _windowTheme, value); }
    }
}
