using LGES_SVA.Core.Datas.Inspection;
using LGES_SVA.Core.Events;
using Prism.Events;
using Prism.Mvvm;
using System;

namespace LGES_SVA.Regions.Yield.ViewModels
{
	public class YieldViewModel : BindableBase
	{
		private readonly IEventAggregator _eventAggregator;

		public int TotalCount { get; set; }
		public int OKCount { get; set; }
		public int NGCount { get; set; }
		public double YieldValue { get; set; }

		public YieldViewModel(IEventAggregator ea)
		{
			_eventAggregator = ea;
			_eventAggregator.GetEvent<InspectComplateResultEvent>().Subscribe(OnInspectComplate);
		}

		private void OnInspectComplate(InspectionResult obj)
		{
		}
	}
}
