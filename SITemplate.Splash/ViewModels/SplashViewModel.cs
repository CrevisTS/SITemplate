using CvsService.Prism.Interfaces;
using Prism.Commands;
using Prism.Mvvm;
using SITemplate.Core.Events;
using SITemplate.Core.Interfaces;
using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SITemplate.Splash.ViewModels
{
    public class SplashViewModel : BindableBase, ISplashViewModel
    {
        #region Fields for property
        private string _message;
        private int _progressValue;
        #endregion

        private readonly IAppBootstrapper _appBoot;

        public string Message { get => _message; set => SetProperty(ref _message, value); }
        public int ProgressValue { get => _progressValue; set => SetProperty(ref _progressValue, value); }

        public ICommand LoadedCommand => new DelegateCommand(async () => await OnLoaded());

        public event EventHandler WindowLoadedCompleted;

        public SplashViewModel(IAppBootstrapper appBoot)
        {
            _appBoot = appBoot;
        }

        private Task OnLoaded()
        {
            _appBoot.WindowLoadedControl += OnSplashWindowLoaded;
            _appBoot.WindowLoadedCompleted += OnSplashWindowLoadedCompleted;

            return _appBoot.InitializeAsync();
        }

        private void OnSplashWindowLoaded(object sender, ProgressMessageEventArgs e)
        {
            Message = e.Message;
            ProgressValue = e.ProgressValue;
        }

        private void OnSplashWindowLoadedCompleted(object sender, EventArgs e)
        {
            WindowLoadedCompleted?.Invoke(this, e);
        }
    }
}
