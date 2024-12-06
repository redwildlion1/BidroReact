using Bidro.Listings;
using Bidro.Listings.Persistence;
using Microsoft.AspNetCore.Mvc;

namespace Bidro.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ListingsController(IListingsDb listingsDb) : ControllerBase
{
    [HttpPost ("addListing")]
    public async Task<IActionResult> AddListing(Listing listing)
    {
        return await listingsDb.AddListing(listing);
    }
    
    [HttpGet ("getListingById")]
    public async Task<IActionResult> GetListingById(Guid listingId)
    {
        return await listingsDb.GetListingById(listingId);
    }
}