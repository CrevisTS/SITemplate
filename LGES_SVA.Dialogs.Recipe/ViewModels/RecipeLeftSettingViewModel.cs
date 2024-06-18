using Cognex.VisionPro.ToolBlock;
using LGES_SVA.Core.Datas.Recipe;
using LGES_SVA.Core.Interfaces.Settings;
using LGES_SVA.Recipe.Services;
using LGES_SVA.VisionPro.Services;
using Prism.Commands;
using Prism.Regions;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Windows.Input;

namespace LGES_SVA.Dialogs.Recipe.ViewModels
{
	public class RecipeLeftSettingViewModel : INavigationAware, IDisposable
	{
		private RecipeService _recipeService;
		private VisionProService _visionProService;
		private ISettingRepository _settingRepository;
		public RecipeData Recipe { get; set; }
		public CogToolBlockEditV2 ToolBlockWindow { get; set; } = new CogToolBlockEditV2();

		public CogToolBlock ToolBlock { get; set; }
		public CogToolBlock ToolBlock2 { get; set; }
		public ICommand SelectToolPathCommand => new DelegateCommand(OnSelectedTool);
		public ICommand SelectImagePathCommand => new DelegateCommand<string>(OnSelectedImage);


		public RecipeLeftSettingViewModel(RecipeService rs, VisionProService vps, ISettingRepository sr)
		{
			_recipeService = rs;
			_visionProService = vps;
			_settingRepository = sr;

			Recipe = _recipeService.SelectedRecipe;

			try
			{
				ToolLoad(Recipe.ToolPath);
				ImageLoad();
				ToolRun();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		private void ToolRun()
		{
			if(ToolBlockWindow.Subject == null)
			{
				return;
			}

			// 처음 Run 하면 속도가 느림
			ToolBlockWindow.Subject.Run();
			ToolBlockWindow.Subject.Run();
		}

		private void ToolLoad(string path)
		{
			if (path == null) return;

			try
			{
				if (File.Exists(path))
				{
					ToolBlock = _visionProService.Load(path) as CogToolBlock;
					ToolBlockWindow.Subject = ToolBlock;
				}
				else
				{
					Recipe.ToolPath = "";
				}
			}
			catch (Exception)
			{
				throw;
			}
		}

		private void ImageLoad()
		{
			if (Recipe.LeftImagePath == null) return;
			if (Recipe.RightImagePath == null) return;

			try
			{
				if (File.Exists(Recipe.LeftImagePath))
				{
					Bitmap leftImage = new Bitmap(Recipe.LeftImagePath);
					ToolBlock.Inputs["LeftImage"].Value = _visionProService.ConvertImage(leftImage);
					leftImage.Dispose();
				}
				else
				{
					Recipe.LeftImagePath = "";
				}

				if (File.Exists(Recipe.RightImagePath))
				{
					Bitmap rightImage = new Bitmap(Recipe.RightImagePath);
					ToolBlock.Inputs["RightImage"].Value = _visionProService.ConvertImage(rightImage);
					rightImage.Dispose();
				}
				else
				{
					Recipe.RightImagePath = "";
				}
			}
			catch (Exception)
			{

				throw;
			}

		}

		private void ImageLoad(string direction, string path)
		{
			if (path == null) return;

			try
			{
				if (direction == "left")
				{
					using (Bitmap image = new Bitmap(path))
					{
						ToolBlock.Inputs["LeftImage"].Value = _visionProService.ConvertImage(image);
					}
				}
				else
				{
					using (Bitmap image = new Bitmap(path))
					{
						ToolBlock.Inputs["RightImage"].Value = _visionProService.ConvertImage(image);
					}
				}
			}
			catch (Exception)
			{

				throw;
			}
			
		}

		private void Run()
		{
		}

		private void OnSelectedTool()
		{
			// 기존에 툴이 등록되어있었는지 확인
			if(ToolBlock != null)
			{
				ToolBlock.Dispose();
				Recipe.LeftImagePath = null;
				Recipe.RightImagePath = null;
			}

			try
			{
				using (OpenFileDialog openFileDialog = new OpenFileDialog())
				{
					// 파일 확장자 필터 설정
					openFileDialog.DefaultExt = ".vpp";
					openFileDialog.Filter = "Toolblock|*.vpp|all|*.*";

					// Show open file dialog box
					DialogResult result = openFileDialog.ShowDialog();

					switch (result)
					{
						case DialogResult.OK:
							Recipe.ToolPath = openFileDialog.FileName;
							// ToolLoad
							ToolLoad(openFileDialog.FileName);
							break;

						case DialogResult.Cancel:
						default:
							break;
					}
				};
			}
			catch (Exception)
			{
				throw;
			}
		}

		private void OnSelectedImage(string direction)
		{
			try
			{
				using (OpenFileDialog openFileDialog = new OpenFileDialog())
				{
					// 파일 확장자 필터 설정
					openFileDialog.DefaultExt = ".bmp";
					openFileDialog.Filter = "image|*.bmp|all|*.*";

					// Show open file dialog box
					DialogResult result = openFileDialog.ShowDialog();

					switch (result)
					{
						case DialogResult.OK:
							if (direction == "left")
							{
								Recipe.LeftImagePath = openFileDialog.FileName;
							}
							else
							{
								Recipe.RightImagePath = openFileDialog.FileName;
							}
							ImageLoad(direction, openFileDialog.FileName);
							break;

						case DialogResult.Cancel:
						default:
							break;
					}
				};
			}
			catch (Exception)
			{

				throw;
			}
			
		}


		public bool IsNavigationTarget(NavigationContext navigationContext)
		{
			return false;
		}

		public void OnNavigatedFrom(NavigationContext navigationContext)
		{
			Dispose();
		}

		public void OnNavigatedTo(NavigationContext navigationContext)
		{
		}

		public void Dispose()
		{
			if(ToolBlockWindow != null)
			{
				ToolBlockWindow.Dispose();
			}

			if(ToolBlock != null)
			{
				ToolBlock.Dispose();
			}
		}
	}
}
