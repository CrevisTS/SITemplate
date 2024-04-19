using LGES_SVA.Settings;
using LGES_SVA.Splash.ViewModels;
using LGES_SVA.Splash.Views;
using System;

namespace LGES_SVA
{
    public sealed class Starter
    {
        [STAThread]
        public static void Main()
        {
            // TODO : IModule 추가하는 부분
            _ = new App()
                .AddInversionModule<SingletonModules>()
                .AddInversionModule<ViewModules>()
                .WireViewViewModuel<WireDataContext>()
                .ActivateSplashWindow<SplashWindow, SplashViewModel>()
                .Run();
        }
    }
}
