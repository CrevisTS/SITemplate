using LGES_SVA.Core.Datas.Login;
using LGES_SVA.Core.Enums;
using LGES_SVA.Core.Enums.Login;
using LGES_SVA.Core.Events;
using LGES_SVA.Core.Interfaces.Settings;
using LGES_SVA.Core.Utils;
using Prism.Events;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Timers;

namespace LGES_SVA.Login.Services
{
	public class LoginService : BindableBase
	{
		private readonly string _path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), Assembly.GetEntryAssembly().GetName().Name, "SettingFile");
		private readonly string USER_PATH;

		private readonly ISettingRepository _settingRepository;
		private readonly IEventAggregator _eventAggregator;
		private readonly CSVParser _csvParser;

		// 300000 = 5분
		private const int MINUTE5 = 300000;

		private Timer _autoLogoutTimer;

		private List<User> _users;

		/// <summary>
		/// 현재 로그인 중인지 확인한다. True = 로그인 / False = 로그아웃
		/// </summary>
		public bool IsLogin { get; set; } = false;

		public LoginService(ISettingRepository settingRepository, IEventAggregator eventAggregator, CSVParser csvParser)
		{
			_settingRepository = settingRepository;
			_eventAggregator = eventAggregator;

			_csvParser = csvParser;

			// TODO : IPC에 D:가 없음
			USER_PATH = $@"{_path}\User.csv";
			_users = _csvParser.ReadCSV<User>(USER_PATH);

			// Mouse Move Event 구독
			_eventAggregator.GetEvent<MouseMoveEvent>().Subscribe(() => MouseMove());
		}

		public bool Login(string id, string pw)
		{
			for(int i = 0; i < _users.Count; i++)
			{
				if(_users[i].ID.Equals(id) && _users[i].Password.Equals(pw))
				{
					_settingRepository.NowUser = _users[i];
					IsLogin = true;
					AutoLogoutStart();

					return true;
				}
			}
			return false;
		}

		public void Logout()
		{
			_settingRepository.NowUser = new User(ELevel.None);
			IsLogin = false;

			AutoLogoutStop();

			_eventAggregator.GetEvent<LogoutEvent>().Publish();

		}

		/// <summary>
		/// 5분간 미 동작 시 자동 로그아웃
		/// </summary>
		private void AutoLogoutStart()
		{
			_autoLogoutTimer = new Timer
			{
				AutoReset = false
			};
			_autoLogoutTimer.Elapsed += AutoLogoutTimer_Elapsed;
			_autoLogoutTimer.Interval = MINUTE5;
			_autoLogoutTimer.Start();
		}

		private void AutoLogoutStop()
		{
			_autoLogoutTimer.Stop();
			_autoLogoutTimer.Dispose();
		}

		/// <summary>
		/// Timer Callback
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void AutoLogoutTimer_Elapsed(object sender, ElapsedEventArgs e)
		{
			AutoLogoutStop();

			Logout();
		}

		/// <summary>
		/// MouseMove 이벤트 Callback
		/// </summary>
		private void MouseMove()
		{
			if(_autoLogoutTimer != null)
			{
				_autoLogoutTimer.Interval = MINUTE5;
			}
		}
	}
}
