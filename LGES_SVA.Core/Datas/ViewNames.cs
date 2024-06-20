using LGES_SVA.Core.Enums;

namespace LGES_SVA.Core.Datas
{
    public sealed class ViewNames
    {
        // Main Region
        public static string ControlBarView => nameof(EViewType.ControlBarView);
        public static string InspectionView => nameof(EViewType.InspectionView);
        public static string SettingView => nameof(EViewType.SettingView);
        public static string TabView => nameof(EViewType.TabView);
        public static string YieldView => nameof(EViewType.YieldView);

        // Tab Inner Region
        public static string TabGraphView => nameof(EViewType.TabGraphView);

        public static string RecipeBasicSettingView => nameof(EViewType.RecipeBasicSettingView);
        public static string RecipeLeftSettingView => nameof(EViewType.RecipeLeftSettingView);
        public static string RecipeRightSettingView => nameof(EViewType.RecipeRightSettingView);
    }
}
