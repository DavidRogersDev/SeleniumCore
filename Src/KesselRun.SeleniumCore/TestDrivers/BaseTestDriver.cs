using System.Linq;
using System.Threading;
using KesselRun.SeleniumCore.Enums;
using KesselRun.SeleniumCore.Exceptions;
using KesselRun.SeleniumCore.Infrastructure;
using KesselRun.SeleniumCore.TestDrivers.Contracts;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using DriverOptions = KesselRun.SeleniumCore.Infrastructure.DriverOptions;

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

            if (string.IsNullOrWhiteSpace(url))
            {
                url = DefaultUrl;
            }

            if (!url.StartsWith(HttpPrefix) && !url.StartsWith(HttpSslPrefix))
            {
                //  Assume not SSL if dev is too lazy to type in "https://"
                url = string.Concat(HttpPrefix, url);
            }

            navigation.GoToUrl(url);
        }

        public virtual bool ElementContainsText(FinderStrategy findBy, string domElement, string text, int? seconds = null)
        {
            var element = GetWebElementByFinderStrategy(findBy, domElement, seconds: seconds);

            return !ReferenceEquals(null, element) && (element.Text.Contains(text));
        }

        public bool ElementIsNotInDom(FinderStrategy findBy, string domElement)
        {
            switch (findBy)
            {
                case FinderStrategy.Id:
                    return !WebDriver.FindElements(By.Id(domElement)).Any();
                case FinderStrategy.Name:
                    return !WebDriver.FindElements(By.Name(domElement)).Any();
                case FinderStrategy.Css:
                    return !WebDriver.FindElements(By.CssSelector(domElement)).Any();
                case FinderStrategy.ClassName:
                    return !WebDriver.FindElements(By.ClassName(domElement)).Any();
                case FinderStrategy.LinkText:
                    return !WebDriver.FindElements(By.LinkText(domElement)).Any();
                case FinderStrategy.PartialLinkText:
                    return !WebDriver.FindElements(By.PartialLinkText(domElement)).Any();
                case FinderStrategy.TagName:
                    return !WebDriver.FindElements(By.TagName(domElement)).Any();
                case FinderStrategy.XPath:
                    return !WebDriver.FindElements(By.XPath(domElement)).Any();
                default:
                    throw new NotSupportedException(string.Format("{0} is not a supported finder strategy.", findBy));
            }
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

        public bool FindByIdClickWithRetries(
            string domElement,
            ExpectedCondition expectedCondition = ExpectedCondition.ElementIsVisible,
            int? seconds = null,
            int? numberOfRetries = 20)
        {
            Action action = () =>
            {
                IWebElement element = FindById(domElement, expectedCondition, seconds);
                ClickWebElement(domElement, seconds, element, FinderStrategy.Id);
            };

            return ExecuteActionWithRetries(action, numberOfRetries);
        }

        public bool FindByClassNameClickWithRetries(
            string domElement, 
            ExpectedCondition expectedCondition = ExpectedCondition.ElementIsVisible,
            int? seconds = null, int? numberOfRetries = 20)
        {
            Action action = () =>
            {
                IWebElement element = FindByClassName(domElement, expectedCondition, seconds);
                ClickWebElement(domElement, seconds, element, FinderStrategy.Id);
            };

            return ExecuteActionWithRetries(action, numberOfRetries);
        }

        public bool FindByCssSelectorClickWithRetries(string domElement, ExpectedCondition expectedCondition = ExpectedCondition.ElementIsVisible,
            int? seconds = null, int? numberOfRetries = 20)
        {
            Action action = () =>
            {
                IWebElement element = FindByCssSelector(domElement, expectedCondition, seconds);
                ClickWebElement(domElement, seconds, element, FinderStrategy.Id);
            };

            return ExecuteActionWithRetries(action, numberOfRetries);
        }

        public bool FindByLinkClickWithRetries(string domElement, ExpectedCondition expectedCondition = ExpectedCondition.ElementIsVisible,
            int? seconds = null, int? numberOfRetries = 20)
        {
            Action action = () =>
            {
                IWebElement element = FindByLink(domElement, expectedCondition, seconds);
                ClickWebElement(domElement, seconds, element, FinderStrategy.Id);
            };

            return ExecuteActionWithRetries(action, numberOfRetries);
        }

        public bool FindByNameClickWithRetries(string domElement, ExpectedCondition expectedCondition = ExpectedCondition.ElementIsVisible,
            int? seconds = null, int? numberOfRetries = 20)
        {
            Action action = () =>
            {
                IWebElement element = FindByName(domElement, expectedCondition, seconds);
                ClickWebElement(domElement, seconds, element, FinderStrategy.Id);
            };

            return ExecuteActionWithRetries(action, numberOfRetries);
        }

        public bool FindByPartialLinkTextClickWithRetries(string domElement, ExpectedCondition expectedCondition = ExpectedCondition.ElementIsVisible,
            int? seconds = null, int? numberOfRetries = 20)
        {
            Action action = () =>
            {
                IWebElement element = FindByPartialLinkText(domElement, expectedCondition, seconds);
                ClickWebElement(domElement, seconds, element, FinderStrategy.Id);
            };

            return ExecuteActionWithRetries(action, numberOfRetries);

        }

        public bool FindByTagNameClickWithRetries(string domElement, ExpectedCondition expectedCondition = ExpectedCondition.ElementIsVisible,
            int? seconds = null, int? numberOfRetries = 20)
        {
            Action action = () =>
            {
                IWebElement element = FindByTagName(domElement, expectedCondition, seconds);
                ClickWebElement(domElement, seconds, element, FinderStrategy.Id);
            };

            return ExecuteActionWithRetries(action, numberOfRetries);
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

        public bool FindByXPathClickWithRetries(string domElement, ExpectedCondition expectedCondition = ExpectedCondition.ElementIsVisible,
            int? seconds = null, int? numberOfRetries = 20)
        {
            Action action = () =>
            {
                IWebElement element = FindByXPath(domElement, expectedCondition, seconds);
                ClickWebElement(domElement, seconds, element, FinderStrategy.XPath);
            };
            return ExecuteActionWithRetries(action, numberOfRetries);
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

        public virtual bool IsElementChecked(FinderStrategy findBy, string domElement, int? seconds = null)
        {
            var element = GetWebElementByFinderStrategy(findBy, domElement, seconds: seconds);

            if (ReferenceEquals(null, element))
                return false;

            return element.Selected;
        }

        public virtual Actions MouseOverElement(FinderStrategy findBy, string domElement, int? seconds = null)
        {
            var element = GetWebElementByFinderStrategy(findBy, domElement, seconds: seconds);
            return HoverOverElement(element);
        }
        
        public virtual void MouseOverElementUsingScript(string script)
        {
            if (script == null) throw new ArgumentNullException("script");
            ((IJavaScriptExecutor)WebDriver).ExecuteScript(script);
        }

        public virtual void Quit()
        {
            WebDriver.Quit();
        }

        public virtual IWebDriver SwitchBackToDefault()
        {
            return WebDriver.SwitchTo().DefaultContent();
        }

        public virtual IWebDriver SwitchToIFrame(IWebElement frame)
        {
            if (frame == null) throw new ArgumentNullException("frame");

            return WebDriver.SwitchTo().Frame(frame);
        }

        public virtual IWebElement TypeText(IWebElement webElement, string text)
        {
            if (ReferenceEquals(webElement ,null))
                return null;

            webElement.Clear();
            webElement.SendKeys(text);

            return webElement;
        }

        public virtual IWebElement TypeText(FinderStrategy findBy, string domElement, string text, int? seconds = null)
        {
            if (string.IsNullOrWhiteSpace(domElement)) 
                throw new ArgumentNullException("domElement");

            IWebElement element = GetWebElementByFinderStrategy(findBy, domElement, seconds: seconds);

            if (ReferenceEquals(element, null))
                throw new ElementWasNullException(findBy, domElement, seconds);

            return TypeText(element, text);
        }

        public virtual IWebElement TypeText(FinderStrategy findBy, string domElement, string text, InputGesture inputGesture, int? seconds = null)
        {
            IWebElement element = TypeText(findBy, domElement, text, seconds: seconds);

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

        public virtual IWebElement FindWithWait(int seconds, Func<IWebDriver, IWebElement> expectedFunc)
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

        private IWebElement GetWebElementByFinderStrategy(FinderStrategy findBy, string domElement, int? seconds = null)
        {
            IWebElement element;

            switch (findBy)
            {
                case FinderStrategy.Id:
                    element = FindById(domElement, seconds: seconds);
                    break;
                case FinderStrategy.Name:
                    element = FindByName(domElement, seconds: seconds);
                    break;
                case FinderStrategy.Css:
                    element = FindByCssSelector(domElement, seconds: seconds);
                    break;
                case FinderStrategy.ClassName:
                    element = FindByClassName(domElement, seconds: seconds);
                    break;
                case FinderStrategy.LinkText:
                    element = FindByLink(domElement, seconds: seconds);
                    break;
                case FinderStrategy.PartialLinkText:
                    element = FindByPartialLinkText(domElement, seconds: seconds);
                    break;
                case FinderStrategy.TagName:
                    element = FindByTagName(domElement, seconds: seconds);
                    break;
                case FinderStrategy.XPath:
                    element = FindByXPath(domElement, seconds: seconds);
                    break;
                default:
                    throw new NotSupportedException(string.Format("{0} is not a supported finder strategy.", findBy));
            }
            return element;
        }


        private Actions HoverOverElement(IWebElement element)
        {
            var actions = new Actions(WebDriver);
            // Move to the Main Menu Element and hover  
            actions.MoveToElement(element).Perform();

            return actions;
        }

        private bool ExecuteActionWithRetries(Action action, int? numberOfRetries = 20)
        {
            var result = false;
            var attempts = 0;
            
            while (attempts < numberOfRetries)
            {
                try
                {
                    action();        
                    result = true;
                    break;
                }
                catch (StaleElementReferenceException)
                {
                    //  log or trace here for future release perhaps
                    Thread.Sleep(100);
                }
                catch (Exception)
                {
                    //  log or trace here for future release perhaps
                    Thread.Sleep(100);
                }

                attempts++;
            }
            return result;
        }

        public void GoToDefaultUrl()
        {
            GoToUrl(DefaultUrl);
        }

        public void WaitForReady(int? seconds)
        {
            // Only works for jQuery.

            var waitTime = seconds ?? DefaultWebDriverWait;
            var wait = new WebDriverWait(WebDriver, TimeSpan.FromSeconds(waitTime));

            wait.Until(driver => (bool)((IJavaScriptExecutor)driver).ExecuteScript("return jQuery.active == 0"));
        }

        public void WaitForReadyAndSpinnerDone(int? seconds)
        {
            // Only works for jQuery.

            var waitTime = seconds ?? DefaultWebDriverWait;
            var wait = new WebDriverWait(WebDriver, TimeSpan.FromSeconds(waitTime));

            wait.Until(driver => (bool)((IJavaScriptExecutor)driver).ExecuteScript("return jQuery.active == 0"));

            wait.Until(driver =>
            {
                bool isAjaxFinished = (bool)((IJavaScriptExecutor)driver).ExecuteScript("return jQuery.active == 0");
                bool isLoaderHidden = (bool)((IJavaScriptExecutor)driver).ExecuteScript("return $('.spinner').is(':visible') == false");
                return isAjaxFinished && isLoaderHidden;
            });
        }
    }
}
