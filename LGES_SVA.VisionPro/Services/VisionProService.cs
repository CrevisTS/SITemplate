using Cognex.VisionPro;
using Cognex.VisionPro.CalibFix;
using Cognex.VisionPro.ToolBlock;
using CvsService.Core.Interfaces;
using LGES_SVA.Core.Interfaces.Modules.VisionPro;
using LGES_SVA.Core.Interfaces.Settings;
using System;

namespace LGES_SVA.VisionPro.Services
{
	public class VisionProService : IVisionProService, IInitializable
	{
		private ISettingRepository _settingRepository;

		/// <summary>
		/// Cam1
		/// </summary>
		public CogCalibNPointToNPointTool CalibrationTool1 { get; set; }
		/// <summary>
		/// Cam2
		/// </summary>
		public CogCalibNPointToNPointTool CalibrationTool2 { get; set; }
		/// <summary>
		/// Cam3
		/// </summary>
		public CogCalibNPointToNPointTool CalibrationTool3 { get; set; }
		/// <summary>
		/// Cam4
		/// </summary>
		public CogCalibNPointToNPointTool CalibrationTool4 { get; set; }


		/// <summary>
		/// 검사를 위한 툴
		/// </summary>
		public CogToolBlock InspectTool1 { get; set; }
		public CogToolBlock InspectTool2 { get; set; }

		public bool IsInit => true;

		public VisionProService(ISettingRepository settingRepository)
		{
			_settingRepository = settingRepository;
		}

		public object Load(string path)
		{
			try
			{
				return CogSerializer.LoadObjectFromFile(path);
			}
			catch (Exception)
			{
				throw;
			}
		}

		public void Save(object toolBlock, string path)
		{
			try
			{
				CogSerializer.SaveObjectToFile(toolBlock, path);
			}
			catch (Exception)
			{

				throw;
			}
		}

		public void Initialize()
		{
			CalibrationTool1 = Load(_settingRepository.VisionProSetting.Calibration1Path) as CogCalibNPointToNPointTool;
			//InspectTool2 = Load(_settingRepository.VisionProSetting.InspectionRecipe.Path);
		}
	}
}
