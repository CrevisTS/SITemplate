using Cognex.VisionPro;
using Cognex.VisionPro.ToolBlock;
using LGES_SVA.Core.Interfaces.Settings;
using LGES_SVA.Recipe.Services;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using System.Windows;
using System.Windows.Input;

namespace LGES_SVA.Dialogs.Recipe.ViewModels
{
	public class RecipeViewModel : BindableBase, IDialogAware
	{
		private RecipeService _recipeService;
		private ISettingRepository _settingRepository;

		public RecipeService RecipeService { get => _recipeService; set => SetProperty(ref _recipeService, value); }
		public CogToolBlockEditV2 ToolBlockWindow { get; set; } = new CogToolBlockEditV2();

		public ICommand AddRecipeCommand => new DelegateCommand(OnAddRecipe);
		public ICommand SelectedCommand => new DelegateCommand(OnSelected);
		public RecipeViewModel(RecipeService recipeService, ISettingRepository settingRepository)
		{
			_recipeService = recipeService;
			_settingRepository = settingRepository;

			ToolBlockInit();
		}

		public void ToolBlockInit()
		{
			var toolBlockPath = _settingRepository.VisionProSetting.InspectionRecipe;

			// 선택된 레시피가 없다면
			if (toolBlockPath == null)
			{
				return;
			}
			// 선택된 레시피가 있다면
			else
			{
				var Toolblock = CogSerializer.LoadObjectFromFile(_settingRepository.VisionProSetting.InspectionRecipe.Path);
				ToolBlockWindow.Subject = Toolblock as CogToolBlock;
			}
		}

		public void OnSelected()
		{
			var nowRecipe = RecipeService.NowRecipe;
			if (nowRecipe == null)
			{
				return;
			}
			// 선택된 레시피가 있다면
			else
			{
				// 다른 레시피 선택 시 현재 레시피의 툴 경로를 변경
				RecipeService.NowRecipe.Path = ToolBlockWindow.Filename;
			}
			
		}
		private void OnAddRecipe()
		{
			_recipeService.AddRecipe("abc");
		}

		#region DialogAware
		public string Title => "Recipe";

		public event Action<IDialogResult> RequestClose;

		public bool CanCloseDialog()
		{
			return true;
		}

		public void OnDialogClosed()
		{
			

			try
			{
				if(ToolBlockWindow.Subject != null)
				{
					CogSerializer.SaveObjectToFile(ToolBlockWindow.Subject, $@"D:\DAT\test.vpp");
				}
			}
			catch (Exception)
			{

				throw;
			}

			Application.Current.Dispatcher.Invoke(() =>
			{
				RequestClose?.Invoke(new DialogResult(ButtonResult.Cancel));
			});
			//_settingRepository.VisionProSetting.NowRecipe = RecipeService.NowRecipe;
			//_settingRepository.SaveSetting();
		}

		public void OnDialogOpened(IDialogParameters parameters)
		{
		}

		#endregion
	}
}
