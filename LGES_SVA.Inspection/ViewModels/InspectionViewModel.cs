using Prism.Mvvm;
using LGES_SVA.Core.Interfaces;

namespace LGES_SVA.Inspection.ViewModels
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
