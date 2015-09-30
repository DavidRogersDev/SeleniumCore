using KesselRun.SeleniumCore.Infrastructure;
using OpenQA.Selenium.Chrome;

namespace KesselRun.SeleniumCore.TestDrivers.Browsers.Chrome
{
    public class ChromeTestDriver : BaseTestDriver
    {
        public override void Initialize(DriverOptions driverOptions)
        {
            //  Do not set implicit waits. Prefer usage of explicit waits.
            //  Can always turn on implicit waits at a later time, but once set, they cannot be changed.

            DefaultUrl = driverOptions.Url;

            ChromeDriverService chromeDriverService = ChromeDriverService.CreateDefaultService(driverOptions.DriverExePath);
            var chromeOptions = new ChromeOptions();

            chromeDriverService.Port = driverOptions.Port; // this is the port for the driver, not the webpage

            WebDriver = new ChromeDriver(chromeDriverService, chromeOptions);
        }

    }
}
