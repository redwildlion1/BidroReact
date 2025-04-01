using Bidro.DTOs.ListingDTOs;

namespace Bidro.Services;

public interface IListingsService
{
    public Task<Guid> AddListing(PostDTOs.PostListingDTO listing);
    public Task<GetDTOs.GetListingDTO> GetListingById(Guid listingId);
    public Task<bool> DeleteListing(Guid listingId);
}