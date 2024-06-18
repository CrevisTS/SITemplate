using Cognex.VisionPro;
using Cognex.VisionPro.Display;
using Cognex.VisionPro.Implementation;
using LGES_SVA.Camera.Services;
using LGES_SVA.Core.Interfaces;
using LGES_SVA.VisionPro.Services;
using Prism.Commands;
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
        private VisionProService _visionProService;
        private CameraManager _cameraManager;

        public ICommand InitCommand => new DelegateCommand(OnInitCogRecordDisplay);

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
                Display1.Record = _record1;

                Display1.Image = _visionProService.ConvertImage(new Bitmap(@"C:\Users\TS_jiwon\Desktop\3CMOS 펌웨어업데이트.bmp"));
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                throw;
            }
        }

		public IInspectionStateProvider InspectionStateProvider => _inspectionStateProvider;

        public CogRecordDisplay Display1 { get; set; }
        public CogRecordDisplay Display2 { get; set; }
        public CogRecordDisplay Display3 { get; set; }
        public CogRecordDisplay Display4 { get; set; }

        public CogRecord _record1 = new CogRecord();
        public InspectionViewModel(IInspectionManager inspectionManager, VisionProService vps, CameraManager cm)
        {
            _inspectionStateProvider = inspectionManager;
            _visionProService = vps;
            _cameraManager = cm;

            Display1 = new CogRecordDisplay();
            Display2 = new CogRecordDisplay();
            Display3 = new CogRecordDisplay();
            Display4 = new CogRecordDisplay();
        }
    }
}
