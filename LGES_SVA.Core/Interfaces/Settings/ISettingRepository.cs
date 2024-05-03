﻿using LGES_SVA.Core.Datas.Settings;

namespace LGES_SVA.Core.Interfaces.SettingsA
{
    public interface ISettingRepository
    {
        AppSetting AppSetting { get; set; }
        void LoadSetting();
        void SaveSetting();

    }
}
