using KesselRun.SeleniumCore.Infrastructure;
using OpenQA.Selenium.Firefox;

namespace KesselRun.SeleniumCore.TestDrivers.Browsers.Firefox
{
    public class FirefoxTestDriver : BaseTestDriver
    {
        public override void Initialize(DriverOptions driverOptions)
        {
            //  Do not set implicit waits. Prefer usage of explicit waits.
            //  Can always turn on implicit waits at a later time, but once set, they cannot be changed.

            DefaultUrl = driverOptions.Url;

            var firefoxBinary = new FirefoxBinary(driverOptions.DriverExePath);
            var firefoxProfile = new FirefoxProfile();

            WebDriver = new FirefoxDriver(firefoxBinary, firefoxProfile);
        }
    }
}

