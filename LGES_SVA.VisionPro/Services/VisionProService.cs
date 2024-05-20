using Cognex.VisionPro;
using Cognex.VisionPro.ToolBlock;
using CvsService.Core.Interfaces;
using LGES_SVA.Core.Interfaces.Modules.VisionPro;
using LGES_SVA.Core.Interfaces.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LGES_SVA.VisionPro.Services
{
	public class VisionProService : IVisionProService, IInitializable
	{
		private ISettingRepository _settingRepository;

		/// <summary>
		/// Cam1, Cam2 Cailbration
		/// </summary>
		public CogToolBlock CailbrationTool1 { get; set; }
		/// <summary>
		/// Cam3, Cam4 Cailbration
		/// </summary>
		public CogToolBlock CailbrationTool2 { get; set; }

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

		public CogToolBlock Load(string path)
		{
			try
			{
				return CogSerializer.LoadObjectFromFile(path) as CogToolBlock;
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
			//if(_settingRepository.VisionProSetting.InspectionRecipe.Path != null)
			//{
			//	InspectTool1 = Load(_settingRepository.VisionProSetting.InspectionRecipe.Path);
			//	InspectTool2 = Load(_settingRepository.VisionProSetting.InspectionRecipe.Path);
			//}
		}
	}
}
