//using Prism.Commands;
//using Prism.Mvvm;
//using System.Threading.Tasks;
//using System.Windows.Input;

//namespace SITemplate.Core.InspectionRepoTest2123122133
//{
//    public enum EInspectionStatus
//    {
//        Stop,
//        Start,
//        Stopping,
//        Starting
//    }
//    public interface IInspectionStatus
//    {
//        EInspectionStatus InspectionStatus { get; }
//    }
//    public interface IInspectionStarter
//    {
//        void InspectionStart();
//        void InspectionStop();
//    }
//    public interface IInspectionManager : IInspectionStarter, IInspectionStatus { }
//    public class InspectionManager : BindableBase, IInspectionManager
//    {
//        private EInspectionStatus _inspectionStatus;
//        public EInspectionStatus InspectionStatus { get => _inspectionStatus; private set => SetProperty(ref _inspectionStatus, value); }

//        public void InspectionStart()
//        {
//            InspectionStatus = EInspectionStatus.Starting;
//            // Starting
//            InspectionStatus = EInspectionStatus.Start;
//        }
//        public void InspectionStop()
//        {
//            InspectionStatus = EInspectionStatus.Stopping;
//            // Starting
//            InspectionStatus = EInspectionStatus.Stop;
//        }
//    }

//    public class InspectionControlViewModel : BindableBase
//    {
//        private IInspectionManager _inspectionManager;

//        public EInspectionStatus InspectionStatus { get => _inspectionManager.InspectionStatus; }

//        public ICommand StartStopBtnClickCommand => new DelegateCommand(OnStartStopBtnClick);
//        public InspectionControlViewModel(IInspectionManager inspectionManager)
//        {
//            _inspectionManager = inspectionManager;
//        }
//        private void OnStartStopBtnClick()
//        {
//            switch (_inspectionManager.InspectionStatus)
//            {
//                case EInspectionStatus.Stop:
//                    Task.Run(() => { _inspectionManager.InspectionStart(); }); // 메인쓰레드에 자유?를 주기위해 wait 안하고 그냥 넘김
//                    break;
//                case EInspectionStatus.Start:
//                    Task.Run(() => { _inspectionManager.InspectionStop(); }); // 메인쓰레드에 자유?를 주기위해 wait 안하고 그냥 넘김
//                    break;
//                case EInspectionStatus.Stopping:
//                case EInspectionStatus.Starting:
//                default: // 아무것도 하지 않음.
//                    break;
//            }
//        }
//    }
//    public class InspectionResultViewModel : BindableBase
//    {
//        private IInspectionStatus _inspectionStatus;

//        public EInspectionStatus InspectionStatus { get => _inspectionStatus.InspectionStatus; }
//        public InspectionResultViewModel(IInspectionManager inspectionManager)
//        {
//            _inspectionStatus = inspectionManager;
//        }
//    }
//}