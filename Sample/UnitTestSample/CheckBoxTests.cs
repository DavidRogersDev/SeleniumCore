using KesselRun.SeleniumCore.Infrastructure;
using KesselRun.SeleniumCore.Infrastructure.Factories;
using KesselRun.SeleniumCore.Infrastructure.Factories.Contracts;
using KesselRun.SeleniumCore.TestDrivers.Browsers.Chrome;
using KesselRun.SeleniumCore.TestDrivers.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Configuration;
using UnitTestSample.Pages;

namespace UnitTestSample
{
    [TestClass]
    public class CheckBoxTests
    {
        [TestMethod]
        public void CheckedCheckBoxIsReportedAsNotCheckedWhenNotChecked()
        {
            //  Arrange
            var driver = GetNewChromeTestDriver();
            var bigPondSigninPage = new BigPondSignInPage(driver);
            bigPondSigninPage.OpenPage();

            //  Act
            //  Assert                        
            Assert.IsFalse(bigPondSigninPage.RememberMeIsChecked);

            bigPondSigninPage.Quit();
        }

        [TestMethod]
        public void CheckedCheckBoxIsReportedAsCheckedWhenChecked()
        {
            //  Arrange
            var driver = GetNewChromeTestDriver();
            var bigPondSigninPage = new BigPondSignInPage(driver);
            bigPondSigninPage.OpenPage();

            //  Act
            bigPondSigninPage.SignIn();
            
            //  Assert                        
            Assert.IsTrue(bigPondSigninPage.RememberMeIsChecked);

            bigPondSigninPage.Quit();
        }

        private static ITestDriver GetNewChromeTestDriver()
        {
            ITestDriverFactory foundry = new TestDriverFactory(new DriverOptions
            {
                DriverExePath = ConfigurationManager.AppSettings["ChromeExePath"],
                Port = int.Parse(ConfigurationManager.AppSettings["ChromeBrowserPort"])
            });

            var chromeTestDriver = foundry.CreateTestDriver<ChromeTestDriver>();
            return chromeTestDriver;
        }
    }
}
