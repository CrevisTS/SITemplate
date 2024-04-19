using Prism.Ioc;
using Prism.Modularity;
using LGES_SVA.Core.Interfaces;
using LGES_SVA.Core.Interfaces.Settings;
using LGES_SVA.Inspection.Services;
using LGES_SVA.Main.Services;
using LGES_SVA.Repository.Services;
using LGES_SVA.Splash.Bootstrappers;
using LGES_SVA.Login.Services;

namespace LGES_SVA.Settings
{
    public sealed class SingletonModules : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            /*
            var a = new SettingRepo();
            _ = containerRegistry.RegisterInstance<IAppSettingRepo>(a);
            _ = containerRegistry.RegisterInstance<ISettingControl>(a);
            _ = containerRegistry.RegisterInstance<ILoginSettingRepo>(a);
            */

            // TODO : Prism Singleton 선언 하는 부분
            _ = containerRegistry.RegisterSingleton<IAppBootstrapper, AppBootstrapper>();
            _ = containerRegistry.RegisterSingleton<IInspectionManager, InspectionManager>();
            _ = containerRegistry.RegisterSingleton<IDisposeManager, DisposeManager>();
            _ = containerRegistry.RegisterSingleton<ISettingRepository, SettingRepository>();

            containerRegistry.RegisterSingleton<LoginService>();

        }
    }
}
