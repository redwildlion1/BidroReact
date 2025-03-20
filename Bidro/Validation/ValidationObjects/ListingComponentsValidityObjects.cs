using Bidro.DTOs.ListingDTOs;

namespace Bidro.Validation.ValidationObjects;

public class ListingLocationValidityObject(PostDTOs.PostLocationDTO listingLocationDTO)
{
    public string Address { get; } = listingLocationDTO.Address;
    public string PostalCode { get; } = listingLocationDTO.PostalCode;
}

public class ListingContactValidityObject(PostDTOs.PostContactDTO listingContactDTO)
{
    public string Email { get; } = listingContactDTO.Email;
    public string Phone { get; } = listingContactDTO.Phone;
}

//Database validation objects

public class ListingLocationValidityObjectDb(PostDTOs.PostLocationDTO listingLocationDTO)
{
    public Guid CountyId { get; } = listingLocationDTO.CountyId;
    public Guid CityId { get; } = listingLocationDTO.CityId;
}