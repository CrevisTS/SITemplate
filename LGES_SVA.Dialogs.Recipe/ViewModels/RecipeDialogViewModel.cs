using Cognex.VisionPro;
using Cognex.VisionPro.ToolBlock;
using LGES_SVA.Core.Datas;
using LGES_SVA.Core.Datas.Recipe;
using LGES_SVA.Core.Events;
using LGES_SVA.Core.Interfaces.Modules.VisionPro;
using LGES_SVA.Core.Interfaces.Settings;
using LGES_SVA.Recipe.Services;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using Prism.Services.Dialogs;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace LGES_SVA.Dialogs.Recipe.ViewModels
{
	public class RecipeDialogViewModel : BindableBase, IDialogAware, IDisposable
	{
		private ISettingRepository _settingRepository;
		private IVisionProService _visionProService;
		private IEventAggregator _eventAggregator;
		private IDialogService _dialogService;
		private IRegionManager _regionManager;

		private RecipeService _recipeService;
		private RecipeData _selectedRecipe;

		public RecipeService RecipeService { get => _recipeService; set => SetProperty(ref _recipeService, value); }
		public RecipeData SelectedRecipe { get => _selectedRecipe; set => SetProperty(ref _selectedRecipe, value); }
		public CogToolBlockEditV2 ToolBlockWindow { get; set; } = new CogToolBlockEditV2();

		public ICommand AddRecipeCommand => new DelegateCommand(OnAddRecipe);
		public ICommand RemoveRecipeCommand => new DelegateCommand<RecipeData>(OnRemoveRecipe);
		/// <summary>
		/// 현재 레시피로 선택 할 때
		/// </summary>
		public ICommand NowRecipeCommand => new DelegateCommand<RecipeData>(OnSelectNowRecipe);
		/// <summary>
		/// 설정을 위해 단순 레시피 선택 할 때
		/// </summary>
		public ICommand SelectedCommand => new DelegateCommand<RecipeData>(OnSelected);
		public ICommand CloseCommand => new DelegateCommand(OnDialogClose);

		public ICommand BasicSettingCommand => new DelegateCommand(OnBasicSettingCommand);
		public ICommand LeftSettingCommand => new DelegateCommand(OnLeftSettingCommand);
		public ICommand RightSettingCommand => new DelegateCommand(OnRightSettingCommand);


		public RecipeDialogViewModel(RecipeService rs, ISettingRepository sr, IVisionProService vp, IEventAggregator ea, IDialogService ds, IRegionManager rm )
		{
			_recipeService = rs;
			_settingRepository = sr;
			_visionProService = vp;
			_eventAggregator = ea;
			_dialogService = ds;
			_regionManager = rm;

			ToolBlockWindowInit();
			
			// 툴블럭 파일명 변경 시 새로 Load하기 위해
			ToolBlockWindow.FilenameChanged += ToolBlockWindow_FilenameChanged;

			_eventAggregator.GetEvent<LogoutEvent>().Subscribe(() => LogoutDialogClosed());
			_eventAggregator.GetEvent<DialogClosingEvent>().Subscribe(OnDialogClosing, ThreadOption.PublisherThread, false, (filter) => filter.Item1.Equals("RecipeDialog"));

			// Recipe 설정 초기값
			_recipeService.SelectedRecipe = _recipeService.NowRecipe;
			_regionManager.RequestNavigate(RegionNames.RecipeSettingRegion, ViewNames.RecipeBasicSettingView);

		}

		private void OnBasicSettingCommand()
		{
			_regionManager.RequestNavigate(RegionNames.RecipeSettingRegion, ViewNames.RecipeBasicSettingView);
		}

		private void OnLeftSettingCommand()
		{
			_regionManager.RequestNavigate(RegionNames.RecipeSettingRegion, ViewNames.RecipeLeftSettingView);
		}
		private void OnRightSettingCommand()
		{
			_regionManager.RequestNavigate(RegionNames.RecipeSettingRegion, ViewNames.RecipeRightSettingView);
		}

		private void OnDialogClosing((string, CancelEventArgs) obj)
		{
			string title = obj.Item1;
			CancelEventArgs e = obj.Item2;

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
				CogToolBlock Toolblock = _visionProService.Load(_settingRepository.VisionProSetting.InspectionRecipe.Path) as CogToolBlock;
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
			DialogParameters parameter = new DialogParameters();
			_dialogService.ShowDialog(DialogNames.NewRecipeDialog, parameter, AddRecipeCallback);
		}

		private void AddRecipeCallback(IDialogResult dialogResult)
		{
			string name = dialogResult.Parameters.GetValue<string>("Name");
			string dateTime = $"{DateTime.Now:MMddHHmm}";
			DateTime.Now.ToString();
			_recipeService.AddRecipe($"{name}_{dateTime}");


		}

		private void OnSelectNowRecipe(RecipeData recipeData)
		{
			_recipeService.SelectNowRecipe(recipeData);
			_recipeService.FindNowRecipe();
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
				_recipeService.SelectedRecipe = recipeData;
				_regionManager.RequestNavigate(RegionNames.RecipeSettingRegion, ViewNames.RecipeBasicSettingView);
			}

			// 현재 선택한 레시피를 삭제하면 return
			if (_recipeService.NowRecipe == null)
			{
				return;
			}
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
