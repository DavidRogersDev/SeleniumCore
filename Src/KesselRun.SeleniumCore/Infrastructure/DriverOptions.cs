using System;
using KesselRun.SeleniumCore.Enums;

namespace KesselRun.SeleniumCore.Infrastructure
{
    public struct DriverOptions
    {
        public string DriverExePath { get; set; }
        public DriverEngine DriverEngine { get; set; }
        public int Port { get; set; }
        public string Url { get; set; }
    }
}
