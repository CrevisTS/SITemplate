using LGES_SVA.Core.Datas.Recipe;
using LGES_SVA.Recipe.Services;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LGES_SVA.Dialogs.Recipe.ViewModels
{
	public class RecipeBasicSettingViewModel : BindableBase, INavigationAware
	{
		private RecipeService _recipeService;
		private RecipeData _recipe;

		public RecipeData Recipe { get => _recipe; set => SetProperty(ref _recipe, value); }
		public RecipeBasicSettingViewModel(RecipeService rs)
		{
			_recipeService = rs;
		}

		public void OnNavigatedTo(NavigationContext navigationContext)
		{
			Recipe = _recipeService.SelectedRecipe;
		}

		public bool IsNavigationTarget(NavigationContext navigationContext)
		{
			return false;
		}

		public void OnNavigatedFrom(NavigationContext navigationContext)
		{
			// 저장 여부 확인
		}
	}
}
