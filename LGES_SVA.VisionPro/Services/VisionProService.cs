using Cognex.VisionPro;
using Cognex.VisionPro.CalibFix;
using Cognex.VisionPro.ToolBlock;
using CvsService.Core.Interfaces;
using LGES_SVA.Core.Interfaces.Settings;
using System;
using System.Drawing;

namespace LGES_SVA.VisionPro.Services
{
	public class VisionProService : IInitializable
	{
		private ISettingRepository _settingRepository;
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
		}

		public CogImage8Grey ConvertImage(Bitmap image)
		{
			return new CogImage8Grey(image);
		}
	}
}
