using System.Net;
using Bidro.Config;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Bidro.LocationComponents.Persistence;

public class LocationComponentsDb(DbContextOptions<EntityDbContext> options) : ILocationComponentsDb
{
    
    public async Task<IActionResult> AddCounty(County county)
    {
        await using var db = new EntityDbContext(options);
        await db.Counties.AddAsync(county);
        await db.SaveChangesAsync();
        return new OkResult();
    }

    public async Task<IActionResult> AddCity(City city)
    {
        await using var db = new EntityDbContext(options);
        await db.Cities.AddAsync(city);
        await db.SaveChangesAsync();
        return new OkResult();
    }
}