using KesselRun.SeleniumCore.Infrastructure;
using OpenQA.Selenium.Firefox;

namespace KesselRun.SeleniumCore.TestDrivers.Browsers.Firefox
{
    public class FirefoxTestDriver : BaseTestDriver
    {
        public override void Initialize(DriverOptions driverOptions)
        {
            DefaultUrl = driverOptions.Url;

            var firefoxBinary = new FirefoxBinary(driverOptions.DriverExePath);
            var firefoxProfile = new FirefoxProfile();

            WebDriver = new FirefoxDriver(firefoxBinary, firefoxProfile);
            TurnOnImplicitWait(null);
            TurnOnScriptWait(null);
        }
    }
}

