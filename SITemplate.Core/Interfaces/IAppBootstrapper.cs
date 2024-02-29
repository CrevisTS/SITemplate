using SITemplate.Core.Events;
using System;
using System.Threading.Tasks;

namespace SITemplate.Core.Interfaces
{
    public interface IAppBootstrapper
    {
        bool IsFail { get; }

        event EventHandler<ProgressMessageEventArgs> WindowLoadedControl;
        event EventHandler WindowLoadedCompleted;
        Task InitializeAsync();
    }
}
