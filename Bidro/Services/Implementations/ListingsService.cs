using Bidro.Config;
using Bidro.DTOs.ListingDTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Bidro.Services.Implementations;

public class ListingsService(EntityDbContext db) : IListingsService
{
    public async Task<IActionResult> AddListing(PostDTOs.PostListingDTO listing)
    {
        await db.Listings.AddAsync(listing.ToListing());
        await db.SaveChangesAsync();
        return new OkResult();
    }

    public async Task<IActionResult> GetListingById(Guid listingId)
    {
        var listing = await db.Listings.FirstOrDefaultAsync(l => l.Id == listingId);
        if (listing == null) return new NotFoundResult();
        return new OkObjectResult(listing);
    }


    public async Task<IActionResult> DeleteListing(Guid listingId)
    {
        var listing = await db.Listings.FirstOrDefaultAsync(l => l.Id == listingId);
        if (listing == null) return new NotFoundResult();
        db.Listings.Remove(listing);
        await db.SaveChangesAsync();
        return new OkResult();
    }
}