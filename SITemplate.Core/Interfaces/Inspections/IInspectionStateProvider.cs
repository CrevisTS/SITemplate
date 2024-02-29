using SITemplate.Core.Enums;


namespace SITemplate.Core.Interfaces.Inspections
{
    public interface IInspectionStateProvider
    {
        EInspectionState InspectionState { get; }
    }
}
