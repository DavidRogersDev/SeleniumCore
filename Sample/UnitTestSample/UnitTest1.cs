using System;
using System.Configuration;
using KesselRun.SeleniumCore.Infrastructure;
using KesselRun.SeleniumCore.Infrastructure.Factories;
using KesselRun.SeleniumCore.Infrastructure.Factories.Contracts;
using KesselRun.SeleniumCore.TestDrivers.Browsers.Chrome;
using KesselRun.SeleniumCore.TestDrivers.Browsers.Firefox;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestSample
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void BasicYahooSearch()
        {
            //  Arrange
            ITestDriverFactory foundry = new TestDriverFactory(new DriverOptions
            {
                DriverExePath = ConfigurationManager.AppSettings["FirefoxExePath"],
                Port = int.Parse(ConfigurationManager.AppSettings["FirefoxBrowserPort"]),
                Url = ConfigurationManager.AppSettings["StartUrl"]
            });

            var firefoxTestDriver = foundry.CreateTestDriver<FirefoxTestDriver>();
            WaiterrantHomePage yahooHomePage = new WaiterrantHomePage(firefoxTestDriver);

            //  Act
            yahooHomePage.Open();
            yahooHomePage.PerformSearch("Congreve");

            //  Assert                        
            Assert.IsTrue(yahooHomePage.SearchResultsTextIsPresent);

            //  Cleanup
            yahooHomePage.Close();
        }
    }
}
