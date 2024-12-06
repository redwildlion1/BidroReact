using Microsoft.AspNetCore.Mvc;

namespace Bidro.Listings.Persistence;

public interface IListingsDb
{
    public Task<IActionResult> AddListing(Listing listing);
    public Task<IActionResult> GetListingById(Guid listingId);
}