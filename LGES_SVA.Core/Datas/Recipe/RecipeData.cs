using Cognex.VisionPro.ToolBlock;
using Newtonsoft.Json;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LGES_SVA.Core.Datas.Recipe
{
	public class RecipeData : BindableBase
	{
		private string _name;
		private string _toolPath;
		private string _model;
		private bool _isNowRecipe;
		private string _leftImagePath;
		private string _rightImagePath;

		/// <summary>
		/// 레시피 명 : 모델명_MMddHHmm
		/// </summary>
		public string Name { get => _name; set => SetProperty(ref _name, value); }

		/// <summary>
		/// TooBlock 파일 경로
		/// </summary>
		public string ToolPath { get => _toolPath; set => SetProperty(ref _toolPath, value); }

		public string LeftImagePath { get => _leftImagePath; set => SetProperty(ref _leftImagePath, value); }
		public string RightImagePath { get => _rightImagePath; set => SetProperty(ref _rightImagePath, value); }
		/// <summary>
		/// 제품 모델명
		/// </summary>
		public string Model { get => _model; set => SetProperty(ref _model, value); }

		public bool IsNowRecipe { get => _isNowRecipe; set => SetProperty(ref _isNowRecipe, value); }

		[JsonIgnore]
		public CogToolBlock ToolBlock { get; set; }

		public double AlignResultY_ { get; set; }
		public double PouchAngle_Correction { get; set; }


		public RecipeData(string name)
		{
			_name = name;
			_model = GetModel(name);
		}

		private string GetModel(string name)
		{
			string[] parts = name.Split('_');
			string leftPart = parts[0];
			return leftPart;
		}

	}
}
