using Cognex.VisionPro.ToolBlock;
using CvsService.Core.Interfaces;
using LGES_SVA.Core.Datas.Recipe;
using LGES_SVA.Core.Utils;
using LGES_SVA.VisionPro.Services;
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

		private ObservableCollection<RecipeData> _recipes;
		private RecipeData _nowRecipe;
		private RecipeData _selectedRecipe;
		private VisionProService _visionProService;


		// 저장된 레시피
		public ObservableCollection<RecipeData> Recipes { get => _recipes; set => SetProperty(ref _recipes, value); }

		// 현재 사용 할 레시피
		public RecipeData NowRecipe { get => _nowRecipe; set => SetProperty(ref _nowRecipe, value); }

		// 레시피 세팅에서 선택된 레시피
		public RecipeData SelectedRecipe { get => _selectedRecipe; set => SetProperty(ref _selectedRecipe, value); }

		public RecipeService(VisionProService vps)
		{
			_visionProService = vps;

			_filePath = Path.Combine(_path, "Recipe.json");
			Recipes = new ObservableCollection<RecipeData>();

			LoadRecipe();
		}

		public void AddRecipe(string name)
		{
			try
			{
				// 중복 레시피 확인해 덮어 씌움
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
			catch (Exception)
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

		public void SelectNowRecipe(RecipeData selectRecipe)
		{
			foreach(var recipe in Recipes)
			{
				recipe.IsNowRecipe = false;
			}

			selectRecipe.IsNowRecipe = true;

		}

		public void FindNowRecipe()
		{
			foreach(var recipe in Recipes)
			{
				if (recipe.IsNowRecipe)
				{
					NowRecipe = recipe;

					LoadNowRecipe();
					break;
				}
			}
		}


		public void SaveRecipe()
		{
			JsonParser.Save(Recipes, _path, _filePath);
			LoadRecipe();
		}


		public void LoadNowRecipe()
		{
			if (!File.Exists(NowRecipe.ToolPath)) return;
			
			NowRecipe.ToolBlock = _visionProService.Load(NowRecipe.ToolPath) as CogToolBlock;
		}

		#region Init
		public bool IsInit => true;
		public void Initialize()
		{
			LoadNowRecipe();
		}
		#endregion
	}
}
