using LGES_SVA.Core.Datas.Login;
using LGES_SVA.Core.Datas.Settings;
using LGES_SVA.Core.Datas.Settings.VisionPro;

namespace LGES_SVA.Core.Interfaces.Settings
{
    public interface ISettingRepository
    {
        AppSetting AppSetting { get; set; }
        VisionProSetting VisionProSetting { get; set; }
        User NowUser { get; set; }

        void LoadSetting();
        void SaveSetting();

    }
}
