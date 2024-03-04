using CvsService.Log.Write.Models;
using CvsService.Log.Write.Services;
using CvsService.Prism.Services;
using Prism.Ioc;
using SITemplate.Main.Views;
using System;
using System.Reflection;
using System.Windows;

namespace SITemplate
{
    public class App : PrismApplicationHelper
    {
        protected override Window CreateShell()
        {
            RunSplash();
            // base.InitializeModules(); -> 만약 Splash, AppBootstrapper를 사용하지 않는다면 RunSplash()를 지우고 활성화.

            return new MainWindow();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            CheckApplicationIsRunning(e, Assembly.GetExecutingAssembly().ManifestModule.Name);
        }

        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            string title = "UnHandled Exception!";
            string msg = e.ExceptionObject.ToString();
            try { LogWriteManager.Instance.Error(new ErrorLogData(title, e.ExceptionObject.ToString())); }
            catch { _ = MessageBox.Show(title + Environment.NewLine + msg, "Error", MessageBoxButton.OK, MessageBoxImage.Error); }
            finally
            {
                Environment.Exit(0);
                System.Diagnostics.Process.GetCurrentProcess().Kill();
            }
        }
    }
}
