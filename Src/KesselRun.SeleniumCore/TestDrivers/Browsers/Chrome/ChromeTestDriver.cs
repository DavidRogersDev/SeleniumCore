using KesselRun.SeleniumCore.Infrastructure;
using OpenQA.Selenium.Chrome;

namespace KesselRun.SeleniumCore.TestDrivers.Browsers.Chrome
{
    public class ChromeTestDriver : BaseTestDriver
    {
        public override void Initialize(DriverOptions driverOptions)
        {
            DefaultUrl = driverOptions.Url;

            ChromeDriverService chromeDriverService = ChromeDriverService.CreateDefaultService(driverOptions.DriverExePath);
            var chromeOptions = new ChromeOptions();

            chromeDriverService.Port = driverOptions.Port; // this is the port for the driver, not the webpage

            WebDriver = new ChromeDriver(chromeDriverService, chromeOptions);
            TurnOnImplicitWait(null);
            TurnOnScriptWait(null);
        }

    }
}
