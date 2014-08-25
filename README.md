Dave's Selenium Wrapper
=======================
[![Build status](https://ci.appveyor.com/api/projects/status/9dwm5u142s6s47vf)](https://ci.appveyor.com/project/DavidRogersDev/seleniumcore)

This library is a wrapper around the C# Selenium bindings, to provide a sipmle API for driving the Selenium Webdriver. I feel it's a bit grandiose to call it a framework, so lets just stick with wrapper.

It has a couple of dependancies which can easily be added via nuget:

- Selenium.WebDriver (obviously)
- Selenium.Support

From the sample project:
```csharp    
ITestDriverFactory testDriverFactory = new TestDriverFactory(
	new DriverOptions
	{
		DriverExePath = ConfigurationManager.AppSettings["FirefoxExePath"],
		Port = int.Parse(ConfigurationManager.AppSettings["FirefoxBrowserPort"]),
		Url = ConfigurationManager.AppSettings["StartUrl"]
	}, "Firefox");

var firefoxWebDriver = testDriverFactory.CreateTestDriver();

firefoxWebDriver.GoToUrl(null); // will use default Url passed in to factory as part of DriverOptions struct

firefoxWebDriver.MouseOverElement(FinderStrategy.Id, "menuLink2");
firefoxWebDriver.FindByIdClick("menuLink2_1");

var heading = firefoxWebDriver.FindByCssSelectorFromWebElement(firefoxWebDriver.FindByClassName("maintd", ExpectedCondition.ElementIsVisible, 5), "h1");

Console.WriteLine(heading.Text);


firefoxWebDriver.Quit();
```