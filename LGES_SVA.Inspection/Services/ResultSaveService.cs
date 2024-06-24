using LGES_SVA.Core.Datas.Inspection;
using LGES_SVA.Core.Events;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LGES_SVA.Inspection.Services
{
	public class ResultSaveService
	{
		private readonly IEventAggregator _eventAggregator;
		public ResultSaveService(IEventAggregator ea)
		{
			_eventAggregator = ea;

			_eventAggregator.GetEvent<InspectComplateResultEvent>().Subscribe(OnInspectComplate);
		}

		private void OnInspectComplate(InspectionResult obj)
		{
		}
	}
}
