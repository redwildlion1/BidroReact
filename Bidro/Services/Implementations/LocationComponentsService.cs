using Bidro.Config;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Bidro.Services.Implementations;

public class LocationComponentsService(EntityDbContext db) : ILocationComponentsService
{
    public async Task<IActionResult> AddCounty(County county)
    {
        await db.Counties.AddAsync(county);
        await db.SaveChangesAsync();
        return new OkResult();
    }

    public async Task<IActionResult> AddCity(City city)
    {
        await db.Cities.AddAsync(city);
        await db.SaveChangesAsync();
        return new OkResult();
    }

    public async Task<IActionResult> GetAllCounties()
    {
        var counties = await db.Counties.ToListAsync();
        return new OkObjectResult(counties);
    }

    public async Task<IActionResult> GetAllCities()
    {
        var cities = await db.Cities.ToListAsync();
        return new OkObjectResult(cities);
    }
}