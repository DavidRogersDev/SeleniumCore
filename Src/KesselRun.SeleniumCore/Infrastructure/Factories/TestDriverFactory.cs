using System;
using KesselRun.SeleniumCore.Enums;
using KesselRun.SeleniumCore.Infrastructure.Factories.Contracts;
using KesselRun.SeleniumCore.TestDrivers.Browsers.Chrome;
using KesselRun.SeleniumCore.TestDrivers.Browsers.Firefox;
using KesselRun.SeleniumCore.TestDrivers.Browsers.Ie;
using KesselRun.SeleniumCore.TestDrivers.Contracts;

namespace KesselRun.SeleniumCore.Infrastructure.Factories
{
    public class TestDriverFactory : ITestDriverFactory 
    {
        private readonly string _testDriverType;
        private readonly DriverOptions _driverOptions;

        public TestDriverFactory(DriverOptions driverOptions)
        {
            _driverOptions = driverOptions;
        }

        public TestDriverFactory(DriverOptions driverOptions, string testDriverType)
        {
            _testDriverType = testDriverType;
            _driverOptions = driverOptions;
        }

        public virtual ITestDriver CreateTestDriver()
        {
            if(string.IsNullOrWhiteSpace(_testDriverType))
                throw new Exception();

            switch (_testDriverType)
            {
                case Constants.ChromeTestDriver:
                    return GetInitializedChromeTestDriver();
                case Constants.FirefoxTestDriver:
                    return GetInitializedFireFoxTestDriver();
                case Constants.IeTestDriver:
                    return GetInitializedIeTestDriver();
                default:
                    throw new NotSupportedException(string.Format("{0} is not a supported driver", _testDriverType));
            }
        }

        public virtual ITestDriver CreateTestDriver<T>()
        {
            if (typeof (T) == typeof (ChromeTestDriver))
            {
                return GetInitializedChromeTestDriver();
            }
            
            if (typeof(T) == typeof(FirefoxTestDriver))
            {
                return GetInitializedFireFoxTestDriver();
            }
            
            if (typeof(T) == typeof(IeTestDriver))
            {
                return GetInitializedIeTestDriver();
            }

            throw new NotSupportedException(string.Format("{0} is not a supported driver", typeof(T)));
        }

        public virtual ITestDriver CreateTestDriver(DriverType driverType)
        {
            switch (driverType)
            {
                case DriverType.Chrome:
                    return GetInitializedChromeTestDriver();
                case DriverType.Firefox:
                    return GetInitializedFireFoxTestDriver();
                case DriverType.Ie:
                    return GetInitializedIeTestDriver();
                default:
                    throw new NotSupportedException(string.Format("{0} is not a supported driver", driverType));
            }
        }

        private ITestDriver GetInitializedChromeTestDriver()
        {
            var chromeTestDriver = new ChromeTestDriver();
            chromeTestDriver.Initialize(_driverOptions);
            return chromeTestDriver;
        }


        private ITestDriver GetInitializedFireFoxTestDriver()
        {
            var firefoxTestDriver = new FirefoxTestDriver();
            firefoxTestDriver.Initialize(_driverOptions);
            return firefoxTestDriver;
        }

        private ITestDriver GetInitializedIeTestDriver()
        {
            var ieTestDriver = new IeTestDriver();
            ieTestDriver.Initialize(_driverOptions);
            return ieTestDriver;
        }
    }
}
