using LGES_SVA.Core.Datas.Settings.Enums;
using LGES_SVA.Core.Enums;
using LGES_SVA.Core.Enums.Login;
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
        private ELevel _userLevel = ELevel.None;
        private Dictionary<ELevel, string> _user;

        // AppSetting 관련
        private string _inspectionName = "Inspection Name";
        private int _NGCount;
        private int _imageDeletePeriod;
        private EInspectionMode _inspectionMode;
        private EROSMode _ROSMode;
        private EByPassMode _ByPassMode;
        private ImageSaveData _originalImageSave;
        private ImageSaveData _overlayImageSave;

        private EMasterSampleMode _masterSampleMode;
		private string _resetTime;

		// TODO : 프로그램 버전
		public string ProgramVersion { get => _programVersion; set => SetProperty(ref _programVersion, value); }
        public WindowSetting WindowSetting { get => _windowSetting; set => SetProperty(ref _windowSetting, value); }
        
        /// <summary>
        /// 1. 검사기 이름
        /// </summary>
        public string InspectionName { get => _inspectionName; set => SetProperty(ref _inspectionName, value); }


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
		/// <summary>
        /// 이미지 삭제 주기(개월)
        /// </summary>
		public int ImageDeletePeriod { get => _imageDeletePeriod; set => SetProperty(ref _imageDeletePeriod, value); }
        /// <summary>
        /// 몇 회 이상 NG시 알람 울림 - 알람카운터
        /// </summary>
        public int NGCount { get => _NGCount; set => SetProperty(ref _NGCount, value); }
        /// <summary>
        /// 결과 리셋 시간(매일 한번씩 발생)
        /// </summary>
        public string ResetTime { get => _resetTime; set => SetProperty(ref _resetTime, value); }

        /// <summary>
        /// 9. Master Sample Mode
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public EMasterSampleMode MasterSampleMode { get => _masterSampleMode; set => SetProperty(ref _masterSampleMode, value); }


        [JsonIgnore]
        public int[] ImageDeletePeriodArray { get; } = new int[] { 6, 7, 8, 9, 10, 11, 12 };
        [JsonIgnore]
        public int[] NGCountArray { get; } = new int[] { 5, 6, 7, 8, 9, 10 };
        [JsonIgnore]
        public string[] ResetTimeArray { get; } = new string[] { "07:00", "07:30", "08:00", "08:30", "09:00", "09:30", "10:00" };

        public AppSetting()
        {
            OriginalImageSave = new ImageSaveData();
            OverlayImageSave = new ImageSaveData();

            ImageDeletePeriod = 6;
            NGCount = 5;
            ResetTime = "07:00";
        }

        public object Clone()
		{
            return MemberwiseClone();
		}
    }


}
