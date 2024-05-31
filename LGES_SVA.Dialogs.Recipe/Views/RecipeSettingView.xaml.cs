using LGES_SVA.Core.Datas;
using Prism.Regions;
using System;
using System.Collections.Generic;
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
	/// RecipeRegion.xaml에 대한 상호 작용 논리
	/// </summary>
	public partial class RecipeSettingView : UserControl
	{
		public RecipeSettingView(IRegionManager rm)
		{
			InitializeComponent();

			if (rm.Regions.ContainsRegionWithName(RegionNames.RecipeSettingInnerRegion))
			{
				rm.Regions.Remove(RegionNames.RecipeSettingInnerRegion);
			}
			RegionManager.SetRegionName(content, RegionNames.RecipeSettingInnerRegion);
			RegionManager.SetRegionManager(content, rm);
			RegionManager.UpdateRegions();
			
		}
	}
}
