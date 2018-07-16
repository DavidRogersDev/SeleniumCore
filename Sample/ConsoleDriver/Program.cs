using KesselRun.SeleniumCore;
using KesselRun.SeleniumCore.Enums;
using KesselRun.SeleniumCore.Infrastructure;
using KesselRun.SeleniumCore.Infrastructure.Factories;
using KesselRun.SeleniumCore.Infrastructure.Factories.Contracts;
using System;
using System.Configuration;
using System.Threading;
using KesselRun.SeleniumCore.TestDrivers.Browsers.Chrome;

namespace ConsoleDriver
{
    internal class Program
    {
        private static void Main()
        {
            var testDriverFactory = new TestDriverFactory(
                new DriverOptions
                {
                    DriverExePath = ConfigurationManager.AppSettings["IeDriverPath"],
                    Port = int.Parse(ConfigurationManager.AppSettings["IeBrowserPort"]),
                    Url = ConfigurationManager.AppSettings["StartUrl"]
                }, Constants.IeTestDriver);

            var testDriver = testDriverFactory.CreateTestDriver();

            testDriver.GoToUrl(null); // will use default passed in to factory as part of DriverOptions struct

            testDriver.MouseOverElement(FinderStrategy.PartialLinkText, "HACCP Certification");

            Thread.Sleep(5000);

            testDriver.FindByLinkClick("HACCP Principles");

            //var heading = testDriver.FindByCssSelectorFromWebElement(testDriver.FindByClassName("maintd", seconds:5), "h1");

            //Console.WriteLine(heading.Text);
           

            testDriver.Quit();

            Console.WriteLine("{0}Press any key to close ...", Environment.NewLine);
            Console.ReadKey();
        }
    }
}
