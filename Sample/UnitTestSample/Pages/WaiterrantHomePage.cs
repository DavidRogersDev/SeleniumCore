using KesselRun.SeleniumCore.TestDrivers.Contracts;

namespace UnitTestSample.Pages
{
    public class WaiterrantHomePage : BasePage
    {
        public WaiterrantHomePage(ITestDriver seleniumTestDriver) : base(seleniumTestDriver)
        {
        }

        public void PerformSearch(string textToSearch)
        {
            var searchBox = SeleniumTestDriver.FindById("s");
            var searchButton = SeleniumTestDriver.FindByXPath("//input[@id='s']/following-sibling::input");

            SeleniumTestDriver.TypeText(searchBox, textToSearch);
            searchButton.Click();
        }

        public bool SearchResultsTextIsPresent
        {
            get { return SeleniumTestDriver.FindByCssSelector("#content > header > h1 > span.search-results-label").Displayed; }
        }
    }
}
