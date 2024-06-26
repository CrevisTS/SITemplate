﻿using Cognex.VisionPro;
using Cognex.VisionPro.Implementation;
using LGES_SVA.Camera.Services;
using LGES_SVA.Core.Datas.Inspection;
using LGES_SVA.Core.Events;
using LGES_SVA.Core.Interfaces;
using LGES_SVA.VisionPro.Services;
using Prism.Commands;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace LGSE_SVA.Regions.Inspection.ViewModels
{
	public class InspectionViewModel
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

            _eventAggregator.GetEvent<InspectComplateImageEvent>().Subscribe(OnInspectComplate);

            Display1 = new CogRecordDisplay();
            Display2 = new CogRecordDisplay();
            Display3 = new CogRecordDisplay();
            Display4 = new CogRecordDisplay();
        }

        private void OnInspectComplate(InspectionResult obj)
        {
            _record1.SubRecords.Clear();
            _record2.SubRecords.Clear();

                foreach (var rec in obj.LeftRecord)
                {
                    _record1.SubRecords.Add(rec);
                }
                foreach (var rec in obj.RightRecord)
                {
                    _record2.SubRecords.Add(rec);
                }

            _record1.Content = obj.LeftImage;
            _record2.Content = obj.RightImage;

            Display1.Record = _record1;
            Display2.Record = _record2;

            // 검사 결과 Display
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

                Display1.AutoFit = true;
                Display2.AutoFit = true;
                Display3.AutoFit = true;
                Display4.AutoFit = true;

                _record1.Content = Display1.Image;
                _record2.Content = Display2.Image;
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
