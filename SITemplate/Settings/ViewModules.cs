using Prism.Ioc;
using Prism.Modularity;
using SITemplate.ControlBar.Views;
using SITemplate.Core.Datas;
using SITemplate.Inspection.Views;
using SITemplate.Setting.Views;

namespace SITemplate.Settings
{
    public sealed class ViewModules : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            // TODO : Region영역에 사용할 UserControl View 선언 하는 부분
            containerRegistry.RegisterForNavigation<ControlBarView>(ViewNames.ControlBarView);
            containerRegistry.RegisterForNavigation<InspectionView>(ViewNames.InspectionView);
            containerRegistry.RegisterForNavigation<SettingView>(ViewNames.SettingView);
        }
    }
}
