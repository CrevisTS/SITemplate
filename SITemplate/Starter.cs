using SITemplate.Settings;
using SITemplate.Splash.ViewModels;
using SITemplate.Splash.Views;
using System;
using System.Drawing;
using System.Drawing.Imaging;

namespace SITemplate
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
