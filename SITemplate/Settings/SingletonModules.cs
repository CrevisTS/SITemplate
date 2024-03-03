using Prism.Ioc;
using Prism.Modularity;
using SITemplate.Core.Interfaces;
using SITemplate.Core.Interfaces.Inspections;
using SITemplate.Inspection.Services;
using SITemplate.Main.Services;
using SITemplate.Splash.Bootstrappers;

namespace SITemplate.Settings
{
    public sealed class SingletonModules : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            // TODO : Prism Singleton 선언 하는 부분
            _ = containerRegistry.RegisterSingleton<IAppBootstrapper, AppBootstrapper>();
            _ = containerRegistry.RegisterSingleton<IInspectionManager, InspectionManager>();
            _ = containerRegistry.RegisterSingleton<IDisposeManager, DisposeManager>();
        }
    }
}
