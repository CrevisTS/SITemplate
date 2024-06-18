using Cognex.VisionPro;
using Cognex.VisionPro.Display;
using Cognex.VisionPro.Implementation;
using LGES_SVA.Camera.Services;
using LGES_SVA.Core.Datas.Inspection;
using LGES_SVA.Core.Events;
using LGES_SVA.Core.Interfaces;
using LGES_SVA.VisionPro.Services;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Drawing;
using System.Windows;
using System.Windows.Input;

namespace LGES_SVA.Inspection.ViewModels
{
	public class InspectionViewModel : BindableBase
    {
        private readonly IInspectionStateProvider _inspectionStateProvider;
        private readonly IEventAggregator _eventAggregator;

        private VisionProService _visionProService;
        private CameraManager _cameraManager;

        public ICommand InitCommand => new DelegateCommand(OnInitCogRecordDisplay);
		
		public IInspectionStateProvider InspectionStateProvider => _inspectionStateProvider;

        public CogRecordDisplay Display1 { get; set; }
        public CogRecordDisplay Display2 { get; set; }
        public CogRecordDisplay Display3 { get; set; }
        public CogRecordDisplay Display4 { get; set; }

        public CogRecord _record1 = new CogRecord();
        public CogRecord _record2 = new CogRecord();
        public InspectionViewModel(IInspectionManager im, IEventAggregator ea, VisionProService vps, CameraManager cm)
        {
            _inspectionStateProvider = im;
            _eventAggregator = ea;
            _visionProService = vps;
            _cameraManager = cm;

            _eventAggregator.GetEvent<InspectComplateEvent>().Subscribe(OnInspectComplate);

            Display1 = new CogRecordDisplay();
            Display2 = new CogRecordDisplay();
            Display3 = new CogRecordDisplay();
            Display4 = new CogRecordDisplay();
        }

		private void OnInspectComplate(InspectionResult obj)
		{
            // 검사 결과 Display
            Display1.Image = obj.LeftImage;
            Display2.Image = obj.RightImage;
        }

		private void OnInitCogRecordDisplay()
        {
            try
            {
                Display1.HorizontalScrollBar = false;
                Display1.VerticalScrollBar = false;
                Display2.HorizontalScrollBar = false;
                Display2.VerticalScrollBar = false;
                Display3.HorizontalScrollBar = false;
                Display3.VerticalScrollBar = false;
                Display4.HorizontalScrollBar = false;
                Display4.VerticalScrollBar = false;

                _record1.Content = Display1.Image;
                _record2.Content = Display1.Image;
                Display1.Record = _record1;
                Display2.Record = _record2;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                throw;
            }
        }
    }
}
