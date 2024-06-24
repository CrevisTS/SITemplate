using Cognex.VisionPro.ToolBlock;
using LGES_SVA.Core.Datas;
using LGES_SVA.Core.Datas.Recipe;
using LGES_SVA.Core.Events;
using LGES_SVA.Core.Interfaces.Settings;
using LGES_SVA.Recipe.Services;
using LGES_SVA.VisionPro.Services;
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
		private readonly ISettingRepository _settingRepository;
		private readonly VisionProService _visionProService;
		private readonly IEventAggregator _eventAggregator;
		private readonly IDialogService _dialogService;
		private readonly IRegionManager _regionManager;

		private RecipeService _recipeService;
		private RecipeData _selectedRecipe;

		private bool _isToolSettingEnabled;
		private bool _isBasicSettingEnabled;

		public bool IsToolSettingEnabled { get => _isToolSettingEnabled; set => SetProperty(ref _isToolSettingEnabled, value); } 
		public bool IsBasicSettingEnabled { get => _isBasicSettingEnabled; set => SetProperty(ref _isBasicSettingEnabled, value); } 

		public RecipeService RecipeService { get => _recipeService; set => SetProperty(ref _recipeService, value); }
		public RecipeData SelectedRecipe { get => _selectedRecipe; set => SetProperty(ref _selectedRecipe, value); }
		public CogToolBlockEditV2 ToolBlockWindow { get; set; } = new CogToolBlockEditV2();

		public ICommand AddRecipeCommand => new DelegateCommand(OnAddRecipe);
		public ICommand RemoveRecipeCommand => new DelegateCommand<RecipeData>(OnRemoveRecipe);
		/// <summary>
		/// 현재 레시피로 선택 할 때
		/// </summary>
		public ICommand NowRecipeCommand => new DelegateCommand<RecipeData>(OnSelectNowRecipe);
		public ICommand CloseCommand => new DelegateCommand(OnDialogClose);

		public ICommand BasicSettingCommand => new DelegateCommand(OnBasicSettingCommand).ObservesCanExecute(() => IsBasicSettingEnabled);
		public DelegateCommand ToolSettingCommand => new DelegateCommand(OnToolSettingCommand).ObservesCanExecute(() => IsToolSettingEnabled);


		public RecipeDialogViewModel(RecipeService rs, ISettingRepository sr, VisionProService vp, IEventAggregator ea, IDialogService ds, IRegionManager rm )
		{
			_recipeService = rs;
			_settingRepository = sr;
			_visionProService = vp;
			_eventAggregator = ea;
			_dialogService = ds;
			_regionManager = rm;

			_eventAggregator.GetEvent<LogoutEvent>().Subscribe(() => LogoutDialogClosed());
			_eventAggregator.GetEvent<DialogClosingEvent>().Subscribe(OnDialogClosing, ThreadOption.PublisherThread, false, (filter) => filter.Item1.Equals("RecipeDialog"));

			// Recipe 설정 초기값
			_recipeService.SelectedRecipe = _recipeService.NowRecipe;
			_regionManager.RequestNavigate(RegionNames.RecipeSettingRegion, ViewNames.RecipeBasicSettingView);

			IsBasicSettingEnabled = false;
			IsToolSettingEnabled = true;
		}

		private void OnBasicSettingCommand()
		{
			_regionManager.RequestNavigate(RegionNames.RecipeSettingRegion, ViewNames.RecipeBasicSettingView);
			
			IsBasicSettingEnabled = false;
			IsToolSettingEnabled = true;
		}

		private void OnToolSettingCommand()
		{
			_regionManager.RequestNavigate(RegionNames.RecipeSettingRegion, ViewNames.RecipeLeftSettingView);

			IsBasicSettingEnabled = true;
			IsToolSettingEnabled = false;
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
			_eventAggregator.GetEvent<DialogClosingEvent>().Unsubscribe(OnDialogClosing);
			_eventAggregator.GetEvent<LogoutEvent>().Unsubscribe(LogoutDialogClosed);
		}
	}
}
