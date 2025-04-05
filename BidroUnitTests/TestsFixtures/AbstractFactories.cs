using Bidro.Config;

namespace BidroUnitTests.TestsFixtures;

public interface IServiceFactory<out TService> where TService : class
{
    TService CreateService(PgConnectionPool dbConnection);
}

public interface IControllerFactory<out TController, in TService> where TService : class
{
    TController CreateController(TService service, PgConnectionPool dbConnection);
}