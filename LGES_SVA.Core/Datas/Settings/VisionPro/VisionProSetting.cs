using LGES_SVA.Core.Datas.Recipe;
using Prism.Mvvm;
using System;
using System.IO;
using System.Reflection;

namespace LGES_SVA.Core.Datas.Settings.VisionPro
{
	public class VisionProSetting : BindableBase
	{
		private RecipeData _inspectionRecipe;
		private readonly string _path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), Assembly.GetEntryAssembly().GetName().Name, "VisionProFile");

		public string Calibration1Path { get; set; } = $"";
		public string Calibration2Path { get; set; } = "";
		public string Calibration3Path { get; set; } = "";
		public string Calibration4Path { get; set; } = "";


		public RecipeData InspectionRecipe { get => _inspectionRecipe; set => SetProperty(ref _inspectionRecipe, value); }

		public VisionProSetting()
		{
			Directory.CreateDirectory(_path);
			Calibration1Path = $@"{_path}\Calibration1.vpp";
			InspectionRecipe = new RecipeData("DefaultRecipe");

		}

	}
}
