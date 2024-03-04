using SITemplate.Core.Datas.Settings;
using SITemplate.Core.Interfaces.Settings;
using SITemplate.Core.Utils;
using System;
using System.IO;
using System.Reflection;

namespace SITemplate.Repository.Services
{
    public class SettingRepository : ISettingRepository
    {
        private readonly string _settingFolderPath;
        private readonly string _settingFullPath;

        public AppSetting AppSetting { get; private set; }

        public SettingRepository()
        {
            _settingFolderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), Assembly.GetEntryAssembly().GetName().Name, "SettingFile");
            _settingFullPath = Path.Combine(_settingFolderPath, "AppSetting.xml");

            LoadXml();
        }

        public void LoadXml()
        {
            AppSetting = XmlParser.LoadXml<AppSetting>(_settingFullPath);
        }

        public void SaveXml()
        {
            XmlParser.SaveXml(AppSetting, _settingFolderPath, _settingFullPath);
        }
    }
}
