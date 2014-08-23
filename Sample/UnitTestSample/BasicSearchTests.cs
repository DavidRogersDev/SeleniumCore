using KesselRun.SeleniumCore.Infrastructure;
using KesselRun.SeleniumCore.Infrastructure.Factories;
using KesselRun.SeleniumCore.Infrastructure.Factories.Contracts;
using KesselRun.SeleniumCore.TestDrivers.Browsers.Firefox;
using KesselRun.SeleniumCore.TestDrivers.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Configuration;
using UnitTestSample.Pages;

namespace UnitTestSample
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void BasicSearchOfWaiterRantBlog()
        {
            //  Arrange
            var firefoxTestDriver = GetNewFirefoxTestDriver();

            var yahooHomePage = new WaiterrantHomePage(firefoxTestDriver);

            //  Act
            yahooHomePage.Open();
            yahooHomePage.PerformSearch("Congreve");

            //  Assert                        
            Assert.IsTrue(yahooHomePage.SearchResultsTextIsPresent);

            //  Cleanup
            yahooHomePage.Close();
        }

        [TestMethod]
        public void SearchProductAtPetstockHomePage()
        {
            //  Arrange
            var testDriver = GetNewFirefoxTestDriver();
            var petStockPage = new PetStockHomePage(testDriver);

            //  Act
            petStockPage.OpenPage("http://www.petstock.com.au/");
            petStockPage.SearchForProduct("kennel");
            

            //  Assert                        
            Assert.IsTrue(petStockPage.ResultsLabelIsPresent);

            //  Cleanup
            petStockPage.Close();
        }

        private static ITestDriver GetNewFirefoxTestDriver()
        {
            ITestDriverFactory foundry = new TestDriverFactory(new DriverOptions
            {
                DriverExePath = ConfigurationManager.AppSettings["FirefoxExePath"],
                Port = int.Parse(ConfigurationManager.AppSettings["FirefoxBrowserPort"]),
                Url = ConfigurationManager.AppSettings["StartUrl"]
            });

            var firefoxTestDriver = foundry.CreateTestDriver<FirefoxTestDriver>();
            return firefoxTestDriver;
        }


    }
}
