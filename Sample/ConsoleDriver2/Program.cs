using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using KesselRun.SeleniumCore.Enums;
using KesselRun.SeleniumCore.Infrastructure;
using KesselRun.SeleniumCore.Infrastructure.Factories;
using KesselRun.SeleniumCore.Infrastructure.Factories.Contracts;
using KesselRun.SeleniumCore.TestDrivers.Browsers.Firefox;
using KesselRun.SeleniumCore.TestDrivers.Browsers.Ie;
using OpenQA.Selenium;

namespace ConsoleDriver2
{
    class Program
    {
        static void Main(string[] args)
        {
        
            ITestDriverFactory testDriverFactory = new TestDriverFactory(
            new DriverOptions
            {
                DriverExePath = ConfigurationManager.AppSettings["FirefoxExePath"],
                Port = int.Parse(ConfigurationManager.AppSettings["FirefoxBrowserPort"]),
                Url = ConfigurationManager.AppSettings["StartUrl"]
            });

            var ieTestDriver = testDriverFactory.CreateTestDriver<FirefoxTestDriver>();

            ieTestDriver.GoToUrl(null); // will use default passed in to factory as part of DriverOptions struct
            
            Thread.Sleep(500);
            try
            {
                var heading = ieTestDriver.FindByXPath("//*[@id='mainheader']", seconds: 5); // #menuLink2 is the javascript selector for the menu item to hover
                Console.WriteLine("Heading: {0}", heading.TagName);
                Debug.WriteLine("Heading: {0}", heading.TagName);
                //var day = ieTestDriver.FindByXPath("(//*[@id='mainheader']/ancestor::div[@class='contentwrapper']/div[starts-with(@class,'container')])[1]", ExpectedCondition.ElementIsVisible, 5);
                var day = ieTestDriver.FindByXPath("//*[@id='mainheader']/ancestor::div[@class='contentwrapper']/div[5]", ExpectedCondition.ElementIsVisible, 5);
                day.Click();
                Console.WriteLine("Day: {0}", day.Text);


                //var ps = ieTestDriver.FindByXPathFromWebElement(day, "following-sibling::div[1]/child::p", 5);

                var ps = day.FindElements(By.XPath(@"following-sibling::div[1]/child::p"));
                //var ps = day.FindElement(By.XPath("self::div"));

                Console.WriteLine(day.Text);
                //Console.WriteLine("Elem: {0}", ps.Text);

 
                //var day = ieTestDriver.FindByXPathFromWebElement(heading, "(//*div[following-sibling::div])[1]", 5);

                Trace.WriteLine("Hit IT!");
            }
            catch (Exception e)
            {
                //Trace.WriteLine("Wrong!");
                //ieTestDriver.MouseOverElement(FinderStrategy.Id, "menuLink2"); // #menuLink2 is the javascript selector for the menu item to hover

                //ieTestDriver.FindByIdClickWithRetries("menuLink2_1");
            }

            ieTestDriver.Quit();

            Console.WriteLine("{0}Press any key to close ...", Environment.NewLine);
            Console.ReadKey();
        }
    }
}
