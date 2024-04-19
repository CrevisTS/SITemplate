using CvsService.Core.Error.Exceptions;
using System;

namespace LGES_SVA.Splash.Error
{
    public class BootstrapperException : CvsServiceException
    {
        public BootstrapperException(string failName, Exception ex) : base($"Loade {failName} Fail!\nInnder Exception : {ex}") { }
        public override string Title { get; protected set; } = "Load Error";
    }
}