using Bidro.EntityObjects;

namespace Bidro.DTOs.ListingDTOs;

public class GetDTOs
{
    public record GetListingDTO(
        Guid Id,
        string Title,
        Guid SubcategoryId,
        Guid UserId,
        GetLocationDTO Location,
        GetContactDTO Contact,
        List<GetFormAnswerDTO> FormAnswers)
    {
        public static GetListingDTO FromListing(Listing listing)
        {
            return new GetListingDTO(
                listing.Id,
                listing.Title,
                listing.SubcategoryId,
                listing.UserId,
                GetLocationDTO.FromLocation(listing.Location),
                GetContactDTO.FromContact(listing.Contact),
                listing.FormAnswers.Select(GetFormAnswerDTO.FromFormAnswer).ToList()
            );
        }
    }

    public record GetLocationDTO(
        Guid CountyId,
        Guid CityId,
        string Address,
        string PostalCode
    )
    {
        public static GetLocationDTO FromLocation(ListingComponents.Location location)
        {
            return new GetLocationDTO(
                location.CountyId,
                location.CityId,
                location.Address,
                location.PostalCode
            );
        }
    }

    public record GetContactDTO(
        string Name,
        string Email,
        string Phone)
    {
        public static GetContactDTO FromContact(ListingComponents.Contact contact)
        {
            return new GetContactDTO(
                contact.Name,
                contact.Email,
                contact.Phone
            );
        }
    }

    public record GetFormAnswerDTO(
        string Value,
        Guid Id,
        Guid FormQuestionId,
        FormQuestionsDTOs.GetDTOs.GetFormQuestionDTO Question)
    {
        public static GetFormAnswerDTO FromFormAnswer(ListingComponents.FormAnswer formAnswer)
        {
            return new GetFormAnswerDTO(
                formAnswer.Value,
                formAnswer.Id,
                formAnswer.FormQuestionId,
                FormQuestionsDTOs.GetDTOs.GetFormQuestionDTO.FromFormQuestion(formAnswer.Question)
            );
        }
    }
}