using Cognex.VisionPro;
using Cognex.VisionPro.ToolBlock;
using LGES_SVA.Core.Interfaces.Settings;
using LGES_SVA.Recipe.Services;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using System.Windows.Input;

namespace LGES_SVA.Dialogs.Recipe.ViewModels
{
	public class RecipeViewModel : BindableBase, IDialogAware
	{
		private RecipeService _recipeService;
		private ISettingRepository _settingRepository;

		public RecipeService RecipeService { get => _recipeService; set => SetProperty(ref _recipeService, value); }
		public string Title => "Recipe";

		public event Action<IDialogResult> RequestClose;

		public CogToolBlockEditV2 ToolBlockWindow { get; set; } = new CogToolBlockEditV2();

		public ICommand AddRecipeCommand => new DelegateCommand(OnAddRecipe);

		private void OnAddRecipe()
		{
			RecipeService.AddRecipe("abc");
		}

		public void OnSelected()
		{
			if (RecipeService.NowRecipe.Path == "")
			{
				ToolBlockWindow.Subject = new CogToolBlock();
			}
			else
			{
				//ToolBlockWindow.Subject = new CogToolBlock();
			}
		}

		public RecipeViewModel(RecipeService recipeService, ISettingRepository settingRepository)
		{
			_recipeService = recipeService;
			_settingRepository = settingRepository;

			// 여기다가 툴블럭을 new해서 넣어!!
			var Toolblock = CogSerializer.LoadObjectFromFile($@"D:\DAT\test.vpp");
			ToolBlockWindow.Subject = Toolblock as CogToolBlock;

		}

		public bool CanCloseDialog()
		{
			return true;
		}

		public void OnDialogClosed()
		{
			CogSerializer.SaveObjectToFile(ToolBlockWindow.Subject, $@"D:\DAT\test.vpp");
			//_settingRepository.VisionProSetting.NowRecipe = RecipeService.NowRecipe;
			//_settingRepository.SaveSetting();
		}

		public void OnDialogOpened(IDialogParameters parameters)
		{
		}
	}
}
