using KesselRun.SeleniumCore.Enums;
using KesselRun.SeleniumCore.TestDrivers.Contracts;

namespace KesselRun.SeleniumCore.Infrastructure.Factories.Contracts
{
    public interface ITestDriverFactory
    {
        ITestDriver CreateTestDriver();
        ITestDriver CreateTestDriver<T>();
        ITestDriver CreateTestDriver(DriverType driverType);
    }
}