using Bidro.Config;
using Bidro.DTOs.ListingDTOs;
using Bidro.Services;
using Bidro.Validation.FluentValidators;
using Bidro.Validation.ValidationObjects;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Bidro.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ListingsController(IListingsService listingsService, PgConnectionPool pgConnectionPool) : ControllerBase
{
    [HttpPost("addListing")]
    [SwaggerOperation(Summary = "Add a new listing")]
    public async Task<IResult> AddListing(PostListingDTO listing)
    {
        var validator = new ListingValidator(pgConnectionPool);
        var validityObject = new ListingValidityObject(listing);
        var validationResult = await validator.ValidateAsync(validityObject);
        if (!validationResult.IsValid) return Results.BadRequest(validationResult.Errors);

        var result = await listingsService.AddListing(listing);
        return Results.Created($"/api/listings/{result}", result);
    }

    [HttpGet("getListingById")]
    [SwaggerOperation(Summary = "Get a listing by its ID")]
    public async Task<IResult> GetListingById(Guid listingId)
    {
        var result = await listingsService.GetListingById(listingId);
        return Results.Ok(result);
    }
}