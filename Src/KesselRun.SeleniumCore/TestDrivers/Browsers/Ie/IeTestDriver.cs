using KesselRun.SeleniumCore.Enums;
using KesselRun.SeleniumCore.Exceptions;
using KesselRun.SeleniumCore.Infrastructure;
using OpenQA.Selenium;
using OpenQA.Selenium.IE;

namespace KesselRun.SeleniumCore.TestDrivers.Browsers.Ie
{
    public class IeTestDriver : BaseTestDriver
    {
        public override void Initialize(DriverOptions driverOptions)
        {
            DefaultUrl = driverOptions.Url;

            var internetExplorerDriverService =
                InternetExplorerDriverService.CreateDefaultService(driverOptions.DriverExePath);
            var internetExplorerOptions = new InternetExplorerOptions();

            internetExplorerDriverService.Port = driverOptions.Port; // this is the port for the driver, not the webpage

            WebDriver = new InternetExplorerDriver(internetExplorerDriverService, internetExplorerOptions);
            TurnOnImplicitWait(null);
            TurnOnScriptWait(null);
        }

        protected override IWebElement ClickWebElement(string domElement, int? seconds, IWebElement element, FinderStrategy finderStrategy)
        {
            if (!ReferenceEquals(null, element))
            {
                element.SendKeys(Keys.Enter); // IE prefers this to a click. Who knows why!?
                return element;
            }

            throw new ElementWasNullException(finderStrategy, domElement, seconds);
        }

        public override void MouseOverElement(FinderStrategy findBy, string domElement, string script = null)
        {
            if (string.IsNullOrWhiteSpace(script))
                throw new EmptyScriptException("The 'script' parameter cannot be null for the IeTestDriver.");

            ((IJavaScriptExecutor)WebDriver).ExecuteScript(script);
        }
    }
}

