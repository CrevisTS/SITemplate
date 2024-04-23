using CvsService.Core.Interfaces;
using LGES_SVA.Core.Datas.Settings;
using LGES_SVA.Core.Interfaces.Settings;
using LGES_SVA.Core.Utils;
using Prism.Mvvm;
using System;
using System.IO;
using System.Reflection;

namespace LGES_SVA.Repository.Services
{

    public class SettingRepository : BindableBase, ISettingRepository, IInitializable
    {
        private readonly string _settingFolderPath;
        private readonly string _settingFullPath;
        private AppSetting _appSetting;
        public AppSetting AppSetting { get => _appSetting; set => SetProperty(ref _appSetting, value); }

        public bool IsInit => true;

		public SettingRepository()
        {
            _settingFolderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), Assembly.GetEntryAssembly().GetName().Name, "SettingFile");
            _settingFullPath = Path.Combine(_settingFolderPath, "AppSetting.json");

            AppSetting = new AppSetting();
        }

        public void LoadSetting()
        {
            AppSetting = JsonParser.Load<AppSetting>(_settingFullPath);
        }

        public void SaveSetting()
        {
            JsonParser.Save(AppSetting, _settingFolderPath, _settingFullPath);
        }

		public void Initialize()
		{
            if (File.Exists(_settingFullPath))
            {
                LoadSetting();
            }
            else
            {
                SaveSetting();
                LoadSetting();
            }
        }

	}
}
