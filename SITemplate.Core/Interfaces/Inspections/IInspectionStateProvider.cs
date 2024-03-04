using SITemplate.Core.Enums;

namespace SITemplate.Core.Interfaces
{
    public interface IInspectionStateProvider
    {
        EInspectionState InspectionState { get; }
    }
}
