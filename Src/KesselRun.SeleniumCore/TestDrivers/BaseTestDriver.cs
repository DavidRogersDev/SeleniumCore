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

        public virtual void GoToUrl(string url)
        {
            INavigation navigation = WebDriver.Navigate();

            if (string.IsNullOrEmpty(url))
            {
                url = DefaultUrl;
            }

            if (!url.StartsWith(HttpPrefix))
            {
                url = string.Concat(HttpPrefix, url);
            }

            navigation.GoToUrl(url);
        }

        public virtual bool ElementContainsText(FinderStrategy findBy, string domElement, string text)
        {
            var element = GetWebElementByFinderStrategy(findBy, domElement);

            return !ReferenceEquals(null, element) && (element.Text.Contains(text));
        }

        public virtual IWebElement FindByClassName(string className, int? seconds = null)
        {
            return FindWebElement(By.ClassName(className), seconds);
        }

        public virtual IWebElement FindByClassNameClick(string className, int? seconds = null)
        {
            IWebElement element = FindByClassName(className, seconds);

            return ClickWebElement(className, seconds, element, FinderStrategy.ClassName);
        }

        public virtual IWebElement FindByClassNameFromWebElement(IWebElement webElement, string domElement, int? seconds = null)
        {
            return FindWebElementFromWebElement(webElement, By.ClassName(domElement), seconds);
        }

        public virtual IWebElement FindByClassNameFromWebElementClick(IWebElement webElement, string domElement, int? seconds = null)
        {
            IWebElement element = FindByClassNameFromWebElement(webElement, domElement, seconds);

            return ClickWebElement(domElement, seconds, element, FinderStrategy.ClassName);

        }

        public virtual IWebElement FindByCssSelector(string cssSelector, int? seconds = null)
        {
            return FindWebElement(By.CssSelector(cssSelector), seconds);
        }

        public virtual IWebElement FindByCssSelectorClick(string cssSelector, int? seconds = null)
        {
            IWebElement element = FindByCssSelector(cssSelector, seconds);

            return ClickWebElement(cssSelector, seconds, element, FinderStrategy.Css);
        }
        public virtual IWebElement FindByCssSelectorFromWebElement(IWebElement webElement, string domElement, int? seconds = null)
        {
            return FindWebElementFromWebElement(webElement, By.CssSelector(domElement), seconds);
        }

        public virtual IWebElement FindByCssSelectorFromWebElementClick(IWebElement webElement, string domElement, int? seconds = null)
        {
            IWebElement element = FindByCssSelectorFromWebElement(webElement, domElement, seconds);

            return ClickWebElement(domElement, seconds, element, FinderStrategy.Css);
        }

        public virtual IWebElement FindById(string domElement, int? seconds = null)
        {
            return FindWebElement(By.Id(domElement), seconds);
        }

        public virtual IWebElement FindByIdClick(string domElement, int? seconds = null)
        {
            IWebElement element = FindById(domElement, seconds);

            return ClickWebElement(domElement, seconds, element, FinderStrategy.Id);
        }

        public virtual IWebElement FindByIdFromWebElement(IWebElement webElement, string domElement, int? seconds = null)
        {
            return FindWebElementFromWebElement(webElement, By.Id(domElement), seconds);
        }

        public virtual IWebElement FindByIdFromWebElementClick(IWebElement webElement, string domElement, int? seconds = null)
        {
            IWebElement element = FindByIdFromWebElement(webElement, domElement, seconds);

            return ClickWebElement(domElement, seconds, element, FinderStrategy.Id);
        }

        public virtual IWebElement FindByLink(string domElement, int? seconds = null)
        {
            return FindWebElement(By.LinkText(domElement), seconds);
        }

        public virtual IWebElement FindByLinkClick(string domElement, int? seconds = null)
        {
            IWebElement element = FindByLink(domElement, seconds);

            return ClickWebElement(domElement, seconds, element, FinderStrategy.LinkText);
        }

        public virtual IWebElement FindByLinkFromWebElement(IWebElement webElement, string domElement, int? seconds = null)
        {
            return FindWebElementFromWebElement(webElement, By.LinkText(domElement), seconds);
        }

        public virtual IWebElement FindByLinkFromWebElementClick(IWebElement webElement, string domElement, int? seconds = null)
        {
            IWebElement element = FindByLinkFromWebElement(webElement, domElement, seconds);

            return ClickWebElement(domElement, seconds, element, FinderStrategy.LinkText);
        }

        public virtual IWebElement FindByName(string domElement, int? seconds = null)
        {
            return FindWebElement(By.Name(domElement), seconds);
        }

        public virtual IWebElement FindByNameClick(string domElement, int? seconds = null)
        {
            IWebElement element = FindByName(domElement, seconds);

            return ClickWebElement(domElement, seconds, element, FinderStrategy.Name);
        }

        public virtual IWebElement FindByNameFromWebElement(IWebElement webElement, string domElement, int? seconds = null)
        {
            return FindWebElementFromWebElement(webElement, By.Name(domElement), seconds);
        }

        public virtual IWebElement FindByNameFromWebElementClick(IWebElement webElement, string domElement, int? seconds = null)
        {
            IWebElement element = FindByNameFromWebElement(webElement, domElement, seconds);

            return ClickWebElement(domElement, seconds, element, FinderStrategy.Name);
        }

        public virtual IWebElement FindByPartialLinkText(string domElement, int? seconds = null)
        {
            return FindWebElement(By.PartialLinkText(domElement), seconds);
        }

        public virtual IWebElement FindByPartialLinkTextClick(string domElement, int? seconds = null)
        {
            IWebElement element = FindByPartialLinkText(domElement, seconds);

            return ClickWebElement(domElement, seconds, element, FinderStrategy.PartialLinkText);
        }

        public virtual IWebElement FindByPartialLinkTextFromWebElement(IWebElement webElement, string domElement, int? seconds = null)
        {
            return FindWebElementFromWebElement(webElement, By.PartialLinkText(domElement), seconds);
        }

        public virtual IWebElement FindByPartialLinkTextFromWebElementClick(IWebElement webElement, string domElement, int? seconds = null)
        {
            IWebElement element = FindByPartialLinkTextFromWebElement(webElement, domElement, seconds);

            return ClickWebElement(domElement, seconds, element, FinderStrategy.PartialLinkText);
        }

        public virtual IWebElement FindByTagName(string domElement, int? seconds = null)
        {
            return FindWebElement(By.TagName(domElement), seconds);
        }

        public virtual IWebElement FindByTagNameClick(string domElement, int? seconds = null)
        {
            IWebElement element = FindByTagName(domElement, seconds);

            return ClickWebElement(domElement, seconds, element, FinderStrategy.TagName);
        }

        public virtual IWebElement FindByTagNameFromWebElement(IWebElement webElement, string domElement, int? seconds = null)
        {
            return FindWebElementFromWebElement(webElement, By.TagName(domElement), seconds);
        }

        public virtual IWebElement FindByTagNameFromWebElementClick(IWebElement webElement, string domElement, int? seconds = null)
        {
            IWebElement element = FindByTagNameFromWebElement(webElement, domElement, seconds);

            return ClickWebElement(domElement, seconds, element, FinderStrategy.TagName);
        }

        public virtual IWebElement FindByXPath(string domElement, int? seconds = null)
        {
            return FindWebElement(By.XPath(domElement), seconds);
        }

        public virtual IWebElement FindByXPathClick(string domElement, int? seconds = null)
        {
            IWebElement element = FindByXPath(domElement, seconds);

            return ClickWebElement(domElement, seconds, element, FinderStrategy.XPath);
        }

        public virtual IWebElement FindByXPathFromWebElement(IWebElement webElement, string domElement, int? seconds = null)
        {
            return FindWebElementFromWebElement(webElement, By.XPath(domElement), seconds);
        }

        public virtual IWebElement FindByXPathFromWebElementClick(IWebElement webElement, string domElement, int? seconds = null)
        {
            IWebElement element = FindByXPathFromWebElement(webElement, domElement, seconds);

            return ClickWebElement(domElement, seconds, element, FinderStrategy.XPath);
        }

        private IWebElement FindWebElement(By findBy, int? seconds)
        {
            return seconds.HasValue ? FindWithWait(findBy, seconds.Value) : WebDriver.FindElement(findBy);
        }
        private IWebElement FindWebElementFromWebElement(IWebElement webElement, By findBy, int? seconds = null)
        {
            return seconds.HasValue ? FindWithWaitFromElement(webElement, findBy, seconds.Value) : webElement.FindElement(findBy);
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

        private IWebElement FindWithWait(By findBy, int seconds)
        {
            var wait = new WebDriverWait(WebDriver, TimeSpan.FromSeconds(seconds));
            return wait.Until(ExpectedConditions.ElementIsVisible(findBy));
        }        
        
        private IWebElement FindWithWaitFromElement(IWebElement webElement, By findBy, int seconds)
        {
            var wait = new WebDriverWait(WebDriver, TimeSpan.FromSeconds(seconds));
            return wait.Until(webdriver => webElement.FindElement(findBy));
        }

        private IWebElement GetWebElementByFinderStrategy(FinderStrategy findBy, string domElement)
        {
            IWebElement element;

            switch (findBy)
            {
                case FinderStrategy.Id:
                    element = FindById(domElement, DefaultWebDriverWait);
                    break;
                case FinderStrategy.Name:
                    element = FindByName(domElement, DefaultWebDriverWait);
                    break;
                case FinderStrategy.Css:
                    element = FindByCssSelector(domElement, DefaultWebDriverWait);
                    break;
                case FinderStrategy.ClassName:
                    element = FindByClassName(domElement, DefaultWebDriverWait);
                    break;
                case FinderStrategy.LinkText:
                    element = FindByLink(domElement, DefaultWebDriverWait);
                    break;
                case FinderStrategy.PartialLinkText:
                    element = FindByPartialLinkText(domElement, DefaultWebDriverWait);
                    break;
                case FinderStrategy.TagName:
                    element = FindByTagName(domElement, DefaultWebDriverWait);
                    break;
                case FinderStrategy.XPath:
                    element = FindByXPath(domElement, DefaultWebDriverWait);
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
