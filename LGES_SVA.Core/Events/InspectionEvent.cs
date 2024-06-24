using Cognex.VisionPro;
using LGES_SVA.Core.Datas.Inspection;
using Prism.Events;

namespace LGES_SVA.Core.Events
{
	/// <summary>
	/// 검사 완료 이벤트 - 이미지 전달(이미지 디스플레이 용)
	/// </summary>
	public class InspectComplateImageEvent : PubSubEvent<InspectionResult> { }

	/// <summary>
	/// 검사 완료 이벤트 - 검사 결과 전달(CSV, Save용)
	/// </summary>
	public class InspectComplateResultEvent : PubSubEvent<InspectionResult> { }
}
