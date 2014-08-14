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

            var searchBox = firefoxWebDriver.FindByCssSelector("#Table_01a > tbody > tr > td:nth-child(2) > input");

            firefoxWebDriver.TypeText(searchBox, "parking");
            firefoxWebDriver.FindByXPathClick("//*[@id='Table_01a']/tbody/tr/td[3]/a/img");

            firefoxWebDriver.Quit();

            Console.WriteLine("...");
            Console.ReadKey();
        }
    }
}
