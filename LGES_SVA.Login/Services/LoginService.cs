using LGES_SVA.Core.Enums;
using LGES_SVA.Core.Events;
using LGES_SVA.Core.Interfaces.Settings;
using Prism.Events;
using System.Timers;

namespace LGES_SVA.Login.Services
{
	public class LoginService
	{
		private readonly ISettingRepository _settingRepository;
		private readonly IEventAggregator _eventAggregator;

		private Timer _autoLogoutTimer;
		public LoginService(ISettingRepository settingRepository, IEventAggregator eventAggregator)
		{
			_settingRepository = settingRepository;
			_eventAggregator = eventAggregator;

			// Mouse Move Event 구독
			_eventAggregator.GetEvent<MouseMoveEvent>().Subscribe(() => MouseMove());
		}

		public bool Login(string obj)
		{
			// 지금 선택된 User와 PW 확인
			if (_settingRepository.AppSetting.User[EUserLevelType.Operator] == obj)
			{
				_settingRepository.AppSetting.NowUserLevel = EUserLevelType.Operator;
				AutoLogoutStart();
				return true;
			}
			else
			{
				return false;
			}

			if (_settingRepository.AppSetting.User[EUserLevelType.Engineer] == obj)
			{
				_settingRepository.AppSetting.NowUserLevel = EUserLevelType.Engineer;
				AutoLogoutStart();
				return true;
			}
			else
			{
				return false;
			}
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
