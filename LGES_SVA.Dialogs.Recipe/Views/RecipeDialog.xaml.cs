using Cognex.VisionPro.ToolBlock;
using LGES_SVA.Core.Datas;
using Prism.Regions;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LGES_SVA.Dialogs.Recipe.Views
{
	/// <summary>
	/// RecipeDialog.xaml에 대한 상호 작용 논리
	/// </summary>
	public partial class RecipeDialog : UserControl
	{

		public RecipeDialog(IRegionManager rm)
		{
			InitializeComponent();

			// Regions에는 데이터가 남아있지만, Dialog와 연결이 끊기는것으로 보임.
			// 우선 다시 연결하는 방법을 몰라 remove하고 새로 생성하는 방법 사용함.
			// 중간에 Remove를하니 내부 Region이 Dispose가 되지않는 문제가 있음.
			
			if (rm.Regions.ContainsRegionWithName(RegionNames.RecipeSettingRegion))
			{
				rm.Regions.Remove(RegionNames.RecipeSettingRegion);
			}

			RegionManager.SetRegionName(content, RegionNames.RecipeSettingRegion);
			RegionManager.SetRegionManager(content, rm);
			RegionManager.UpdateRegions();
			
		}

	}
}
