using LGES_SVA.Core.Datas;
using LGES_SVA.Core.Datas.Recipe;
using LGES_SVA.Recipe.Services;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace LGES_SVA.Dialogs.Recipe.ViewModels
{
	public class RecipeSettingViewModel : BindableBase, INavigationAware
	{
		private IRegionManager _regionManager;
		private RecipeService _recipeService;

		public RecipeData Recipe { get; set; }

		public ICommand BasicSettingCommand => new DelegateCommand(OnBasicSetting);
		public ICommand LeftSettingCommand => new DelegateCommand(OnLeftSetting);

		

		public RecipeSettingViewModel(IRegionManager rm, RecipeService rs)
		{
			_regionManager = rm;
			_recipeService = rs;
		}

		private void OnBasicSetting()
		{
			_regionManager.RequestNavigate(RegionNames.RecipeSettingInnerRegion, ViewNames.RecipeBasicSettingView);
		}
		private void OnLeftSetting()
		{
			_regionManager.RequestNavigate(RegionNames.RecipeSettingInnerRegion, ViewNames.RecipeLeftSettingView);
		}

		#region NavigationAware
		public void OnNavigatedTo(NavigationContext navigationContext)
		{
			Recipe = _recipeService.SelectedRecipe;

			_regionManager.Regions[RegionNames.RecipeSettingInnerRegion].RemoveAll();
			_regionManager.RequestNavigate(RegionNames.RecipeSettingInnerRegion, ViewNames.RecipeBasicSettingView);
		}

		public bool IsNavigationTarget(NavigationContext navigationContext)
		{
			return false;
		}

		public void OnNavigatedFrom(NavigationContext navigationContext)
		{
		}

		#endregion
	}
}
