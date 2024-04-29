using LGES_SVA.Core.Enums;
using LGES_SVA.Core.Events;
using LGES_SVA.Core.Interfaces.Settings;
using Prism.Events;
using Prism.Mvvm;
using System.Timers;

namespace LGES_SVA.Login.Services
{
	public class LoginService : BindableBase
	{
		private readonly ISettingRepository _settingRepository;
		private readonly IEventAggregator _eventAggregator;

		private Timer _autoLogoutTimer;

		private EUserLevelType _selectedUserLevel;
		public EUserLevelType SelectedUserLevel { get => _selectedUserLevel; set => SetProperty(ref _selectedUserLevel, value); }
		public LoginService(ISettingRepository settingRepository, IEventAggregator eventAggregator)
		{
			_settingRepository = settingRepository;
			_eventAggregator = eventAggregator;

			// Mouse Move Event 구독
			_eventAggregator.GetEvent<MouseMoveEvent>().Subscribe(() => MouseMove());
		}

		public bool Login(string obj)
		{
			switch(SelectedUserLevel)
			{
				case EUserLevelType.Operator:

					if (_settingRepository.AppSetting.User[EUserLevelType.Operator] == obj)
					{
						_settingRepository.AppSetting.NowUserLevel = EUserLevelType.Operator;
						AutoLogoutStart();
						return true;
					}
					break;

				case EUserLevelType.Engineer:
					if (_settingRepository.AppSetting.User[EUserLevelType.Engineer] == obj)
					{
						_settingRepository.AppSetting.NowUserLevel = EUserLevelType.Engineer;
						AutoLogoutStart();
						return true;
					}
					break;
				case EUserLevelType.None:
				default:
					break;
			}
			
			return false;
			
		}

		public void Logout()
		{
			_settingRepository.AppSetting.NowUserLevel = EUserLevelType.None;
		}

		/// <summary>
		/// 5분간 미 동작 시 자동 로그아웃
		/// </summary>
		private void AutoLogoutStart()
		{
			_autoLogoutTimer = new Timer();
			_autoLogoutTimer.AutoReset = false;
			_autoLogoutTimer.Elapsed += _autoLogoutTimer_Elapsed;
			_autoLogoutTimer.Interval = 300000;
			_autoLogoutTimer.Start();
		}

		/// <summary>
		/// Timer Callback
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void _autoLogoutTimer_Elapsed(object sender, ElapsedEventArgs e)
		{
			_autoLogoutTimer.Stop();
			_autoLogoutTimer.Dispose();

			Logout();
		}

		/// <summary>
		/// MouseMove 이벤트 Callback
		/// </summary>
		private void MouseMove()
		{
			if(_autoLogoutTimer != null)
			{
				_autoLogoutTimer.Interval = 300000;
			}
		}
	}
}
