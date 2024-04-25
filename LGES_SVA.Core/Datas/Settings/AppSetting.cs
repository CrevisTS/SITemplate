using LGES_SVA.Core.Datas.Settings.Enums;
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

        // Login 관련
        private EUserLevelType _userLevel = EUserLevelType.None;
        private Dictionary<EUserLevelType, string> _user;

        // AppSetting 관련
        private string _programName = "ProgramName";
        private int _NGCount;
        private string _deleteCycle;

        // TODO : 프로그램 버전
        public string ProgramVersion { get => _programVersion; set => SetProperty(ref _programVersion, value); }
        
        public string ProgramName { get => _programName; set => SetProperty(ref _programName, value); }

        public WindowSetting WindowSetting { get => _windowSetting; set => SetProperty(ref _windowSetting, value); }

        /// <summary>
        /// 사용자 레벨(권한) -> Json으로 저장하지 않음
        /// </summary>
        [JsonIgnore]
        public EUserLevelType NowUserLevel { get => _userLevel; set => SetProperty(ref _userLevel, value); }

        /// <summary>
        /// User DB
        /// </summary>
        public Dictionary<EUserLevelType, string> User { get => _user; set => SetProperty(ref _user, value); }

		
        public EInspectionMode InspectionMode { get; set; }
        public EROSMode ROSMode;

        public EByPassMode ByPassMode;

		private ImageSaveData _originalImageSave;
		private ImageSaveData _overlayImageSave;

		public ImageSaveData OriginalImageSave { get => _originalImageSave; set => SetProperty(ref _originalImageSave, value); }
        public ImageSaveData OverlayImageSave { get => _overlayImageSave; set => SetProperty(ref _overlayImageSave, value); }
		

		public string DeleteCycle { get => _deleteCycle; set => SetProperty(ref _deleteCycle, value); }
        public int NGCount { get => _NGCount; set => SetProperty(ref _NGCount, value); }

        [JsonIgnore]
        public List<EImageType> ImageTypeList { get; set; } = new List<EImageType>();

        public AppSetting()
        {
            User = new Dictionary<EUserLevelType, string> { { EUserLevelType.Operator, "1234" }, { EUserLevelType.Engineer, "1234" } };
            OriginalImageSave = new ImageSaveData();
            OverlayImageSave = new ImageSaveData();
        }

        public object Clone()
		{
            return MemberwiseClone();
		}
    }


}
