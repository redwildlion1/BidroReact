using Bidro.EntityObjects;

namespace Bidro.DTOs.ListingDTOs;

public class PostDTOs
{
    public record PostListingDTO(
        string Title,
        Guid SubcategoryId,
        Guid UserId,
        PostLocationDTO Location,
        PostContactDTO Contact,
        List<PostFormAnswerDTO> FormAnswers)
    {
        public Listing ToListing(Guid id)
        {
            return new Listing(
                id,
                Title,
                SubcategoryId,
                UserId);
        }
    }

    public record PostLocationDTO(
        Guid CountyId,
        Guid CityId,
        string Address,
        string PostalCode);

    public record PostContactDTO(
        string Name,
        string Email,
        string Phone);

    public record PostFormAnswerDTO(
        string Value,
        Guid FormQuestionId);
}