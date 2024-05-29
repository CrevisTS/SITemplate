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
		private string _path;
		private string _model;
		private bool _isNowRecipe;

		/// <summary>
		/// 레시피 명 : 모델명_MMDDHHmm
		/// </summary>
		public string Name 
		{ 
			get => _name;
			set
			{
				SetProperty(ref _name, value);
				IsChanged = true;
			}
		}

		/// <summary>
		/// TooBlock 파일 경로
		/// </summary>
		public string Path 
		{ 
			get => _path;
			set
			{
				SetProperty(ref _path, value);
				IsChanged = true;
			}
		}

		/// <summary>
		/// 제품 모델명
		/// </summary>
		public string Model 
		{ 
			get => _model;
			set
			{
				SetProperty(ref _model, value);
				IsChanged = true;
			}
		}

		public bool IsNowRecipe { get => _isNowRecipe; set => SetProperty(ref _isNowRecipe, value); }

		[JsonIgnore]
		public bool IsChanged { get; private set; }

		public double AlignResultY_ { get; set; }
		public double PouchAngle_Correction { get; set; }
		

		public RecipeData(string name)
		{
			_name = name;
		}

		public void RecipeSaved()
		{
			IsChanged = false;
		}
	}
}
