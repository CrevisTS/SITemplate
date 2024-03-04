using Prism.Mvvm;
using SITemplate.Core.Interfaces;

namespace SITemplate.Inspection.ViewModels
{
    public class InspectionViewModel : BindableBase
    {
        private readonly IInspectionStateProvider _inspectionStateProvider;

        public IInspectionStateProvider InspectionStateProvider => _inspectionStateProvider;

        public InspectionViewModel(IInspectionManager inspectionManager)
        {
            _inspectionStateProvider = inspectionManager;
        }
    }
}
