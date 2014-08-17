using System;
using KesselRun.SeleniumCore.Enums;
using KesselRun.SeleniumCore.Exceptions;
using KesselRun.SeleniumCore.Infrastructure;
using KesselRun.SeleniumCore.TestDrivers.Contracts;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Interactions;

namespace KesselRun.SeleniumCore.TestDrivers
{
    public abstract class BaseTestDriver : ITestDriver
    {
        public const int DefaultWebDriverWait = 15;
        public const string HttpPrefix = @"http://";
        public const string HttpSslPrefix = @"https://";

        public string DefaultUrl { get; set; }
        public IWebDriver WebDriver { get; protected set; }

        protected virtual IWebElement ClickWebElement(
            string domElement, 
            int? seconds, 
            IWebElement element, 
            FinderStrategy finderStrategy)
        {
            if (!ReferenceEquals(null, element))
            {
                element.Click();
                return element;
            }

            throw new ElementWasNullException(finderStrategy, domElement, seconds);
        }

        public virtual string GetDocumentTitle()
        {
            return WebDriver.Title;
        }

        public virtual void GoToUrl(string url)
        {
            INavigation navigation = WebDriver.Navigate();

            if (string.IsNullOrEmpty(url))
            {
                url = DefaultUrl;
            }

            if (!url.StartsWith(HttpPrefix) && !url.StartsWith(HttpSslPrefix))
            {
                //  Assume not SSL if dev to lazy to type in "https://"
                url = string.Concat(HttpPrefix, url);
            }

            navigation.GoToUrl(url);
        }

        public virtual bool ElementContainsText(FinderStrategy findBy, string domElement, string text)
        {
            var element = GetWebElementByFinderStrategy(findBy, domElement);

            return !ReferenceEquals(null, element) && (element.Text.Contains(text));
        }

        public virtual IWebElement FindByClassName(
            string className, 
            ExpectedCondition expectedCondition = ExpectedCondition.ElementIsVisible, 
            int? seconds = null)
        {
            Func<IWebDriver, IWebElement> webelementFunc = GetWebelementFunc(By.ClassName(className), expectedCondition);

            return FindWebElement(By.ClassName(className), seconds, webelementFunc);
        }

        public virtual IWebElement FindByClassNameClick(
            string className, 
            ExpectedCondition expectedCondition = ExpectedCondition.ElementIsVisible, 
            int? seconds = null)
        {
            IWebElement element = FindByClassName(className, expectedCondition, seconds);

            return ClickWebElement(className, seconds, element, FinderStrategy.ClassName);
        }

        public virtual IWebElement FindByClassNameFromWebElement(IWebElement webElement, string domElement, int? seconds = null)
        {
            Func<IWebDriver, IWebElement> webelementFromWebElementFunc = GetWebelementFromWebElementFunc(By.ClassName(domElement), webElement);

            return FindWebElementFromWebElement(By.ClassName(domElement), seconds, webElement, webelementFromWebElementFunc);
        }

        public virtual IWebElement FindByClassNameFromWebElementClick(IWebElement webElement, string domElement, int? seconds = null)
        {
            IWebElement element = FindByClassNameFromWebElement(webElement, domElement, seconds);

            return ClickWebElement(domElement, seconds, element, FinderStrategy.ClassName);
        }

        public virtual IWebElement FindByCssSelector(
            string cssSelector, 
            ExpectedCondition expectedCondition = ExpectedCondition.ElementIsVisible, 
            int? seconds = null)
        {
            Func<IWebDriver, IWebElement> getWebelementFunc = GetWebelementFunc(By.CssSelector(cssSelector), expectedCondition);

            return FindWebElement(By.CssSelector(cssSelector), seconds, getWebelementFunc);
        }

        public virtual IWebElement FindByCssSelectorClick(
            string cssSelector, 
            ExpectedCondition expectedCondition = ExpectedCondition.ElementIsVisible, 
            int? seconds = null)
        {
            IWebElement element = FindByCssSelector(cssSelector, expectedCondition, seconds);

            return ClickWebElement(cssSelector, seconds, element, FinderStrategy.Css);
        }
        public virtual IWebElement FindByCssSelectorFromWebElement(IWebElement webElement, string domElement, int? seconds = null)
        {
            Func<IWebDriver, IWebElement> webelementFromWebElementFunc = GetWebelementFromWebElementFunc(By.CssSelector(domElement), webElement);

            return FindWebElementFromWebElement(By.CssSelector(domElement), seconds, webElement, webelementFromWebElementFunc);
        }

        public virtual IWebElement FindByCssSelectorFromWebElementClick(IWebElement webElement, string domElement, int? seconds = null)
        {
            IWebElement element = FindByCssSelectorFromWebElement(webElement, domElement, seconds);

            return ClickWebElement(domElement, seconds, element, FinderStrategy.Css);
        }

        public virtual IWebElement FindById(
            string domElement, 
            ExpectedCondition expectedCondition = ExpectedCondition.ElementIsVisible, 
            int? seconds = null)
        {
            Func<IWebDriver, IWebElement> getWebelementFunc = GetWebelementFunc(By.Id(domElement), expectedCondition);

            return FindWebElement(By.Id(domElement), seconds, getWebelementFunc);
        }

        public virtual IWebElement FindByIdClick(
            string domElement, 
            ExpectedCondition expectedCondition = ExpectedCondition.ElementIsVisible, 
            int? seconds = null)
        {
            IWebElement element = FindById(domElement, expectedCondition, seconds);

            return ClickWebElement(domElement, seconds, element, FinderStrategy.Id);
        }

        public virtual IWebElement FindByIdFromWebElement(IWebElement webElement, string domElement, int? seconds = null)
        {
            Func<IWebDriver, IWebElement> webelementFromWebElementFunc = GetWebelementFromWebElementFunc(By.Id(domElement), webElement);

            return FindWebElementFromWebElement(By.Id(domElement), seconds, webElement, webelementFromWebElementFunc);
        }

        public virtual IWebElement FindByIdFromWebElementClick(IWebElement webElement, string domElement, int? seconds = null)
        {
            IWebElement element = FindByIdFromWebElement(webElement, domElement, seconds);

            return ClickWebElement(domElement, seconds, element, FinderStrategy.Id);
        }

        public virtual IWebElement FindByLink(
            string domElement,
            ExpectedCondition expectedCondition = ExpectedCondition.ElementIsVisible, 
            int? seconds = null)
        {
            Func<IWebDriver, IWebElement> getWebelementFunc = GetWebelementFunc(By.LinkText(domElement), expectedCondition);

            return FindWebElement(By.LinkText(domElement), seconds, getWebelementFunc);
        }

        public virtual IWebElement FindByLinkClick(
            string domElement,
            ExpectedCondition expectedCondition = ExpectedCondition.ElementIsVisible, 
            int? seconds = null)
        {
            IWebElement element = FindByLink(domElement, expectedCondition, seconds);

            return ClickWebElement(domElement, seconds, element, FinderStrategy.LinkText);
        }

        public virtual IWebElement FindByLinkFromWebElement(IWebElement webElement, string domElement, int? seconds = null)
        {
            Func<IWebDriver, IWebElement> webelementFromWebElementFunc = GetWebelementFromWebElementFunc(By.LinkText(domElement), webElement);

            return FindWebElementFromWebElement(By.LinkText(domElement), seconds, webElement, webelementFromWebElementFunc);
        }

        public virtual IWebElement FindByLinkFromWebElementClick(IWebElement webElement, string domElement, int? seconds = null)
        {
            IWebElement element = FindByLinkFromWebElement(webElement, domElement, seconds);

            return ClickWebElement(domElement, seconds, element, FinderStrategy.LinkText);
        }

        public virtual IWebElement FindByName(
            string domElement,
            ExpectedCondition expectedCondition = ExpectedCondition.ElementIsVisible, 
            int? seconds = null)
        {
            Func<IWebDriver, IWebElement> getWebelementFunc = GetWebelementFunc(By.Name(domElement), expectedCondition);

            return FindWebElement(By.Name(domElement), seconds, getWebelementFunc);
        }

        public virtual IWebElement FindByNameClick(
            string domElement,
            ExpectedCondition expectedCondition = ExpectedCondition.ElementIsVisible, 
            int? seconds = null)
        {
            IWebElement element = FindByName(domElement, expectedCondition, seconds);

            return ClickWebElement(domElement, seconds, element, FinderStrategy.Name);
        }

        public virtual IWebElement FindByNameFromWebElement(IWebElement webElement, string domElement, int? seconds = null)
        {
            Func<IWebDriver, IWebElement> webelementFromWebElementFunc = GetWebelementFromWebElementFunc(By.Name(domElement), webElement);

            return FindWebElementFromWebElement(By.Name(domElement), seconds, webElement, webelementFromWebElementFunc);
        }

        public virtual IWebElement FindByNameFromWebElementClick(IWebElement webElement, string domElement, int? seconds = null)
        {
            IWebElement element = FindByNameFromWebElement(webElement, domElement, seconds);

            return ClickWebElement(domElement, seconds, element, FinderStrategy.Name);
        }

        public virtual IWebElement FindByPartialLinkText(
            string domElement,
            ExpectedCondition expectedCondition = ExpectedCondition.ElementIsVisible, 
            int? seconds = null)
        {
            Func<IWebDriver, IWebElement> getWebelementFunc = GetWebelementFunc(By.PartialLinkText(domElement), expectedCondition);

            return FindWebElement(By.PartialLinkText(domElement), seconds, getWebelementFunc);

        }

        public virtual IWebElement FindByPartialLinkTextClick(
            string domElement,
            ExpectedCondition expectedCondition = ExpectedCondition.ElementIsVisible, 
            int? seconds = null)
        {
            IWebElement element = FindByPartialLinkText(domElement, expectedCondition, seconds);

            return ClickWebElement(domElement, seconds, element, FinderStrategy.PartialLinkText);
        }

        public virtual IWebElement FindByPartialLinkTextFromWebElement(IWebElement webElement, string domElement, int? seconds = null)
        {
            Func<IWebDriver, IWebElement> webelementFromWebElementFunc = GetWebelementFromWebElementFunc(By.PartialLinkText(domElement), webElement);

            return FindWebElementFromWebElement(By.PartialLinkText(domElement), seconds, webElement, webelementFromWebElementFunc);
        }

        public virtual IWebElement FindByPartialLinkTextFromWebElementClick(IWebElement webElement, string domElement, int? seconds = null)
        {
            IWebElement element = FindByPartialLinkTextFromWebElement(webElement, domElement, seconds);

            return ClickWebElement(domElement, seconds, element, FinderStrategy.PartialLinkText);
        }

        public virtual IWebElement FindByTagName(
            string domElement,
            ExpectedCondition expectedCondition = ExpectedCondition.ElementIsVisible, 
            int? seconds = null)
        {
            Func<IWebDriver, IWebElement> getWebelementFunc = GetWebelementFunc(By.TagName(domElement), expectedCondition);

            return FindWebElement(By.TagName(domElement), seconds, getWebelementFunc);
        }

        public virtual IWebElement FindByTagNameClick(
            string domElement,
            ExpectedCondition expectedCondition = ExpectedCondition.ElementIsVisible, 
            int? seconds = null)
        {
            IWebElement element = FindByTagName(domElement, expectedCondition, seconds);

            return ClickWebElement(domElement, seconds, element, FinderStrategy.TagName);
        }

        public virtual IWebElement FindByTagNameFromWebElement(IWebElement webElement, string domElement, int? seconds = null)
        {
            Func<IWebDriver, IWebElement> webelementFromWebElementFunc = GetWebelementFromWebElementFunc(By.TagName(domElement), webElement);

            return FindWebElementFromWebElement(By.TagName(domElement), seconds, webElement, webelementFromWebElementFunc);
        }

        public virtual IWebElement FindByTagNameFromWebElementClick(IWebElement webElement, string domElement, int? seconds = null)
        {
            IWebElement element = FindByTagNameFromWebElement(webElement, domElement, seconds);

            return ClickWebElement(domElement, seconds, element, FinderStrategy.TagName);
        }

        public virtual IWebElement FindByXPath(
            string domElement,
            ExpectedCondition expectedCondition = ExpectedCondition.ElementIsVisible, 
            int? seconds = null)
        {
            Func<IWebDriver, IWebElement> getWebelementFunc = GetWebelementFunc(By.XPath(domElement), expectedCondition);

            return FindWebElement(By.XPath(domElement), seconds, getWebelementFunc);

        }

        public virtual IWebElement FindByXPathClick(
            string domElement,
            ExpectedCondition expectedCondition = ExpectedCondition.ElementIsVisible, 
            int? seconds = null)
        {
            IWebElement element = FindByXPath(domElement, expectedCondition, seconds);

            return ClickWebElement(domElement, seconds, element, FinderStrategy.XPath);
        }

        public virtual IWebElement FindByXPathFromWebElement(IWebElement webElement, string domElement, int? seconds = null)
        {
            Func<IWebDriver, IWebElement> webelementFromWebElementFunc = GetWebelementFromWebElementFunc(By.XPath(domElement), webElement);

            return FindWebElementFromWebElement(By.XPath(domElement), seconds, webElement, webelementFromWebElementFunc);
        }

        public virtual IWebElement FindByXPathFromWebElementClick(IWebElement webElement, string domElement, int? seconds = null)
        {
            IWebElement element = FindByXPathFromWebElement(webElement, domElement, seconds);

            return ClickWebElement(domElement, seconds, element, FinderStrategy.XPath);
        }

        private IWebElement FindWebElement(By findBy, int? seconds, Func<IWebDriver, IWebElement> expectedFunc)
        {
            return seconds.HasValue ? FindWithWait(seconds.Value, expectedFunc) : WebDriver.FindElement(findBy);
        }
        private IWebElement FindWebElementFromWebElement(By findBy, int? seconds, IWebElement webElement, Func<IWebDriver, IWebElement> expectedFunc)
        {
            return seconds.HasValue ? FindWithWait(seconds.Value, expectedFunc) : webElement.FindElement(findBy);
        }
        
        public abstract void Initialize(DriverOptions driverOptions);

        public virtual void MouseOverElement(FinderStrategy findBy, string domElement, string script = null)
        {
            var element = GetWebElementByFinderStrategy(findBy, domElement);
            HoverOverElement(element);
        }

        public void Quit()
        {
            WebDriver.Quit();
        }

        public virtual IWebElement TypeText(IWebElement webElement, string text)
        {
            if (ReferenceEquals(webElement ,null))
                return null;

            webElement.Clear();
            webElement.SendKeys(text);

            return webElement;
        }

        public virtual IWebElement TypeText(FinderStrategy findBy, string domElement, string text)
        {
            if (string.IsNullOrWhiteSpace(domElement)) 
                throw new ArgumentNullException("domElement");


            IWebElement element = GetWebElementByFinderStrategy(findBy, domElement);

            return TypeText(element, text);
        }

        public virtual IWebElement TypeText(FinderStrategy findBy, string domElement, string text, InputGesture inputGesture)
        {
            IWebElement element = TypeText(findBy, domElement, text);

            if (ReferenceEquals(element, null))
                return element;

            if ((inputGesture & InputGesture.Enter) == InputGesture.Enter)
                element.SendKeys(Keys.Enter);

            if ((inputGesture & InputGesture.Tab) == InputGesture.Tab)
                element.SendKeys(Keys.Tab);
            
            return element;
        }

        protected virtual void TurnOnImplicitWait(int? wait = null)
        {
            WebDriver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(wait ?? DefaultWebDriverWait));
        }

        protected virtual void TurnOnScriptWait(int? wait = null)
        {
            WebDriver.Manage().Timeouts().SetScriptTimeout(TimeSpan.FromSeconds(wait ?? DefaultWebDriverWait));
        }

        private IWebElement FindWithWait(int seconds, Func<IWebDriver, IWebElement> expectedFunc)
        {
            var wait = new WebDriverWait(WebDriver, TimeSpan.FromSeconds(seconds));
            return wait.Until(expectedFunc);
        }

        private static Func<IWebDriver, IWebElement> GetWebelementFunc(By locator, ExpectedCondition expectedCondition)
        {
            Func<IWebDriver, IWebElement> getWebelementFunc;

            switch (expectedCondition)
            {
                case ExpectedCondition.ElementExists:
                    getWebelementFunc = ExpectedConditions.ElementExists(locator);
                    break;
                case ExpectedCondition.ElementIsVisible:
                    getWebelementFunc = ExpectedConditions.ElementIsVisible(locator);
                    break;

                default:
                    throw new NotSupportedException(
                        string.Format(
                            "This method {0} does not support the selected option of the enum {1}. Please select either {2} or {3}",
                            "FindByClassName",
                            expectedCondition,
                            "ElementExists",
                            "ElementIsVisible"
                            ));
            }
            return getWebelementFunc;
        }

        private static Func<IWebDriver, IWebElement> GetWebelementFromWebElementFunc(By locator, IWebElement webElement)
        {
            return webdriver => webElement.FindElement(locator);
        }

        private IWebElement GetWebElementByFinderStrategy(FinderStrategy findBy, string domElement)
        {
            IWebElement element;

            switch (findBy)
            {
                case FinderStrategy.Id:
                    element = FindById(domElement);
                    break;
                case FinderStrategy.Name:
                    element = FindByName(domElement);
                    break;
                case FinderStrategy.Css:
                    element = FindByCssSelector(domElement);
                    break;
                case FinderStrategy.ClassName:
                    element = FindByClassName(domElement);
                    break;
                case FinderStrategy.LinkText:
                    element = FindByLink(domElement);
                    break;
                case FinderStrategy.PartialLinkText:
                    element = FindByPartialLinkText(domElement);
                    break;
                case FinderStrategy.TagName:
                    element = FindByTagName(domElement);
                    break;
                case FinderStrategy.XPath:
                    element = FindByXPath(domElement);
                    break;
                default:
                    throw new NotSupportedException(string.Format("{0} is not a supported finder strategy.", findBy));
            }
            return element;
        }


        private void HoverOverElement(IWebElement element)
        {
            var action = new Actions(WebDriver);
            // Move to the Main Menu Element and hover  
            action.MoveToElement(element).Perform();             
        }
    }
}
