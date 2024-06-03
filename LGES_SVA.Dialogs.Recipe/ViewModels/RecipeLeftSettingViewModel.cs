using Cognex.VisionPro.Display;
using Cognex.VisionPro.ToolBlock;
using Cognex.VisionPro.Caliper;
using LGES_SVA.Core.Datas.Recipe;
using LGES_SVA.Recipe.Services;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using LGES_SVA.Core.Interfaces.Modules.VisionPro;
using LGES_SVA.Core.Interfaces.Settings;

namespace LGES_SVA.Dialogs.Recipe.ViewModels
{
	public class RecipeLeftSettingViewModel : INavigationAware
	{
		private RecipeService _recipeService;
		private IVisionProService _visionProService;
		private ISettingRepository _settingRepository;
		public RecipeData Recipe { get; set; }
		public CogFindLineEditV2 ToolBlockWindow { get; set; } = new CogFindLineEditV2();

		public RecipeLeftSettingViewModel(RecipeService rs, IVisionProService vps, ISettingRepository sr)
		{
			_recipeService = rs;
			_visionProService = vps;
			_settingRepository = sr;

			Recipe = _recipeService.SelectedRecipe;

			CogToolBlock Toolblock = _visionProService.Load(_settingRepository.VisionProSetting.InspectionRecipe.Path) as CogToolBlock;
			ToolBlockWindow.Subject = Toolblock.Tools["ROI1_LeadLine"] as CogFindLineTool;
		}
		public bool IsNavigationTarget(NavigationContext navigationContext)
		{
			return false;
		}

		public void OnNavigatedFrom(NavigationContext navigationContext)
		{
		}

		public void OnNavigatedTo(NavigationContext navigationContext)
		{
		}
	}
}
