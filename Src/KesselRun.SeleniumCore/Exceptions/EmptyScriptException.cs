using System;

namespace KesselRun.SeleniumCore.Exceptions
{
    public class EmptyScriptException : Exception
    {
        public EmptyScriptException(string message)
            : base(message)
        {
            
        }
    }
}
