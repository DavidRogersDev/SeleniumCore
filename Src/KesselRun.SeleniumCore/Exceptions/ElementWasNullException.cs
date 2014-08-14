using System;
using KesselRun.SeleniumCore.Enums;

namespace KesselRun.SeleniumCore.Exceptions
{
    public class ElementWasNullException : Exception
    {
        public readonly FinderStrategy AttemptedFindStrategy;
        public readonly string DomElementSearched;
        public readonly int? Wait;

        public ElementWasNullException(FinderStrategy attemptedFindStrategy, string domElementSearched, int? wait)
        {
            AttemptedFindStrategy = attemptedFindStrategy;
            DomElementSearched = domElementSearched;
            Wait = wait ?? -1;
        }
    }
}