using Bidro.Listings;
using Bidro.Listings.Persistence;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Bidro.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ListingsController(IListingsDb listingsDb) : ControllerBase
{
    [HttpPost ("addListing")]
    [SwaggerOperation (Summary = "Add a new listing")]
    public async Task<IActionResult> AddListing(Listing listing)
    {
        return await listingsDb.AddListing(listing);
    }
    
    [HttpGet ("getListingById")]
    [SwaggerOperation (Summary = "Get a listing by its ID")]
    public async Task<IActionResult> GetListingById(Guid listingId)
    {
        return await listingsDb.GetListingById(listingId);
    }
}