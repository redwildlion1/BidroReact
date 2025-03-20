using Bidro.EntityObjects;

namespace Bidro.DTOs.ListingDTOs;

public static class PostDTOs
{
    public record PostListingDTO(
        PostListingBaseDTO ListingBase,
        PostLocationDTO Location,
        PostContactDTO Contact,
        List<PostFormAnswerDTO> FormAnswers)
    {
        public Listing ToListing()
        {
            return new Listing(
                ListingBase.Title,
                ListingBase.SubcategoryId,
                ListingBase.UserId);
        }
    }

    public record PostListingBaseDTO(
        string Title,
        Guid SubcategoryId,
        Guid UserId);


    public record PostLocationDTO(
        Guid CountyId,
        Guid CityId,
        string Address,
        string PostalCode)
    {
        public ListingLocation ToListingLocation()
        {
            return new ListingLocation(
                CountyId,
                CityId,
                Address,
                PostalCode);
        }
    }

    public record PostContactDTO(
        string Name,
        string Email,
        string Phone)
    {
        public ListingContact ToListingContact()
        {
            return new ListingContact(
                Name,
                Email,
                Phone);
        }
    }

    public record PostFormAnswerDTO(
        string Value,
        Guid FormQuestionId);
}