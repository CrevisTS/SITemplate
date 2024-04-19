using LGES_SVA.Core.Enums;

namespace LGES_SVA.Core.Datas
{
    public sealed class RegionNames
    {
        public static string ControlRegion => nameof(ERegionType.ControlRegion);
        public static string MainViewRegion => nameof(ERegionType.MainViewRegion);
        public static string TabViewRegion => nameof(ERegionType.TabViewRegion);
    }
}
