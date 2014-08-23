using KesselRun.SeleniumCore.Enums;
using KesselRun.SeleniumCore.TestDrivers.Contracts;

namespace UnitTestSample.Pages
{
    public class PetStockHomePage : BasePage
    {
        public PetStockHomePage(ITestDriver seleniumTestDriver) : base(seleniumTestDriver)
        {
            
        }

        public virtual void SearchForProduct(string productName)
        {
            SeleniumTestDriver.TypeText(FinderStrategy.Id, "search", productName);
            SeleniumTestDriver.FindByIdClick("searchButton");
        }

        public bool ResultsLabelIsPresent
        {
            get
            {
                return SeleniumTestDriver.FindByCssSelector("div.searchResultsTerm > span").Displayed;
            }
        }
    }
}
