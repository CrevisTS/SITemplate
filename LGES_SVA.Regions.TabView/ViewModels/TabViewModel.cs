using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LGES_SVA.Core.Datas;
using LGES_SVA.Regions.TabView.Views;
using System.Windows.Input;
using Prism.Commands;

namespace LGES_SVA.Regions.TabView.ViewModels
{
	public class TabViewModel
	{
		private IRegionManager _regionManager;

		public ICommand ImageCommand => new DelegateCommand(OnImageViewShow);
		public ICommand ResultCommand => new DelegateCommand(OnResultViewShow);
		public ICommand GraphCommand => new DelegateCommand(OnGrahpViewShow);

		private void OnGrahpViewShow()
		{
			_regionManager.RequestNavigate(RegionNames.TabInnerRegion, ViewNames.TabGraphView);
		}

		private void OnImageViewShow()
		{
			_regionManager.RequestNavigate(RegionNames.TabInnerRegion, ViewNames.TabImageView);
		}


		private void OnResultViewShow()
		{
			_regionManager.RequestNavigate(RegionNames.TabInnerRegion, ViewNames.TabResultView);
		}

		public TabViewModel(IRegionManager rm)
		{
				_regionManager = rm;
		}
	}
}
