using Bidro.EntityObjects;
using Microsoft.AspNetCore.Mvc;

namespace Bidro.Services;

public interface IListingsService
{
    public Task<IActionResult> AddListing(Listing listing);
    public Task<IActionResult> GetListingById(Guid listingId);
    public Task<IActionResult> UpdateListing(Listing listing);
    public Task<IActionResult> DeleteListing(Guid listingId);
}