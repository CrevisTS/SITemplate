using LGES_SVA.Core.Datas.Recipe;
using Prism.Mvvm;

namespace LGES_SVA.Core.Datas.Settings.VisionPro
{
	public class VisionProSetting : BindableBase
	{
		private RecipeData _inspectionRecipe;

		public RecipeData InspectionRecipe { get => _inspectionRecipe; set => SetProperty(ref _inspectionRecipe, value); }

		public VisionProSetting()
		{
			InspectionRecipe = new RecipeData("DefaultRecipe");
		}
	}
}
