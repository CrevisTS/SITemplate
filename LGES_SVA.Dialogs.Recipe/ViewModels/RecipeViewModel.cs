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
	public class RecipeViewModel : BindableBase, IDialogAware
	{
		#region DialogAware
		private bool _canClose = true;
		public string Title => "Recipe";


		public event Action<IDialogResult> RequestClose;

		public bool CanCloseDialog() => _canClose;

		public void OnDialogClosed()
		{
			try
			{
				ToolBlockWindow.FilenameChanged -= ToolBlockWindow_FilenameChanged;
			}
			catch (Exception)
			{

				throw;
			}
		}

		public void OnDialogOpened(IDialogParameters parameters)
		{
		}

		#endregion

		private RecipeService _recipeService;
		private ISettingRepository _settingRepository;
		private IVisionProService _visionProService;
		private IEventAggregator _eventAggregator;

		public RecipeService RecipeService { get => _recipeService; set => SetProperty(ref _recipeService, value); }
		public CogToolBlockEditV2 ToolBlockWindow { get; set; } = new CogToolBlockEditV2();

		public ICommand AddRecipeCommand => new DelegateCommand(OnAddRecipe);
		public ICommand RemoveRecipeCommand => new DelegateCommand<RecipeData>(OnRemoveRecipe);

		public ICommand SaveRecipeCommand => new DelegateCommand(OnSaveRecipe);
		public ICommand SelectedCommand => new DelegateCommand<RecipeData>(OnSelected);
		public ICommand AddLeftImageCommand => new DelegateCommand(OnAddLeftImage);
		public ICommand AddRightImageCommand => new DelegateCommand(OnAddRightImage);


		
		public RecipeViewModel(RecipeService recipeService, ISettingRepository settingRepository, IVisionProService visionProService, IEventAggregator eventAggregator)
		{
			_recipeService = recipeService;
			_settingRepository = settingRepository;
			_visionProService = visionProService;

			ToolBlockWindowInit();
			
			ToolBlockWindow.FilenameChanged += ToolBlockWindow_FilenameChanged;

			_eventAggregator = eventAggregator;
			_eventAggregator.GetEvent<LogoutEvent>().Subscribe(() => LogoutDialogClosed());
			_eventAggregator.GetEvent<DialogClosingEvent>().Subscribe(OnDialogClosing, ThreadOption.PublisherThread, false, (filter) => filter.Item1.Equals("RecipeDialog"));

		}


		private void OnDialogClosing((string, CancelEventArgs) obj)
		{
			// 현재 레시피가 있는지 확인
			if (_recipeService.NowRecipe == null)
			{
				MessageBox.Show("No recipe selected.");
				obj.Item2.Cancel = true;

				return;
			}

			var result = MessageBox.Show("저장하시겠습니까?", "Save", MessageBoxButton.YesNoCancel, MessageBoxImage.Question);

			switch (result)
			{
				case MessageBoxResult.Yes:
					// TODO : 저장 로직
					break;

				case MessageBoxResult.No:
					break;

				case MessageBoxResult.Cancel:
					obj.Item2.Cancel = true;
					break;

			}
		}

		private void LogoutDialogClosed()
		{
			DialogClose();
		}

		private void DialogClose()
		{
			Application.Current.Dispatcher.Invoke(() =>
			{
				RequestClose?.Invoke(new DialogResult(ButtonResult.OK));
			});
		}

		public void ToolBlockWindowInit()
		{
			var toolBlockPath = _settingRepository.VisionProSetting.InspectionRecipe;

			// 현재 레시피가 없다면
			if (toolBlockPath == null)
			{
				return;
			}
			// 현재 레시피가 있다면
			else
			{
				var Toolblock = _visionProService.Load(_settingRepository.VisionProSetting.InspectionRecipe.Path);
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
			_recipeService.NowRecipe = recipeData;

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
	}
}
