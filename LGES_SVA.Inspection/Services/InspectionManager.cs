using Prism.Mvvm;
using LGES_SVA.Core.Enums;
using LGES_SVA.Core.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace LGES_SVA.Inspection.Services
{
    public class InspectionManager : BindableBase, IInspectionManager
    {
        private EInspectionState _inspectionState;

        public EInspectionState InspectionState { get => _inspectionState; private set => SetProperty(ref _inspectionState, value); }

        public async void InspectionStartAsync()
        {
            InspectionState = EInspectionState.Starting;
            await Task.Run(() =>
            {
                // TODO: Inspection Start 준비
                Thread.Sleep(1000);
            });
            InspectionState = EInspectionState.Start;
        }

        public async void InspectionStopAsync()
        {
            InspectionState = EInspectionState.Stopping;
            await Task.Run(() =>
            {
                // TODO: Inspection Stop 준비
                Thread.Sleep(1000);
            });
            InspectionState = EInspectionState.Stop;
        }
    }
}
