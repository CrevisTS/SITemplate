using CvsService.Core.Interfaces;
using CvsService.Log.Core.Interfaces;
using CvsService.Log.Display.Interfaces;
using CvsService.Log.Display.Services;
using CvsService.Log.Write.Interfaces;
using CvsService.Log.Write.Models;
using CvsService.Log.Write.Services;
using SITemplate.Core.Events;
using SITemplate.Core.Interfaces;
using SITemplate.Core.Interfaces.Settings;
using SITemplate.Splash.Error;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace SITemplate.Splash.Bootstrappers
{
    public class AppBootstrapper : IAppBootstrapper
    {
        private ILogWriteManager _logWrite;
        private ILogDisplayManager _logDisplay;

        private readonly Lazy<IDisposeManager> _lazyDisposeManager;
        private readonly Lazy<ISettingRepository> _lazySettingRepo;

        public bool IsFail { get; private set; } = false;

        public event EventHandler<ProgressMessageEventArgs> WindowLoadedControl;
        public event EventHandler WindowLoadedCompleted;

        public AppBootstrapper(Lazy<IDisposeManager> lazyDisposeManager, Lazy<ISettingRepository> lazySettingRepo)
        {
            _lazyDisposeManager = lazyDisposeManager;
            _lazySettingRepo = lazySettingRepo;
        }

        public Task InitializeAsync()
        {
            return Task.Run(() =>
            {
                Thread.CurrentThread.Name = "LoadingThread";

                // CvsService Log Initialize
                LogInit(20);
                // TODO : Prism Singleton 초기화 하는 부분.
                _ = LazyInstanceInit(_lazySettingRepo, "Setting", 50);

                // AppBoot에서 초기화하는 클래스 중 Dispose()가 필요하면 여기에서 추가.
                // 만약 다른곳에서 추가해야한다면 생성자에서 의존성 주입으로 IDisposeManager 받아서 추가하면 됨
                IDisposeManager disposeManager = LazyInstanceInit(_lazyDisposeManager, "Dispose Manager", 90);

                // disposeManager.AddIDisposable(CameraManager);
                // disposeManager.AddIDisposable(IOManager);
                // ..

                OnComplete();
            });
        }

        /// <summary>
        /// 만약 IInitializable 이라면 Initialize까지 실행되고, 아니라면 생성자만 실행됨 
        /// </summary>
        /// <param name="name"> SplashWindow에 표시될 로딩중인 이름 $"{name} Loading..." </param>
        /// <param name="percent"> SplashWindow의 ProgressBar에 표시될 로딩 % </param>
        private T LazyInstanceInit<T>(Lazy<T> lazy, string name, int percent)
        {
            T reVal = default;

            try
            {
                OnLoadedControl(new ProgressMessageEventArgs($"{name} Loading...", percent));
                reVal = lazy.Value;

                if (reVal is IInitializable ii) { ii.Initialize(); }
                _logWrite?.Info(new InfoLogData($"{name} Load Complete"));
            }
            catch (Exception ex) { ErrorWrite(ex, name); }

            return reVal;
        }

        private void LogInit(int percent)
        {
            try
            {
                OnLoadedControl(new ProgressMessageEventArgs("Log Loading...", percent));
                _logWrite = LogWriteManager.Instance;
                _logWrite.InitializeLogging(new LogFilePath());
                _logDisplay = LogDisplayManager.Instance;
                _logWrite?.Info(new InfoLogData("Log Load Complete"));

            }
            catch (Exception ex) { ErrorWrite(ex, "Log"); }
        }

        private void ErrorWrite(Exception ex, string failName)
        {
            IsFail = true;
            BootstrapperException bootEx = new BootstrapperException(failName, ex);
            ILogData bsErrLogData = new ErrorLogData(bootEx);
            _logWrite?.Error(bsErrLogData);

            if (_logDisplay != null) { _logDisplay.ShowErrCustomMsgBox(bsErrLogData); }
            else { _ = MessageBox.Show(bsErrLogData.FullMessage); }
        }

        protected virtual void OnLoadedControl(ProgressMessageEventArgs e)
        {
            WindowLoadedControl?.Invoke(this, e);
        }

        protected virtual void OnComplete()
        {
            OnLoadedControl(new ProgressMessageEventArgs("Complete", 100));
            WindowLoadedCompleted?.Invoke(this, EventArgs.Empty);
        }
    }
}
