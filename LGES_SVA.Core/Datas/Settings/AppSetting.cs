using LGES_SVA.Core.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Prism.Mvvm;
using System.Collections.Generic;

namespace LGES_SVA.Core.Datas.Settings
{
	public class AppSetting : BindableBase
    {
        private string _programVersion = "v1.0.0";
        private WindowSetting _windowSetting = new WindowSetting();
        private EUserLevelType _userLevel = EUserLevelType.None;
        private Dictionary<EUserLevelType, string> _user;

        // TODO : 프로그램 버전
		public string ProgramVersion { get => _programVersion; set => SetProperty(ref _programVersion, value); }
        
        public WindowSetting WindowSetting { get => _windowSetting; set => SetProperty(ref _windowSetting, value); }

        /// <summary>
        /// 사용자 레벨(권한) -> Json으로 저장하지 않음
        /// </summary>
        [JsonIgnore]
        public EUserLevelType UserLevel { get => _userLevel; set => SetProperty(ref _userLevel, value); }

        public Dictionary<EUserLevelType, string> User { get => _user; set => SetProperty(ref _user, value); }

		public AppSetting()
		{
            User = new Dictionary<EUserLevelType, string> { { EUserLevelType.Operator, "1234" }, { EUserLevelType.Engineer, "1234" } };
		}
	}
}
