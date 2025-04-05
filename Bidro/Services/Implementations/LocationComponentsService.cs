using Bidro.Config;
using Bidro.DTOs.LocationComponentsDTOs;
using Bidro.EntityObjects;
using Dapper;

namespace Bidro.Services.Implementations;

public class LocationComponentsService(PgConnectionPool pgConnectionPool) : ILocationComponentsService
{
    public async Task<GetCountyDTO> AddCounty(PostCountyDTO postCounty)
    {
        const string sql =
            "INSERT INTO \"Counties\" (\"Name\", \"Code\") VALUES (@Name, @Code) RETURNING \"Id\"";

        using var db = await pgConnectionPool.RentAsync();
        var id = await db.ExecuteScalarAsync<Guid>(sql, postCounty);
        return new GetCountyDTO(
            id,
            postCounty.Name,
            postCounty.Code
        );
    }

    public async Task<IEnumerable<GetCountyDTO>> GetAllCounties()
    {
        const string sql = "SELECT * FROM \"Counties\"";

        using var db = await pgConnectionPool.RentAsync();
        var counties = await db.QueryAsync<County>(sql);
        return counties.Select(c => new GetCountyDTO(
            c.Id,
            c.Name,
            c.Code
        )).ToList();
    }

    public async Task<GetCityDTO> AddCity(PostCityDTO postCity)
    {
        const string sql =
            "INSERT INTO \"Cities\" (\"CountyId\", \"Name\") VALUES (@CountyId, @Name) RETURNING \"Id\"";

        using var db = await pgConnectionPool.RentAsync();
        var id = await db.ExecuteScalarAsync<Guid>(sql, postCity);
        return new GetCityDTO(
            id,
            postCity.Name,
            postCity.CountyId
        );
    }

    public async Task<IEnumerable<GetCityDTO>> GetAllCities()
    {
        const string sql = "SELECT * FROM \"Cities\"";

        using var db = await pgConnectionPool.RentAsync();
        var cities = await db.QueryAsync<City>(sql);
        return cities.Select(c => new GetCityDTO(
            c.Id,
            c.Name,
            c.CountyId
        )).ToList();
    }
}