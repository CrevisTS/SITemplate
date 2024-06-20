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

		public ICommand GraphCommand => new DelegateCommand(OnGrahpViewShow);

		private void OnGrahpViewShow()
		{
			_regionManager.RequestNavigate(RegionNames.TabInnerRegion, ViewNames.TabGraphView);
		}

		public TabViewModel(IRegionManager rm)
		{
				_regionManager = rm;
		}
	}
}
