using KesselRun.SeleniumCore.Enums;
using KesselRun.SeleniumCore.Infrastructure;
using KesselRun.SeleniumCore.Infrastructure.Factories;
using KesselRun.SeleniumCore.Infrastructure.Factories.Contracts;
using System;
using System.Configuration;
using KesselRun.SeleniumCore.TestDrivers.Browsers.Chrome;

namespace ConsoleDriver
{
    internal class Program
    {
        private static void Main()
        {
            ITestDriverFactory testDriverFactory = new TestDriverFactory(
                new DriverOptions
                {
                    DriverExePath = ConfigurationManager.AppSettings["ChromeDriverPath"],
                    Port = int.Parse(ConfigurationManager.AppSettings["ChromeBrowserPort"]),
                    Url = ConfigurationManager.AppSettings["StartUrl"]
                });

            var testDriver = testDriverFactory.CreateTestDriver<ChromeTestDriver>();

            testDriver.GoToUrl(null); // will use default passed in to factory as part of DriverOptions struct

            testDriver.MouseOverElement(FinderStrategy.Id, "menuLink2");
            testDriver.FindByIdClick("menuLink2_1");

            var heading = testDriver.FindByCssSelectorFromWebElement(testDriver.FindByClassName("maintd", seconds:5), "h1");

            Console.WriteLine(heading.Text);
           

            testDriver.Quit();

            Console.WriteLine("{0}Press any key to close ...", Environment.NewLine);
            Console.ReadKey();
        }
    }
}
