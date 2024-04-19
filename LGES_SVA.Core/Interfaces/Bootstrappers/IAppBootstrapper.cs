using LGES_SVA.Core.Events;
using System;
using System.Threading.Tasks;

namespace LGES_SVA.Core.Interfaces
{
    public interface IAppBootstrapper
    {
        bool IsFail { get; }

        event EventHandler<ProgressMessageEventArgs> WindowLoadedControl;
        event EventHandler WindowLoadedCompleted;
        Task InitializeAsync();
    }
}
