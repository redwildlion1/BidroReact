using Bidro.DTOs.ListingDTOs;

namespace Bidro.Validation.ValidationObjects;

public class ListingValidityObject(PostDTOs.PostListingDTO listingDTO)
{
    public string Title { get; } = listingDTO.Title;
}

public class ListingValidityObjectDb(PostDTOs.PostListingDTO listingDTO)
{
    public Guid SubcategoryId { get; } = listingDTO.SubcategoryId;
    public Guid UserId { get; set; } = listingDTO.UserId;
}