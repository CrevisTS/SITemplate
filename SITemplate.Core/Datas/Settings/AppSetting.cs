using Prism.Mvvm;
using System.Xml.Serialization;

namespace SITemplate.Core.Datas.Settings
{
    public class AppSetting : BindableBase
    {
        private WindowSetting _windowSetting = new WindowSetting();
        [XmlElement]
        public WindowSetting WindowSetting { get => _windowSetting; set => SetProperty(ref _windowSetting, value); }
    }
}
