﻿using CvsService.Core.Interfaces;
using LGES_SVA.Core.Datas.Settings;
using LGES_SVA.Core.Utils;
using LGES_SVA.Repository.Datas;
using LGES_SVA.Repository.Services.Interface;
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
        private readonly string _visionProFullPath;

        private AppSetting _appSetting;
        private VisionProSetting _visionProSetting;

        public AppSetting AppSetting { get => _appSetting; set => SetProperty(ref _appSetting, value); }

        public VisionProSetting VisionProSetting { get => _visionProSetting; set => SetProperty(ref _visionProSetting, value); }
        public bool IsInit => true;

		public SettingRepository()
        {
            _settingFolderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), Assembly.GetEntryAssembly().GetName().Name, "SettingFile");
            _settingFullPath = Path.Combine(_settingFolderPath, "AppSetting.json");
            _visionProFullPath = Path.Combine(_settingFolderPath, "VisionProSetting.json");

            AppSetting = new AppSetting();
            VisionProSetting = new VisionProSetting();

        }

        public void LoadSetting()
        {
            AppSetting = JsonParser.Load<AppSetting>(_settingFullPath);
            VisionProSetting = JsonParser.Load<VisionProSetting>(_visionProFullPath);
        }

        public void SaveSetting()
        {
            JsonParser.Save(AppSetting, _settingFolderPath, _settingFullPath);
            JsonParser.Save(VisionProSetting, _settingFolderPath, _visionProFullPath);
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
