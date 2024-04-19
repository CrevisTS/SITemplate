using LGES_SVA.Core.Datas.Settings;

namespace LGES_SVA.Core.Interfaces.Settings
{
    public interface ISettingRepository
    {
        AppSetting AppSetting { get; }
        void LoadSetting();
        void SaveSetting();
    }
}
