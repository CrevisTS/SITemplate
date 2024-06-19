using LGES_SVA.Core.Enums;

namespace LGES_SVA.Core.Datas
{
    public sealed class RegionNames
    {
        // Main UI
        public static string ControlRegion => nameof(ERegionType.ControlRegion);
        public static string MainViewRegion => nameof(ERegionType.MainViewRegion);
        public static string TabViewRegion => nameof(ERegionType.TabViewRegion);
        public static string YieldRegion => nameof(ERegionType.YieldRegion);

        // Tab View
        public static string TabInnerRegion => nameof(ERegionType.TabInnerRegion);

        public static string RecipeSettingRegion => nameof(ERegionType.RecipeSettingRegion);
    }
}
