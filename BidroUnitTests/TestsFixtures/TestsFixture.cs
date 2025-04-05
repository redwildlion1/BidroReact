using Bidro.Config;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace BidroUnitTests.TestsFixtures;

public class TestFixture<TController, TService> : IDisposable where TService : class
{
    private readonly string _databaseName = $"testdb_{Guid.NewGuid()}"; // Unique test database

    private readonly EntityDbContext _dbContext;

    public readonly PgConnectionPool DbConnection;

    protected TestFixture(IServiceFactory<TService> serviceFactory,
        IControllerFactory<TController, TService> controllerFactory)
    {
        const string sqlCreateExtension = "CREATE EXTENSION IF NOT EXISTS \"uuid-ossp\";";
        const string masterConnectionString = "Host=localhost;Username=postgres;Password=admin;Database=postgres;";

        // Create a new test database
        using (var masterConnection = new NpgsqlConnection(masterConnectionString))
        {
            masterConnection.Open();
            using (var command = masterConnection.CreateCommand())
            {
                command.CommandText = $"CREATE DATABASE \"{_databaseName}\"";
                command.ExecuteNonQuery();
                command.CommandText = sqlCreateExtension;
                command.ExecuteNonQuery();
            }
        }

        var connectionString =
            $"Host=localhost;Username=postgres;Password=admin;Pooling=true;MinPoolSize=5;MaxPoolSize=100;Database={_databaseName}";
        DbConnection = new PgConnectionPool(connectionString);

        var options = new DbContextOptionsBuilder<EntityDbContext>()
            .UseNpgsql(connectionString)
            .Options;

        _dbContext = new EntityDbContext(options);
        _dbContext.Database.Migrate(); // Apply migrations

        // Create service and controller using factories
        Service = serviceFactory.CreateService(DbConnection);
        Controller = controllerFactory.CreateController(Service, DbConnection);
    }

    public TController Controller { get; }
    public TService Service { get; }

    public void Dispose()
    {
        _dbContext.Dispose();
        DbConnection.Dispose();

        // Drop the test database
        using var masterConnection =
            new NpgsqlConnection("Host=localhost;Username=postgres;Password=admin;Database=postgres");
        masterConnection.Open();
        using var command = masterConnection.CreateCommand();
        command.CommandText = $"DROP DATABASE \"{_databaseName}\" WITH (FORCE)";
        command.ExecuteNonQuery();
    }
}