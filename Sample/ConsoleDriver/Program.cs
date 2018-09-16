using KesselRun.SeleniumCore.Enums;
using KesselRun.SeleniumCore.Infrastructure;
using KesselRun.SeleniumCore.Infrastructure.Factories;
using System;
using System.Configuration;
using System.Threading;
using KesselRun.SeleniumCore.TestDrivers.Browsers.Firefox;

namespace ConsoleDriver
{
    internal class Program
    {
        private static void Main()
        {
            var testDriverFactory = new TestDriverFactory(
                new DriverOptions
                {
                    DriverExePath = ConfigurationManager.AppSettings["FirefoxDriverPath"],
                    Port = int.Parse(ConfigurationManager.AppSettings["FirefoxBrowserPort"]),
                    Url = ConfigurationManager.AppSettings["StartUrl"]
                });

            var firfoxTestDriver = testDriverFactory.CreateTestDriver<FirefoxTestDriver>();

            firfoxTestDriver.GoToUrl(null); // will use default passed in to factory as part of DriverOptions struct

            firfoxTestDriver.MaximiseWindow();

            firfoxTestDriver.MouseOverElement(FinderStrategy.PartialLinkText, "HACCP Certification");
            
            firfoxTestDriver.FindByPartialLinkTextClick("HACCP Principles", ExpectedCondition.ElementIsVisible, 10);

            var heading = firfoxTestDriver.FindByTagName("h1", seconds: 5);

            Console.WriteLine(heading.Text);
           

            firfoxTestDriver.Quit();

            Console.WriteLine("{0}Press any key to close ...", Environment.NewLine);
            Console.ReadKey();
        }
    }
}
