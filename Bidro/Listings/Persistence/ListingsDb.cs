using System.Net;
using System.Text;
using System.Text.Json;
using Bidro.Config;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Bidro.Listings.Persistence;

public class ListingsDb(DbContextOptions<EntityDbContext> options) : IListingsDb
{
    public async Task<IActionResult> AddListing(Listing listing)
    {
        await using var db = new EntityDbContext(options);
        await db.Listings.AddAsync(listing);
        await db.SaveChangesAsync();
        return new OkResult();
    }

    public async Task<IActionResult> GetListingById(Guid listingId)
    {
        await using var db = new EntityDbContext(options);
        var listing = await db.Listings.FirstOrDefaultAsync(l => l.Id == listingId);
        if (listing == null)
        {
            return new NotFoundResult();
        }
        return new OkObjectResult(listing);
    }
}