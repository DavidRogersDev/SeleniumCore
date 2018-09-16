using System;
using System.Configuration;
using System.Diagnostics;
using System.Threading;
using KesselRun.SeleniumCore.Enums;
using KesselRun.SeleniumCore.Infrastructure.Factories;
using KesselRun.SeleniumCore.Infrastructure.Factories.Contracts;
using KesselRun.SeleniumCore.TestDrivers.Browsers.Chrome;
using OpenQA.Selenium;

namespace ConsoleDriver2
{
    class Program
    {
        static void Main(string[] args)
        {
        
            ITestDriverFactory testDriverFactory = new TestDriverFactory(
            new KesselRun.SeleniumCore.Infrastructure.DriverOptions
            {
                DriverExePath = ConfigurationManager.AppSettings["ChromeDriverPath"],
                Port = int.Parse(ConfigurationManager.AppSettings["ChromeBrowserPort"]),
                Url = ConfigurationManager.AppSettings["StartUrl"]
            });

            var chromeTestDriver = testDriverFactory.CreateTestDriver<ChromeTestDriver>();

            chromeTestDriver.GoToUrl(null); // will use default passed in to factory as part of DriverOptions struct

            chromeTestDriver.MaximiseWindow();

            try
            {
                var heading = chromeTestDriver.FindByXPath("//*[@id='mainheader']", seconds: 5); // #menuLink2 is the javascript selector for the menu item to hover
                Console.WriteLine("Heading: {0}", heading.TagName);
                Debug.WriteLine("Heading: {0}", heading.TagName);
                //var day = chromeTestDriver.FindByXPath("(//*[@id='mainheader']/ancestor::div[@class='contentwrapper']/div[starts-with(@class,'container')])[1]", ExpectedCondition.ElementIsVisible, 5);
                var day = chromeTestDriver.FindByXPath("//*[@id='mainheader']/ancestor::div[@class='contentwrapper']/div[5]", ExpectedCondition.ElementIsVisible, 5);
                day.Click();
                Console.WriteLine("Day: {0}", day.Text);


                //var ps = chromeTestDriver.FindByXPathFromWebElement(day, "following-sibling::div[1]/child::p", 5);

                var ps = day.FindElements(By.XPath(@"following-sibling::div[1]/child::p"));
                //var ps = day.FindElement(By.XPath("self::div"));

                Console.WriteLine(day.Text);
                //Console.WriteLine("Elem: {0}", ps.Text);

 
                //var day = chromeTestDriver.FindByXPathFromWebElement(heading, "(//*div[following-sibling::div])[1]", 5);

                Trace.WriteLine("Hit IT!");
            }
            catch (Exception e)
            {
                //Trace.WriteLine("Wrong!");
                //chromeTestDriver.MouseOverElement(FinderStrategy.Id, "menuLink2"); // #menuLink2 is the javascript selector for the menu item to hover

                //chromeTestDriver.FindByIdClickWithRetries("menuLink2_1");
            }

            chromeTestDriver.Quit();

            Console.WriteLine("{0}Press any key to close ...", Environment.NewLine);
            Console.ReadKey();
        }
    }
}
