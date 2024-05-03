using LGES_SVA.Core.Datas.Settings;
using LGES_SVA.Repository.Datas;

namespace LGES_SVA.Repository.Services.Interface
{
    public interface ISettingRepository
    {
        AppSetting AppSetting { get; set; }
        VisionProSetting VisionProSetting { get; set; }
        void LoadSetting();
        void SaveSetting();

    }
}
