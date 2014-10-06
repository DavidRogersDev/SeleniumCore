using KesselRun.SeleniumCore.Enums;
using KesselRun.SeleniumCore.TestDrivers.Contracts;

namespace UnitTestSample.Pages
{
    public class BigPondSignInPage : BasePage
    {
        private const string RememberMe = "rememberMe";

        public BigPondSignInPage(ITestDriver seleniumTestDriver) 
            : base(seleniumTestDriver)
        {
            
        }

        public void OpenPage()
        {
            SeleniumTestDriver.GoToUrl("https://signon.bigpond.com/login");
        }

        public void SignIn()
        {
            SeleniumTestDriver.FindByIdClick(RememberMe);
        }

        public bool RememberMeIsChecked
        {
            get { return SeleniumTestDriver.IsElementChecked(FinderStrategy.Id, RememberMe); }
        }

        public void Quit()
        {
            SeleniumTestDriver.Quit();
        }
    }
}
