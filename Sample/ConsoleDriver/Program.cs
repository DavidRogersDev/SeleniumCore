using KesselRun.SeleniumCore.Enums;
using KesselRun.SeleniumCore.Infrastructure;
using KesselRun.SeleniumCore.Infrastructure.Factories;
using KesselRun.SeleniumCore.Infrastructure.Factories.Contracts;
using System;
using System.Configuration;

namespace ConsoleDriver
{
    internal class Program
    {
        private static void Main()
        {
            ITestDriverFactory testDriverFactory = new TestDriverFactory(
                new DriverOptions
                {
                    DriverExePath = ConfigurationManager.AppSettings["FirefoxExePath"],
                    Port = int.Parse(ConfigurationManager.AppSettings["FirefoxBrowserPort"]),
                    Url = ConfigurationManager.AppSettings["StartUrl"]
                }, "Firefox");

            var firefoxWebDriver = testDriverFactory.CreateTestDriver();

            firefoxWebDriver.GoToUrl(null); // will use default passed in to factory as part of DriverOptions struct

            firefoxWebDriver.MouseOverElement(FinderStrategy.Id, "menuLink2");
            firefoxWebDriver.FindByIdClick("menuLink2_1");

            var heading = firefoxWebDriver.FindByCssSelectorFromWebElement(firefoxWebDriver.FindByClassName("maintd"), "h1");

            Console.WriteLine(heading.Text);
           

            firefoxWebDriver.Quit();

            Console.WriteLine("Press any key to close ...");
            Console.ReadKey();
        }
    }
}
