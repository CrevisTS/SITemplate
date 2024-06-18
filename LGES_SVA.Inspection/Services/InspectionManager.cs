using Prism.Mvvm;
using LGES_SVA.Core.Enums;
using LGES_SVA.Core.Interfaces;
using System.Threading;
using System.Threading.Tasks;
using LGES_SVA.Recipe.Services;
using LGES_SVA.Camera.Services;
using LGES_SVA.VisionPro.Services;
using System.Drawing;
using LGES_SVA.Core.Utils;
using System.Windows;

namespace LGES_SVA.Inspection.Services
{
    public class InspectionManager : BindableBase, IInspectionManager
    {
        private EInspectionState _inspectionState;
        private Thread _inspectionThread;
        public EInspectionState InspectionState { get => _inspectionState; private set => SetProperty(ref _inspectionState, value); }


        private CameraManager _cameraManager;
        private RecipeService _recipeService;
		private VisionProService _visionProService;

		public InspectionManager(CameraManager cm, RecipeService rs, VisionProService vps)
		{
            _cameraManager = cm;
            _recipeService = rs;
            _visionProService = vps;
            
        }

        private void MainLogic()
		{
			try
			{
                while (EInspectionState.Stopping != InspectionState)
                {
                    Application.Current.Dispatcher.Invoke(() => 
                    { 
                        _recipeService.NowRecipe.ToolBlock.Inputs["LeftImage"].Value = _visionProService.ConvertImage(ImageHelper.BitmapFromWriteableBitmap(_cameraManager.Cameras["Cam1"].Bitmap.WBitmap));
                        _recipeService.NowRecipe.ToolBlock.Inputs["RightImage"].Value = _visionProService.ConvertImage(ImageHelper.BitmapFromWriteableBitmap(_cameraManager.Cameras["Cam2"].Bitmap.WBitmap));
                    });
                    
                    _recipeService.NowRecipe.ToolBlock.Run();
                }
            }
			catch (System.Exception)
			{

				throw;
			}
            
		}

        public async void InspectionStartAsync()
        {
            InspectionState = EInspectionState.Starting;
            await Task.Run(() =>
            {
                // TODO: Inspection Start 준비
                Thread.Sleep(1000);
                _cameraManager.AllAcqStart();

                _inspectionThread = new Thread(new ThreadStart(MainLogic));
                _inspectionThread.Name = "Inspection Thread";
                _inspectionThread.Start();
            });
            InspectionState = EInspectionState.Start;
        }

        public async void InspectionStopAsync()
        {
            InspectionState = EInspectionState.Stopping;
            await Task.Run(() =>
            {
                // TODO: Inspection Stop 준비
                Thread.Sleep(1000);
                _inspectionThread.Join();

                _cameraManager.AllAcqStop();
            });
            InspectionState = EInspectionState.Stop;
        }
    }
}
