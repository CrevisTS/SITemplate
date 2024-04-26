using LGES_SVA.Core.Datas.Settings.Enums;
using LGES_SVA.Core.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Prism.Mvvm;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

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
        private string _inspectionName = "Inspection Name";
        private int _NGCount;
        private string _deleteCycle;
        private EInspectionMode _inspectionMode;
        private EROSMode _ROSMode;
        private EByPassMode _ByPassMode;
        private ImageSaveData _originalImageSave;
        private ImageSaveData _overlayImageSave;

        private EMasterSampleMode _masterSampleMode;

        // TODO : 프로그램 버전
        public string ProgramVersion { get => _programVersion; set => SetProperty(ref _programVersion, value); }
        public WindowSetting WindowSetting { get => _windowSetting; set => SetProperty(ref _windowSetting, value); }
        
        /// <summary>
        /// 1. 검사기 이름
        /// </summary>
        public string InspectionName { get => _inspectionName; set => SetProperty(ref _inspectionName, value); }


        /// <summary>
        /// 사용자 레벨(권한) -> Json으로 저장하지 않음
        /// </summary>
        [JsonIgnore]
        public EUserLevelType NowUserLevel { get => _userLevel; set => SetProperty(ref _userLevel, value); }

        /// <summary>
        /// User DB
        /// </summary>
        public Dictionary<EUserLevelType, string> User { get => _user; set => SetProperty(ref _user, value); }

        /// <summary>
        /// 2. 검사 모드
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public EInspectionMode InspectionMode { get => _inspectionMode; set => SetProperty(ref _inspectionMode, value); }
        
        /// <summary>
        /// 3. ROS 모드
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public EROSMode ROSMode { get => _ROSMode; set => SetProperty(ref _ROSMode, value); }

        /// <summary>
        /// 4. ByPass 모드
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public EByPassMode ByPassMode { get => _ByPassMode; set => SetProperty(ref _ByPassMode, value); }

		public ImageSaveData OriginalImageSave { get => _originalImageSave; set => SetProperty(ref _originalImageSave, value); }
        public ImageSaveData OverlayImageSave { get => _overlayImageSave; set => SetProperty(ref _overlayImageSave, value); }
		
		public string DeleteCycle { get => _deleteCycle; set => SetProperty(ref _deleteCycle, value); }
        public int NGCount { get => _NGCount; set => SetProperty(ref _NGCount, value); }

        /// <summary>
        /// 9. Master Sample Mode
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public EMasterSampleMode MasterSampleMode { get => _masterSampleMode; set => SetProperty(ref _masterSampleMode, value); }

      

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
