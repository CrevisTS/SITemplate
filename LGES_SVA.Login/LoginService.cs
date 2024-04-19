using LGES_SVA.Core.Interfaces.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LGES_SVA.Login
{
    public class LoginService
    {
		private readonly ISettingRepository _settingRepository;
		public LoginService(ISettingRepository settingRepository)
		{
			_settingRepository = settingRepository;

		}
        public void A()
		{
			// 지금 선택된 User와 PW 확인
			if (_settingRepository.AppSetting.User[EUserLevelType.Operator] == obj)
			{
				_settingRepository.AppSetting.UserLevel = EUserLevelType.Operator;

				RequestClose?.Invoke(new DialogResult(ButtonResult.OK));

			}
			else
			{
				MessageBox.Show("비밀번호가 다릅니다.");
			}

			if (_settingRepository.AppSetting.User[EUserLevelType.Engineer] == obj)
			{
				_settingRepository.AppSetting.UserLevel = EUserLevelType.Engineer;

				RequestClose?.Invoke(new DialogResult(ButtonResult.OK));

			}
			else
			{
				MessageBox.Show("비밀번호가 다릅니다.");
			}
		}
    }
}
