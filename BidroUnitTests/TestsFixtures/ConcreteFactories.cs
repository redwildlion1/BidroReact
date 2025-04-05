using Bidro.Config;
using Bidro.Controllers;
using Bidro.Services.Implementations;

namespace BidroUnitTests.TestsFixtures;

public class CategoriesServiceFactory : IServiceFactory<CategoriesService>
{
    public CategoriesService CreateService(PgConnectionPool dbConnection)
    {
        return new CategoriesService(dbConnection);
    }
}

public class CategoriesControllerFactory : IControllerFactory<CategoriesController, CategoriesService>
{
    public CategoriesController CreateController(CategoriesService service, PgConnectionPool dbConnection)
    {
        return new CategoriesController(service, dbConnection);
    }
}

// Firms
public class FirmsServiceFactory : IServiceFactory<FirmsService>
{
    public FirmsService CreateService(PgConnectionPool dbConnection)
    {
        return new FirmsService(dbConnection);
    }
}

public class FirmsControllerFactory : IControllerFactory<FirmsController, FirmsService>
{
    public FirmsController CreateController(FirmsService service, PgConnectionPool dbConnection)
    {
        return new FirmsController(service, dbConnection);
    }
}

// Listings
public class ListingsServiceFactory : IServiceFactory<ListingsService>
{
    public ListingsService CreateService(PgConnectionPool dbConnection)
    {
        return new ListingsService(dbConnection);
    }
}

public class ListingsControllerFactory : IControllerFactory<ListingsController, ListingsService>
{
    public ListingsController CreateController(ListingsService service, PgConnectionPool dbConnection)
    {
        return new ListingsController(service, dbConnection);
    }
}

// LocationComponents

public class LocationComponentsServiceFactory : IServiceFactory<LocationComponentsService>
{
    public LocationComponentsService CreateService(PgConnectionPool dbConnection)
    {
        return new LocationComponentsService(dbConnection);
    }
}

public class
    LocationComponentsControllerFactory : IControllerFactory<LocationComponentsController, LocationComponentsService>
{
    public LocationComponentsController CreateController(LocationComponentsService service,
        PgConnectionPool dbConnection)
    {
        return new LocationComponentsController(service, dbConnection);
    }
}