using LGES_SVA.Core.Interfaces;
using System;
using System.Collections.Generic;

namespace LGES_SVA.Main.Services
{
    public class DisposeManager : IDisposeManager
    {
        private readonly List<IDisposable> _disposeValues = new List<IDisposable>();

        public DisposeManager()
        {

        }
        public void AddIDisposable(IDisposable disposeable)
        {
            _disposeValues.Add(disposeable);
        }

        public void DisposeResources()
        {
            foreach (var value in _disposeValues)
            {
                value?.Dispose();
            }
            _disposeValues.Clear();
        }
    }
}
