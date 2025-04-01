using System.Data;
using Bidro.DTOs.LocationComponentsDTOs;
using Bidro.EntityObjects;
using Dapper;

namespace Bidro.Services.Implementations;

public class LocationComponentsService(IDbConnection db) : ILocationComponentsService
{
    public async Task<GetDTOs.GetCountyDTO> AddCounty(PostDTOs.PostCountyDTO postCounty)
    {
        const string sql =
            "INSERT INTO \"Counties\" (\"Name\", \"Code\") VALUES (@Name, @Code) RETURNING \"Id\"";
        var id = await db.ExecuteScalarAsync<Guid>(sql, postCounty);
        return new GetDTOs.GetCountyDTO(
            id,
            postCounty.Name,
            postCounty.Code
        );
    }

    public async Task<IEnumerable<GetDTOs.GetCountyDTO>> GetAllCounties()
    {
        const string sql = "SELECT * FROM \"Counties\"";
        var counties = await db.QueryAsync<County>(sql);
        return counties.Select(c => new GetDTOs.GetCountyDTO(
            c.Id,
            c.Name,
            c.Code
        )).ToList();
    }

    public async Task<GetDTOs.GetCityDTO> AddCity(PostDTOs.PostCityDTO postCity)
    {
        const string sql =
            "INSERT INTO \"Cities\" (\"CountyId\", \"Name\") VALUES (@CountyId, @Name) RETURNING \"Id\"";
        var id = await db.ExecuteScalarAsync<Guid>(sql, postCity);
        return new GetDTOs.GetCityDTO(
            id,
            postCity.Name,
            postCity.CountyId
        );
    }

    public async Task<IEnumerable<GetDTOs.GetCityDTO>> GetAllCities()
    {
        const string sql = "SELECT * FROM \"Cities\"";
        var cities = await db.QueryAsync<City>(sql);
        return cities.Select(c => new GetDTOs.GetCityDTO(
            c.Id,
            c.Name,
            c.CountyId
        )).ToList();
    }
}