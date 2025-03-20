using Bidro.DTOs.ListingDTOs;
using Microsoft.AspNetCore.Mvc;

namespace Bidro.Services;

public interface IListingsService
{
    public Task<IActionResult> AddListing(PostDTOs.PostListingDTO listing);
    public Task<IActionResult> GetListingById(Guid listingId);
    public Task<IActionResult> DeleteListing(Guid listingId);
}