using LGES_SVA.Core.Enums;
using LGES_SVA.Core.Events;
using LGES_SVA.Core.Interfaces.Settings;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Input;

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

			_eventAggregator.GetEvent<MouseMoveEvent>().Subscribe(() => MouseMove());
		}

		public bool Login(string obj)
		{
			// 지금 선택된 User와 PW 확인
			if (_settingRepository.AppSetting.User[EUserLevelType.Operator] == obj)
			{
				_settingRepository.AppSetting.UserLevel = EUserLevelType.Operator;
				AutoLogoutStart();
				return true;
			}
			else
			{
				return false;
			}

			if (_settingRepository.AppSetting.User[EUserLevelType.Engineer] == obj)
			{
				_settingRepository.AppSetting.UserLevel = EUserLevelType.Engineer;
				AutoLogoutStart();
				return true;
			}
			else
			{
				return false;
			}
		}

		/// <summary>
		/// 5분간 미 동작 시 자동 로그아웃
		/// </summary>
		private void AutoLogoutStart()
		{
			_autoLogoutTimer = new Timer();
			_autoLogoutTimer.AutoReset = false;
			_autoLogoutTimer.Elapsed += _autoLogoutTimer_Elapsed;
			_autoLogoutTimer.Interval = 100000;
			_autoLogoutTimer.Start();
		}

		private void _autoLogoutTimer_Elapsed(object sender, ElapsedEventArgs e)
		{
			_autoLogoutTimer.Stop();
			_autoLogoutTimer.Dispose();

			_settingRepository.AppSetting.UserLevel = EUserLevelType.None;
		}


		private void MouseMove()
		{
			_autoLogoutTimer.Interval = 5000;
		}
	}
}
