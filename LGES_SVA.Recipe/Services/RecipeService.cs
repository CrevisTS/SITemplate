using Cognex.VisionPro.ToolBlock;
using CvsService.Core.Interfaces;
using LGES_SVA.Core.Datas.Recipe;
using LGES_SVA.Core.Interfaces.Modules.VisionPro;
using LGES_SVA.Core.Utils;
using Prism.Mvvm;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Reflection;

namespace LGES_SVA.Recipe.Services
{
	public class RecipeService : BindableBase, IInitializable
	{
		private readonly string _path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), Assembly.GetEntryAssembly().GetName().Name, "SettingFile");
		private readonly string _filePath;
		private IVisionProService _visionProService;

		private ObservableCollection<RecipeData> _recipes;
		private RecipeData _nowRecipe;

		// 저장된 레시피
		public ObservableCollection<RecipeData> Recipes { get => _recipes; set => SetProperty(ref _recipes, value); }

		// 현재 선택된 레시피
		public RecipeData NowRecipe { get => _nowRecipe; set => SetProperty(ref _nowRecipe, value); }

		public RecipeService() { }
		public RecipeService(IVisionProService visionProService)
		{
			_visionProService = visionProService;
			_filePath = Path.Combine(_path, "Recipe.json");
			Recipes = new ObservableCollection<RecipeData>();

			LoadRecipe();


		}

		public void AddRecipe(string name)
		{
			try
			{
				foreach (var recipe in Recipes)
				{
					if (recipe.Name == name)
					{
						Recipes.Remove(recipe);
						break;
					}
				}
				Recipes.Add(new RecipeData(name: name));
			}
			catch (Exception e)
			{
				throw;
			}
			
		}

		public void RemoveRecipe(RecipeData recipeData)
		{
			Recipes.Remove(recipeData);
		}

		public void LoadRecipe()
		{
			if (File.Exists(_filePath))
			{
				Recipes =  JsonParser.Load<ObservableCollection<RecipeData>>(_filePath);
			}

			FindNowRecipe();
		}

		private void FindNowRecipe()
		{
			foreach(var recipe in Recipes)
			{
				if (recipe.IsNowRecipe)
				{
					NowRecipe = recipe;
					break;
				}
			}
		}

		public void SaveRecipe()
		{
			JsonParser.Save(Recipes, _path, _filePath);
			LoadRecipe();
		}


		#region Init
		public bool IsInit => true;
		public void Initialize() { }
		#endregion
	}
}
