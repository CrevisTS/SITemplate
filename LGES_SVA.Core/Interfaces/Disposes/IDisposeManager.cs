using System;

namespace LGES_SVA.Core.Interfaces
{
    public interface IDisposeManager
    {
        void AddIDisposable(IDisposable disposable);
        void DisposeResources();
    }
}
