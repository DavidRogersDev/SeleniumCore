using KesselRun.SeleniumCore.Infrastructure;
using KesselRun.SeleniumCore.Infrastructure.Factories;
using KesselRun.SeleniumCore.Infrastructure.Factories.Contracts;
using System;
using System.Configuration;

namespace ConsoleDriver
{
    class Program
    {
        static void Main()
        {
            ITestDriverFactory testDriverFactory = new TestDriverFactory("Firefox",
                new DriverOptions
                {
                    DriverExePath = ConfigurationManager.AppSettings["FirefoxExePath"],
                    Port = int.Parse(ConfigurationManager.AppSettings["FirefoxBrowserPort"]),
                    Url = ConfigurationManager.AppSettings["StartUrl"]
                });

            var driver = testDriverFactory.CreateTestDriver();

            driver.GoToUrl(null);

            driver.Quit();

            Console.WriteLine("...");
            Console.ReadKey();
        }
    }
}
