using Bidro.Config;
using Dapper;

namespace Bidro.Validation.FluentValidators.LittleValidators;

public static class VerifyIfExists
{
    public static async Task<bool> FirmExists(PgConnectionPool pgConnectionPool, Guid id)
    {
        using var connection = await pgConnectionPool.RentAsync();
        const string query = "SELECT COUNT(*) FROM \"Firms\" WHERE \"Id\" = @Id";
        var count = await connection.ExecuteScalarAsync<int>(query, new { Id = id });
        return count > 0;
    }

    public static async Task<bool> CategoryExists(PgConnectionPool pgConnectionPool, Guid id)
    {
        using var connection = await pgConnectionPool.RentAsync();
        const string query = "SELECT COUNT(*) FROM \"Categories\" WHERE \"Id\" = @Id";
        var count = await connection.ExecuteScalarAsync<int>(query, new { Id = id });
        return count > 0;
    }

    public static async Task<bool> SubcategoryExists(PgConnectionPool pgConnectionPool, Guid id)
    {
        using var connection = await pgConnectionPool.RentAsync();
        const string query = "SELECT COUNT(*) FROM \"Subcategories\" WHERE \"Id\" = @Id";
        var count = await connection.ExecuteScalarAsync<int>(query, new { Id = id });
        return count > 0;
    }
}