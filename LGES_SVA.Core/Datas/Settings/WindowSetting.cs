using CvsService.UI.Windows.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Prism.Mvvm;
using System.Xml.Serialization;

namespace LGES_SVA.Core.Datas.Settings
{
    public class WindowSetting : BindableBase
    {
        private EWindowTheme _windowTheme = EWindowTheme.Dark;

        [JsonConverter(typeof(StringEnumConverter))]
        public EWindowTheme WindowTheme { get => _windowTheme; set => SetProperty(ref _windowTheme, value); }
    }
}
