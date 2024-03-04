using System;

namespace SITemplate.Core.Interfaces
{
    public interface IDisposeManager
    {
        void AddIDisposable(IDisposable disposable);
        void DisposeResources();
    }
}
