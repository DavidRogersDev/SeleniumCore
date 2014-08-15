using KesselRun.SeleniumCore.Enums;
using KesselRun.SeleniumCore.Infrastructure;
using OpenQA.Selenium;

namespace KesselRun.SeleniumCore.TestDrivers.Contracts
{
    public interface ITestDriver
    {
        bool ElementContainsText(FinderStrategy findBy, string domElement, string text);
        IWebElement FindByClassName(string className, int? seconds  = null);
        IWebElement FindByClassNameClick(string className, int? seconds = null);
        IWebElement FindByClassNameFromWebElement(IWebElement webElement, string domElement, int? seconds = null);
        IWebElement FindByClassNameFromWebElementClick(IWebElement webElement, string domElement, int? seconds = null);
        IWebElement FindByCssSelector(string cssSelector, int? seconds = null);
        IWebElement FindByCssSelectorClick(string cssSelector, int? seconds = null);
        IWebElement FindByCssSelectorFromWebElement(IWebElement webElement, string domElement, int? seconds = null);
        IWebElement FindByCssSelectorFromWebElementClick(IWebElement webElement, string domElement, int? seconds = null);
        IWebElement FindById(string domElement, int? seconds = null);
        IWebElement FindByIdClick(string domElement, int? seconds = null);
        IWebElement FindByIdFromWebElement(IWebElement webElement, string domElement, int? seconds = null);
        IWebElement FindByIdFromWebElementClick(IWebElement webElement, string domElement, int? seconds = null);
        IWebElement FindByLink(string domElement, int? seconds = null);
        IWebElement FindByLinkClick(string domElement, int? seconds = null);
        IWebElement FindByLinkFromWebElement(IWebElement webElement, string domElement, int? seconds = null);
        IWebElement FindByLinkFromWebElementClick(IWebElement webElement, string domElement, int? seconds = null);
        IWebElement FindByName(string domElement, int? seconds = null);
        IWebElement FindByNameClick(string domElement, int? seconds = null);
        IWebElement FindByNameFromWebElement(IWebElement webElement, string domElement, int? seconds = null);
        IWebElement FindByNameFromWebElementClick(IWebElement webElement, string domElement, int? seconds = null);
        IWebElement FindByPartialLinkText(string domElement, int? seconds = null);
        IWebElement FindByPartialLinkTextClick(string domElement, int? seconds = null);
        IWebElement FindByPartialLinkTextFromWebElement(IWebElement webElement, string domElement, int? seconds = null);
        IWebElement FindByPartialLinkTextFromWebElementClick(IWebElement webElement, string domElement, int? seconds = null);
        IWebElement FindByTagName(string domElement, int? seconds = null);
        IWebElement FindByTagNameClick(string domElement, int? seconds = null);
        IWebElement FindByTagNameFromWebElement(IWebElement webElement, string domElement, int? seconds = null);
        IWebElement FindByTagNameFromWebElementClick(IWebElement webElement, string domElement, int? seconds = null);
        IWebElement FindByXPath(string domElement, int? seconds = null);
        IWebElement FindByXPathClick(string domElement, int? seconds = null);
        IWebElement FindByXPathFromWebElement(IWebElement webElement, string domElement, int? seconds = null);
        IWebElement FindByXPathFromWebElementClick(IWebElement webElement, string domElement, int? seconds = null);
        string GetDocumentTitle();
        void GoToUrl(string url);
        void Initialize(DriverOptions driverOptions);
        void MouseOverElement(FinderStrategy findBy, string domElement, string script = null);
        void Quit();
        IWebElement TypeText(IWebElement webElement, string text);
        IWebElement TypeText(FinderStrategy findBy, string domElement, string text);
        IWebElement TypeText(FinderStrategy findBy, string domElement, string text, InputGesture inputGesture);
        string DefaultUrl { get; set; }
        IWebDriver WebDriver { get; }
    }
}
