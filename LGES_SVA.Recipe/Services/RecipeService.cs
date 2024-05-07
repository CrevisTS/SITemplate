using Cognex.VisionPro.ToolBlock;
using CvsService.Core.Interfaces;
using LGES_SVA.Core.Datas.Recipe;
using Prism.Mvvm;
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

		public RecipeService()
		{
			Recipes = new ObservableCollection<RecipeData>();
		}

		public void AddRecipe(string name)
		{
			Recipes.Add(new RecipeData(name: name));
		}

		#region Init
		public bool IsInit => true;
		public void Initialize()
		{
			_ = new CogToolBlock();
		}
		#endregion
	}
}
