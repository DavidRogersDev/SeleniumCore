using System;
using KesselRun.SeleniumCore.Enums;
using KesselRun.SeleniumCore.Exceptions;
using KesselRun.SeleniumCore.Infrastructure;
using OpenQA.Selenium;
using OpenQA.Selenium.IE;
using DriverOptions = KesselRun.SeleniumCore.Infrastructure.DriverOptions;

namespace KesselRun.SeleniumCore.TestDrivers.Browsers.Ie
{
    public class IeTestDriver : BaseTestDriver
    {
        private Converter<DriverEngine, OpenQA.Selenium.IE. InternetExplorerDriver> _engineConverter;

        public override void Initialize(DriverOptions driverOptions)
        {
            //  Do not set implicit waits. Prefer usage of explicit waits.
            //  Can always turn on implicit waits at a later time, but once set, they cannot be changed.

            DefaultUrl = driverOptions.Url;

            var internetExplorerDriverService =
                InternetExplorerDriverService.CreateDefaultService(driverOptions.DriverExePath);
            var internetExplorerOptions = new InternetExplorerOptions();

            internetExplorerOptions.IntroduceInstabilityByIgnoringProtectedModeSettings = true;
            internetExplorerOptions.AddAdditionalCapability("EnsureCleanSession", true);
            
            internetExplorerDriverService.Port = driverOptions.Port; // this is the port for the driver, not the webpage

            WebDriver = new InternetExplorerDriver(internetExplorerDriverService, internetExplorerOptions);
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
    }
}

