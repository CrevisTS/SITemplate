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
using Prism.Events;
using LGES_SVA.Core.Events;
using LGES_SVA.Core.Datas.Inspection;
using System;
using Cognex.VisionPro;

namespace LGES_SVA.Inspection.Services
{
    public class InspectionManager : BindableBase, IInspectionManager
    {
        private readonly IEventAggregator _eventAggregator;
        private EInspectionState _inspectionState;
        private Thread _inspectionThread;
        public EInspectionState InspectionState { get => _inspectionState; private set => SetProperty(ref _inspectionState, value); }


        private CameraManager _cameraManager;
        private RecipeService _recipeService;
		private VisionProService _visionProService;
        
        // Result

        // 검사 끝나면 검사 결과를 Event로 쏴줘야겠네

		public InspectionManager(IEventAggregator ea, CameraManager cm, RecipeService rs, VisionProService vps)
		{
            _eventAggregator = ea;
            _cameraManager = cm;
            _recipeService = rs;
            _visionProService = vps;

        }

        private void MainLogic()
		{
            Bitmap bmp1;
            Bitmap bmp2;

			try
			{
                while (EInspectionState.Stopping != InspectionState)
                {
                    // 아직 Queue에 촬영한 이미지가 없다면
                    if(_cameraManager.Cameras["Cam1"].Bitmaps.Count < 0 || _cameraManager.Cameras["Cam2"].Bitmaps.Count < 0)
					{
                        return;
					}

                    if(_cameraManager.Cameras["Cam1"].Bitmaps.TryDequeue(out bmp1) && _cameraManager.Cameras["Cam2"].Bitmaps.TryDequeue(out bmp2))
					{
                        Application.Current.Dispatcher.Invoke(() => 
                        { 
                            _recipeService.NowRecipe.ToolBlock.Inputs["LeftImage"].Value = _visionProService.ConvertImage(bmp1);
                            _recipeService.NowRecipe.ToolBlock.Inputs["RightImage"].Value = _visionProService.ConvertImage(bmp2);
                        });
                        
                        _recipeService.NowRecipe.ToolBlock.Run();
                        if (_recipeService.NowRecipe.ToolBlock.RunStatus.Result == Cognex.VisionPro.CogToolResultConstants.Error)
						{
                            var inspectResult = new InspectionResult(
                                leftImage : _recipeService.NowRecipe.ToolBlock.Inputs["LeftImage"].Value as ICogImage,
                                rightImage : _recipeService.NowRecipe.ToolBlock.Inputs["RightImage"].Value as ICogImage,
                                leftTerraceAngle: null,
                                rightTerraceAngle: null,
                                leftTerraceToLeadDistance: null,
                                rightTerraceToLeadDistance: null,
                                leftLeadAngle: null,
                                rightLeadAngle: null,
                                cellDistance: null
                            );

                            _eventAggregator.GetEvent<InspectComplateEvent>().Publish(inspectResult);

                        }
                        else
						{
                            var inspectResult = new InspectionResult(
                                leftImage: _recipeService.NowRecipe.ToolBlock.Inputs["LeftImage"].Value as ICogImage,
                                rightImage: _recipeService.NowRecipe.ToolBlock.Inputs["RightImage"].Value as ICogImage,
                                leftTerraceAngle: (double)_recipeService.NowRecipe.ToolBlock.Outputs["Left_TerraceAngle"].Value,
                                rightTerraceAngle: (double)_recipeService.NowRecipe.ToolBlock.Outputs["Right_TerraceAngle"].Value,
                                leftTerraceToLeadDistance: (double)_recipeService.NowRecipe.ToolBlock.Outputs["Left_TerraceToLeadDistance"].Value,
                                rightTerraceToLeadDistance: (double)_recipeService.NowRecipe.ToolBlock.Outputs["Right_TerraceToLeadDistance"].Value,
                                leftLeadAngle: (double)_recipeService.NowRecipe.ToolBlock.Outputs["Left_LeadAngle"].Value,
                                rightLeadAngle: (double)_recipeService.NowRecipe.ToolBlock.Outputs["Right_LeadAngle"].Value,
                                cellDistance: (double)_recipeService.NowRecipe.ToolBlock.Outputs["Cell_Distance"].Value
                            );

                            _eventAggregator.GetEvent<InspectComplateEvent>().Publish(inspectResult);

                        }
                    }
                }
            }
			catch (Exception)
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
