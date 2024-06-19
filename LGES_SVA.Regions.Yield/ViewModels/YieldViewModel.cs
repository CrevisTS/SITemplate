using LGES_SVA.Inspection.Services;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LGES_SVA.Regions.Yield.ViewModels
{
	public class YieldViewModel : BindableBase
	{
		private InspectionManager _inspectionManager;
		public InspectionManager InspectionManager { get => _inspectionManager; set => SetProperty(ref _inspectionManager, value); }
		public YieldViewModel(InspectionManager im)
		{
			InspectionManager = im;
		}
	}
}
