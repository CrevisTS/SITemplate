using Cognex.VisionPro;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LGES_SVA.Core.Datas.Inspection
{
	public class InspectionResult : BindableBase
	{
		private ICogImage _leftImage;
		private ICogImage _rightImage;

		private ICogRecords _leftRecord;
		private ICogRecords _rightRecord;
		// 1. 검사 안함
		
		// 2. Master Jig Center부터 Cell Center Position

		// 3. Jig Line에서부터 테라스 끝의 길이

		// 4. Left Terrace의 각도
		private double? _leftTerraceAngle;
		// 4. Right Terrace의 각도
		private double? _rightTerraceAngle;

		// 5. Left Terrace 중앙에서 부터 Lead 중앙까지의 길이
		private double? _leftTerraceToLeadDistance;
		// 5. Right Terrace 중앙에서 부터 Lead 중앙까지의 길이
		private double? _rightTerraceToLeadDistance;

		// 6. Left Lead의 각도
		private double? _leftLeadAngle;
		// 6. Right Lead의 각도
		private double? _rightLeadAngle;

		// 7. Left Terrace 중앙에서 부터 Right Terrace 중앙까지의 길이
		private double? _cellDistance;


		// 8. 검사 안함

		// 9. Center Gap


		private EInspectResult _inspectResult;

		public ICogImage LeftImage { get => _leftImage; set => SetProperty(ref _leftImage, value); }
		public ICogImage RightImage { get => _rightImage; set => SetProperty(ref _rightImage, value); }
		public ICogRecords LeftRecord { get => _leftRecord; set => SetProperty(ref _leftRecord, value); }
		public ICogRecords RightRecord { get => _rightRecord; set => SetProperty(ref _rightRecord, value); }
		public InspectionResult() { }

		public InspectionResult(ICogImage leftImage, ICogImage rightImage, ICogRecords leftRecord, ICogRecords rightRecord)
		{
			LeftImage = leftImage;
			RightImage = rightImage;
			LeftRecord = leftRecord;
			RightRecord = rightRecord;
		}

		public InspectionResult( double? leftTerraceAngle, double? rightTerraceAngle, double? leftTerraceToLeadDistance, double? rightTerraceToLeadDistance, double? leftLeadAngle, double? rightLeadAngle, double? cellDistance, EInspectResult result)
		{
			_leftTerraceAngle = leftTerraceAngle;
			_rightTerraceAngle = rightTerraceAngle;
			_leftTerraceToLeadDistance = leftTerraceToLeadDistance;
			_rightTerraceToLeadDistance = rightTerraceToLeadDistance;
			_leftLeadAngle = leftLeadAngle;
			_rightLeadAngle = rightLeadAngle;
			_cellDistance = cellDistance;

			_inspectResult = result;
		}
	}

	public enum EInspectResult
	{
		OK,
		NG,
		Pass
	}
}
