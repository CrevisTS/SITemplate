using Cognex.VisionPro;
using Cognex.VisionPro.ToolBlock;
using LGES_SVA.Core.Datas.Recipe;
using LGES_SVA.Core.Events;
using LGES_SVA.Core.Interfaces.Modules.VisionPro;
using LGES_SVA.Core.Interfaces.Settings;
using LGES_SVA.Recipe.Services;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace LGES_SVA.Dialogs.Recipe.ViewModels
{
	public class RecipeViewModel : BindableBase, IDialogAware, IDisposable
	{
		private ISettingRepository _settingRepository;
		private IVisionProService _visionProService;
		private IEventAggregator _eventAggregator;
		private RecipeService _recipeService;
		private RecipeData _selectedRecipe;

		private int _count;

		public RecipeService RecipeService { get => _recipeService; set => SetProperty(ref _recipeService, value); }
		public RecipeData SelectedRecipe { get => _selectedRecipe; set => SetProperty(ref _selectedRecipe, value); }
		public CogToolBlockEditV2 ToolBlockWindow { get; set; } = new CogToolBlockEditV2();

		public ICommand AddRecipeCommand => new DelegateCommand(OnAddRecipe);
		public ICommand RemoveRecipeCommand => new DelegateCommand<RecipeData>(OnRemoveRecipe);
		public ICommand SaveRecipeCommand => new DelegateCommand(OnSaveRecipe);
		public ICommand SelectedCommand => new DelegateCommand<RecipeData>(OnSelected);
		public ICommand AddLeftImageCommand => new DelegateCommand(OnAddLeftImage);
		public ICommand AddRightImageCommand => new DelegateCommand(OnAddRightImage);
		public ICommand CloseCommand => new DelegateCommand(OnDialogClose);

		public RecipeViewModel(RecipeService rs, ISettingRepository sr, IVisionProService vp, IEventAggregator ea)
		{
			_recipeService = rs;
			_settingRepository = sr;
			_visionProService = vp;
			_eventAggregator = ea;

			ToolBlockWindowInit();
			
			// 툴블럭 파일명 변경 시 새로 Load하기 위해
			ToolBlockWindow.FilenameChanged += ToolBlockWindow_FilenameChanged;

			_eventAggregator.GetEvent<LogoutEvent>().Subscribe(() => LogoutDialogClosed());
			_eventAggregator.GetEvent<DialogClosingEvent>().Subscribe(OnDialogClosing, ThreadOption.PublisherThread);

		}


		private void OnDialogClosing(CancelEventArgs e)
		{
			var result = MessageBox.Show("저장하시겠습니까?", "Save", MessageBoxButton.YesNoCancel, MessageBoxImage.Question);

			switch (result)
			{
				case MessageBoxResult.Yes:
					// TODO : 저장 로직
					_recipeService.SaveRecipe();

					break;

				case MessageBoxResult.No:
					_recipeService.LoadRecipe();
					break;

				case MessageBoxResult.Cancel:
					e.Cancel = true;
					break;
			}
		}

		private void LogoutDialogClosed()
		{
			OnDialogClose();
		}

		private void OnDialogClose()
		{
			// TODO : 저장하지 않고 Close 해야함
			Application.Current.Dispatcher.Invoke(() =>
			{
				RequestClose?.Invoke(new DialogResult(ButtonResult.OK));
			});
		}

		public void ToolBlockWindowInit()
		{
			var toolBlockPath = _settingRepository.VisionProSetting.InspectionRecipe.Path;

			// 현재 레시피가 없다면
			if (toolBlockPath == null)
			{
				return;
			}
			// 현재 레시피가 있다면
			else
			{
				var Toolblock = _visionProService.Load(_settingRepository.VisionProSetting.InspectionRecipe.Path) as CogToolBlock;
				ToolBlockWindow.Subject = Toolblock;
			}
		}

		private void ToolBlockWindow_FilenameChanged(object sender, EventArgs e)
		{
			_recipeService.NowRecipe.Path = ToolBlockWindow.Filename;
		}

		private void OnAddRightImage()
		{
			// Dialog를 띄워서 이미지를 가져옴

			// 이미지를 Cailbration1에 넣음
		}

			private void OnAddLeftImage()
		{
			// Dialog를 띄워서 이미지를 가져옴

			// 이미지를 Cailbration1에 넣음
		}

		private void OnAddRecipe()
		{
			_recipeService.AddRecipe("abc");
		}

		private void OnRemoveRecipe(RecipeData recipeData)
		{
			MessageBoxResult result = MessageBox.Show("Are you sure you want to delete?", "Delete", MessageBoxButton.OKCancel, MessageBoxImage.Question);

			switch (result)
			{
				case MessageBoxResult.OK:
					_recipeService.RemoveRecipe(recipeData);
					break;

				case MessageBoxResult.Cancel:
					break;
			}
		}

		private void OnSaveRecipe()
		{
		}

		private void OnSelected(RecipeData recipeData)
		{
			if(recipeData == null)
			{
				return;
			}
			else
			{
				SelectedRecipe = recipeData;
			}

			foreach(var recipe in _recipeService.Recipes)
			{
				recipe.IsNowRecipe = false;
			}

			recipeData.IsNowRecipe = true;
			// Window에 다른 Toolblock이 있다면 Dispose
			if (ToolBlockWindow.Subject != null)
			{
				ToolBlockWindow.Subject.Dispose();
			}

			// 현재 선택한 레시피를 삭제하면 return
			if (_recipeService.NowRecipe == null)
			{
				return;
			}

			// 현재 레시피에 Toolblock 경로가 없다면 Load하지 않음
			if (_recipeService.NowRecipe.Path == null)
			{
				return;
			}

			_visionProService.Load(_recipeService.NowRecipe.Path);
		}

		#region DialogAware
		public string Title => "Recipe";
		public event Action<IDialogResult> RequestClose;
		public bool CanCloseDialog() => true;
		public void OnDialogClosed()
		{
			Dispose();
		}
		public void OnDialogOpened(IDialogParameters parameters) { }
		#endregion
		public void Dispose()
		{
			ToolBlockWindow.FilenameChanged -= ToolBlockWindow_FilenameChanged;

			_eventAggregator.GetEvent<DialogClosingEvent>().Unsubscribe(OnDialogClosing);
			_eventAggregator.GetEvent<LogoutEvent>().Unsubscribe(LogoutDialogClosed);

		}
	}
}
