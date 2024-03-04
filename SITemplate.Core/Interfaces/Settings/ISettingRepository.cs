using SITemplate.Core.Datas.Settings;

namespace SITemplate.Core.Interfaces.Settings
{
    public interface ISettingRepository
    {
        AppSetting AppSetting { get; }
        void LoadXml();
        void SaveXml();
    }
}
