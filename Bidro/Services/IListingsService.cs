using Bidro.DTOs.ListingDTOs;

namespace Bidro.Services;

public interface IListingsService
{
    public Task<Guid> AddListing(PostListingDTO listing);
    public Task<GetListingDTO> GetListingById(Guid listingId);
    public Task<bool> DeleteListing(Guid listingId);
}