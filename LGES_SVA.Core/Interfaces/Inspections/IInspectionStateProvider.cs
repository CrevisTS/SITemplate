using LGES_SVA.Core.Enums;

namespace LGES_SVA.Core.Interfaces
{
    public interface IInspectionStateProvider
    {
        EInspectionState InspectionState { get; }
    }
}
