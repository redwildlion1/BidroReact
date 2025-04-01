using Bidro.DTOs.ListingDTOs;
using Bidro.Services;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Bidro.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ListingsController(IListingsService listingsService) : ControllerBase
{
    [HttpPost("addListing")]
    [SwaggerOperation(Summary = "Add a new listing")]
    public async Task<IActionResult> AddListing(PostDTOs.PostListingDTO listing)
    {
        var result = await listingsService.AddListing(listing);
        return CreatedAtAction(nameof(AddListing), new { listingId = result }, result);
    }

    [HttpGet("getListingById")]
    [SwaggerOperation(Summary = "Get a listing by its ID")]
    public async Task<IActionResult> GetListingById(Guid listingId)
    {
        var result = await listingsService.GetListingById(listingId);
        return Ok(result);
    }
}