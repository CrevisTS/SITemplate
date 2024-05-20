using LGES_SVA.Core.Interfaces;
using LGES_SVA.Core.Interfaces.Communicate;
using LGES_SVA.Core.Interfaces.Modules.VisionPro;
using LGES_SVA.Core.Interfaces.Settings;
using LGES_SVA.Inspection.Services;
using LGES_SVA.Login.Services;
using LGES_SVA.Main.Services;
using LGES_SVA.Recipe.Services;
using LGES_SVA.Repository.Services;
using LGES_SVA.Splash.Bootstrappers;
using LGES_SVA.VisionPro.Services;
using Prism.Ioc;
using Prism.Modularity;

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
            containerRegistry.RegisterSingleton<IAppBootstrapper, AppBootstrapper>();
            containerRegistry.RegisterSingleton<IInspectionManager, InspectionManager>();
            containerRegistry.RegisterSingleton<IDisposeManager, DisposeManager>();
            containerRegistry.RegisterSingleton<ISettingRepository, SettingRepository>();
            containerRegistry.RegisterSingleton<ICommunicateRepository, CommunicateRepository>();

            containerRegistry.RegisterSingleton<IVisionProService, VisionProService>();

            containerRegistry.RegisterSingleton<LoginService>();
            containerRegistry.RegisterSingleton<RecipeService>();

        }
    }
}
