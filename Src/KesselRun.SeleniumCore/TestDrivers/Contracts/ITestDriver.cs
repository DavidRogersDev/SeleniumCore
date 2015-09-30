using KesselRun.SeleniumCore.Enums;
using KesselRun.SeleniumCore.Infrastructure;
using OpenQA.Selenium;
using System;
using OpenQA.Selenium.Interactions;

namespace KesselRun.SeleniumCore.TestDrivers.Contracts
{
    public interface ITestDriver
    {
        bool ElementContainsText(FinderStrategy findBy, string domElement, string text, int? seconds = null);
        bool ElementIsNotInDom(FinderStrategy findBy, string domElement);
        IWebElement FindByClassName(string className, ExpectedCondition expectedCondition = ExpectedCondition.ElementIsVisible, int? seconds = null);
        IWebElement FindByClassNameClick(string className, ExpectedCondition expectedCondition = ExpectedCondition.ElementIsVisible, int? seconds = null);
        IWebElement FindByClassNameFromWebElement(IWebElement webElement, string domElement, int? seconds = null);
        IWebElement FindByClassNameFromWebElementClick(IWebElement webElement, string domElement, int? seconds = null);
        IWebElement FindByCssSelector(string cssSelector, ExpectedCondition expectedCondition = ExpectedCondition.ElementIsVisible, int? seconds = null);
        IWebElement FindByCssSelectorClick(string cssSelector, ExpectedCondition expectedCondition = ExpectedCondition.ElementIsVisible, int? seconds = null);
        IWebElement FindByCssSelectorFromWebElement(IWebElement webElement, string domElement, int? seconds = null);
        IWebElement FindByCssSelectorFromWebElementClick(IWebElement webElement, string domElement, int? seconds = null);
        IWebElement FindById(string domElement, ExpectedCondition expectedCondition = ExpectedCondition.ElementIsVisible, int? seconds = null);
        IWebElement FindByIdClick(string domElement, ExpectedCondition expectedCondition = ExpectedCondition.ElementIsVisible, int? 
            seconds = null);

        bool FindByIdClickWithRetries(string domElement, ExpectedCondition expectedCondition = ExpectedCondition.ElementIsVisible, int? seconds = null, int? numberOfRetries = 20);
        IWebElement FindByIdFromWebElement(IWebElement webElement, string domElement, int? seconds = null);
        IWebElement FindByIdFromWebElementClick(IWebElement webElement, string domElement, int? seconds = null);
        IWebElement FindByLink(string domElement, ExpectedCondition expectedCondition = ExpectedCondition.ElementIsVisible, int? seconds = null);
        IWebElement FindByLinkClick(string domElement, ExpectedCondition expectedCondition = ExpectedCondition.ElementIsVisible, int? seconds = null);
        IWebElement FindByLinkFromWebElement(IWebElement webElement, string domElement, int? seconds = null);
        IWebElement FindByLinkFromWebElementClick(IWebElement webElement, string domElement, int? seconds = null);
        IWebElement FindByName(string domElement, ExpectedCondition expectedCondition = ExpectedCondition.ElementIsVisible, int? seconds = null);
        IWebElement FindByNameClick(string domElement, ExpectedCondition expectedCondition = ExpectedCondition.ElementIsVisible, int? seconds = null);
        IWebElement FindByNameFromWebElement(IWebElement webElement, string domElement, int? seconds = null);
        IWebElement FindByNameFromWebElementClick(IWebElement webElement, string domElement, int? seconds = null);
        IWebElement FindByPartialLinkText(string domElement, ExpectedCondition expectedCondition = ExpectedCondition.ElementIsVisible, int? seconds = null);
        IWebElement FindByPartialLinkTextClick(string domElement, ExpectedCondition expectedCondition = ExpectedCondition.ElementIsVisible, int? seconds = null);
        IWebElement FindByPartialLinkTextFromWebElement(IWebElement webElement, string domElement, int? seconds = null);
        IWebElement FindByPartialLinkTextFromWebElementClick(IWebElement webElement, string domElement, int? seconds = null);
        IWebElement FindByTagName(string domElement, ExpectedCondition expectedCondition = ExpectedCondition.ElementIsVisible, int? seconds = null);
        IWebElement FindByTagNameClick(string domElement, ExpectedCondition expectedCondition = ExpectedCondition.ElementIsVisible, int? seconds = null);
        IWebElement FindByTagNameFromWebElement(IWebElement webElement, string domElement, int? seconds = null);
        IWebElement FindByTagNameFromWebElementClick(IWebElement webElement, string domElement, int? seconds = null);
        IWebElement FindByXPath(string domElement, ExpectedCondition expectedCondition = ExpectedCondition.ElementIsVisible, int? seconds = null);
        IWebElement FindByXPathClick(string domElement, ExpectedCondition expectedCondition = ExpectedCondition.ElementIsVisible, int? seconds = null);
        bool FindByXPathClickWithRetries(string domElement, ExpectedCondition expectedCondition = ExpectedCondition.ElementIsVisible, int? seconds = null, int? numberOfRetries = 20);

        IWebElement FindByXPathFromWebElement(IWebElement webElement, string domElement, int? seconds = null);
        IWebElement FindByXPathFromWebElementClick(IWebElement webElement, string domElement, int? seconds = null);
        IWebElement FindWithWait(int seconds, Func<IWebDriver, IWebElement> expectedFunc);
        string GetDocumentTitle();
        void GoToUrl(string url);
        void Initialize(DriverOptions driverOptions);
        bool IsElementChecked(FinderStrategy findBy, string domElement, int? seconds = null);
        Actions MouseOverElement(FinderStrategy findBy, string domElement, int? seconds = null);
        void MouseOverElementUsingScript(string script);
        void Quit();
        IWebDriver SwitchBackToDefault();
        IWebDriver SwitchToIFrame(IWebElement frame);
        IWebElement TypeText(IWebElement webElement, string text);
        IWebElement TypeText(FinderStrategy findBy, string domElement, string text, int? seconds = null);
        IWebElement TypeText(FinderStrategy findBy, string domElement, string text, InputGesture inputGesture, int? seconds = null);
        string DefaultUrl { get; set; }
        IWebDriver WebDriver { get; }
    }
}
