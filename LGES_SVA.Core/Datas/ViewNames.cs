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

        // Tab Inner Region
        public static string TabImageView => nameof(EViewType.TabImageView);
        public static string TabResultView => nameof(EViewType.TabResultView);
        public static string TabGraphView => nameof(EViewType.TabGraphView);
    }
}
