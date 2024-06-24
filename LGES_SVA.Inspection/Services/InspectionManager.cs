using Cognex.VisionPro;
using LGES_SVA.Camera.Services;
using LGES_SVA.Core.Datas.Inspection;
using LGES_SVA.Core.Enums;
using LGES_SVA.Core.Events;
using LGES_SVA.Core.Interfaces;
using LGES_SVA.Recipe.Services;
using LGES_SVA.VisionPro.Services;
using Prism.Events;
using Prism.Mvvm;
using System;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

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

		public InspectionManager(IEventAggregator ea, CameraManager cm, RecipeService rs, VisionProService vps)
        {
            _eventAggregator = ea;
            _cameraManager = cm;
            _recipeService = rs;
            _visionProService = vps;
        }

        private void InspectionThreadLogic()
        {
            Bitmap bmp1;
            Bitmap bmp2;

            try
            {
                while (EInspectionState.Stopping != InspectionState)
                {
                    if (_cameraManager.Cameras.Count == 0)
                    {
                        MessageBox.Show("연결된 카메라가 없습니다.");
                        break;
                    }

                    // 아직 Queue에 촬영한 이미지가 없다면
                    if (_cameraManager.Cameras["Cam1"].Bitmaps.Count < 0 || _cameraManager.Cameras["Cam2"].Bitmaps.Count < 0)
                    {
                        return;
                    }

                    if (_cameraManager.Cameras["Cam1"].Bitmaps.TryDequeue(out bmp1) && _cameraManager.Cameras["Cam2"].Bitmaps.TryDequeue(out bmp2))
                    {
                        MainLogic(bmp1, bmp2);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

        }

        public void MainLogic(Bitmap bmp1, Bitmap bmp2)
		{
            Application.Current.Dispatcher.Invoke(() =>
            {
                _recipeService.NowRecipe.ToolBlock.Inputs["LeftImage"].Value = _visionProService.ConvertImage(new Bitmap(@"D:\7. 프로젝트\IPC_SmartVision\240516_프로그램 제작 중\Exp5000\left_cell.bmp"));
                _recipeService.NowRecipe.ToolBlock.Inputs["RightImage"].Value = _visionProService.ConvertImage(new Bitmap(@"D:\7. 프로젝트\IPC_SmartVision\240516_프로그램 제작 중\Exp5000\right_cell.bmp"));
                //_recipeService.NowRecipe.ToolBlock.Inputs["LeftImage"].Value = _visionProService.ConvertImage(bmp1);
                //_recipeService.NowRecipe.ToolBlock.Inputs["RightImage"].Value = _visionProService.ConvertImage(bmp2);
            });

            _recipeService.NowRecipe.ToolBlock.Run();
            if (_recipeService.NowRecipe.ToolBlock.RunStatus.Result == CogToolResultConstants.Error)
            {
                // UI 출력 데이터
                var imageResult = new InspectionResult(
                    leftRecord: _recipeService.NowRecipe.ToolBlock.CreateLastRunRecord().SubRecords,
                    rightRecord: _recipeService.NowRecipe.ToolBlock.CreateLastRunRecord().SubRecords,
                    leftImage: _recipeService.NowRecipe.ToolBlock.Outputs["LeftImage"].Value as ICogImage,
                    rightImage: _recipeService.NowRecipe.ToolBlock.Outputs["RightImage"].Value as ICogImage
                    );

                // csv 저장 데이터
                var inspectResult = new InspectionResult(
                    leftTerraceAngle: (double)_recipeService.NowRecipe.ToolBlock.Outputs["Left_TerraceAngle"].Value,
                    rightTerraceAngle: (double)_recipeService.NowRecipe.ToolBlock.Outputs["Right_TerraceAngle"].Value,
                    leftTerraceToLeadDistance: (double)_recipeService.NowRecipe.ToolBlock.Outputs["Left_TerraceToLeadDistance"].Value,
                    rightTerraceToLeadDistance: (double)_recipeService.NowRecipe.ToolBlock.Outputs["Right_TerraceToLeadDistance"].Value,
                    leftLeadAngle: (double)_recipeService.NowRecipe.ToolBlock.Outputs["Left_LeadAngle"].Value,
                    rightLeadAngle: (double)_recipeService.NowRecipe.ToolBlock.Outputs["Right_LeadAngle"].Value,
                    cellDistance: (double)_recipeService.NowRecipe.ToolBlock.Outputs["Cell_Distance"].Value,
                    result: EInspectResult.OK
                );

                _eventAggregator.GetEvent<InspectComplateImageEvent>().Publish(inspectResult);

            }
            else
            {
                var imageResult = new InspectionResult(
                    leftRecord: _recipeService.NowRecipe.ToolBlock.CreateLastRunRecord().SubRecords,
                    rightRecord: _recipeService.NowRecipe.ToolBlock.CreateLastRunRecord().SubRecords,
                    leftImage: _recipeService.NowRecipe.ToolBlock.Outputs["LeftImage"].Value as ICogImage,
                    rightImage: _recipeService.NowRecipe.ToolBlock.Outputs["RightImage"].Value as ICogImage
                    );

                var inspectResult = new InspectionResult(
                    leftTerraceAngle: (double)_recipeService.NowRecipe.ToolBlock.Outputs["Left_TerraceAngle"].Value,
                    rightTerraceAngle: (double)_recipeService.NowRecipe.ToolBlock.Outputs["Right_TerraceAngle"].Value,
                    leftTerraceToLeadDistance: (double)_recipeService.NowRecipe.ToolBlock.Outputs["Left_TerraceToLeadDistance"].Value,
                    rightTerraceToLeadDistance: (double)_recipeService.NowRecipe.ToolBlock.Outputs["Right_TerraceToLeadDistance"].Value,
                    leftLeadAngle: (double)_recipeService.NowRecipe.ToolBlock.Outputs["Left_LeadAngle"].Value,
                    rightLeadAngle: (double)_recipeService.NowRecipe.ToolBlock.Outputs["Right_LeadAngle"].Value,
                    cellDistance: (double)_recipeService.NowRecipe.ToolBlock.Outputs["Cell_Distance"].Value,
                    result: EInspectResult.OK
                );

                _eventAggregator.GetEvent<InspectComplateImageEvent>().Publish(imageResult);
                _eventAggregator.GetEvent<InspectComplateResultEvent>().Publish(imageResult);

            }
        }

        public async void InspectionStartAsync()
        {
            InspectionState = EInspectionState.Starting;
            await Task.Run(() =>
            {
                // 그냥 대기
                Thread.Sleep(100);

                if (_cameraManager.Cameras.Count != 0)
                {
                    _cameraManager.AllAcqStart();
                }

				_inspectionThread = new Thread(new ThreadStart(InspectionThreadLogic))
				{
					Name = "Inspection Thread"
				};
				_inspectionThread.Start();
            });
            InspectionState = EInspectionState.Start;
        }

        public async void InspectionStopAsync()
        {
            InspectionState = EInspectionState.Stopping;
            await Task.Run(() =>
            {
                // 그냥 대기
                Thread.Sleep(100);

                _inspectionThread.Join();

                if (_cameraManager.Cameras.Count != 0)
                {
                    _cameraManager.AllAcqStop();
                }
            });
            InspectionState = EInspectionState.Stop;
        }
    }
}
