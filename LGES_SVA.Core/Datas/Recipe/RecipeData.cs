﻿using Newtonsoft.Json;
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
		/// 레시피 명 : 모델명_MMddHHmm
		/// </summary>
		public string Name
		{
			get => _name;
			set
			{
				SetProperty(ref _name, value);
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
			}
		}

		public bool IsNowRecipe { get => _isNowRecipe; set => SetProperty(ref _isNowRecipe, value); }

		public double AlignResultY_ { get; set; }
		public double PouchAngle_Correction { get; set; }


		public RecipeData(string name)
		{
			_name = name;
		}
	}

	public class RecipeLeftData
	{
		private string _imagePath;
		private Box ROI1 { get; set; }
		private Box ROI2 { get; set; }
		private Box ROI3 { get; set; }
	}
}
