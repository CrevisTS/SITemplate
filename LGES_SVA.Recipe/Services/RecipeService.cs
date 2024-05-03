using Cognex.VisionPro.ToolBlock;
using CvsService.Core.Interfaces;
using LGES_SVA.Recipe.Datas;
using Prism.Mvvm;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace LGES_SVA.Recipe.Services
{
	public class RecipeService : BindableBase, IInitializable
	{
		private ObservableCollection<RecipeData> _recipes;
		private RecipeData _nowRecipe;

		// 저장된 레시피
		public ObservableCollection<RecipeData> Recipes { get => _recipes; set => SetProperty(ref _recipes, value); }

		// 현재 선택된 레시피
		public RecipeData NowRecipe { get => _nowRecipe; set => SetProperty(ref _nowRecipe, value); }

		/// <summary>
		/// Cam1, Cam2 Cailbration
		/// </summary>
		public CogToolBlock CailbrationTool1 { get; set; }
		/// <summary>
		/// Cam3, Cam4 Cailbration
		/// </summary>
		public CogToolBlock CailbrationTool2 { get; set; }

		public bool IsInit => true;

		public RecipeService()
		{
			Recipes = new ObservableCollection<RecipeData>();
		}

		public void AddRecipe(string name)
		{
			Recipes.Add(new RecipeData(name: name));
		}

		public void Initialize()
		{
			_ = new CogToolBlock();
		}
	}
}
