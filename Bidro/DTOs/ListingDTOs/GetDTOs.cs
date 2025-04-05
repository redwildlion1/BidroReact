using Bidro.DTOs.FormQuestionsDTOs;
using Bidro.EntityObjects;

namespace Bidro.DTOs.ListingDTOs;

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
            GetLocationDTO.FromLocation(listing.Location!),
            GetContactDTO.FromContact(listing.Contact!),
            listing.FormAnswers!.Select(GetFormAnswerDTO.FromFormAnswer).ToList()
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
    public static GetLocationDTO FromLocation(ListingLocation listingLocation)
    {
        return new GetLocationDTO(
            listingLocation.CountyId,
            listingLocation.CityId,
            listingLocation.Address,
            listingLocation.PostalCode
        );
    }
}

public record GetContactDTO(
    string Name,
    string Email,
    string Phone)
{
    public static GetContactDTO FromContact(ListingContact listingContact)
    {
        return new GetContactDTO(
            listingContact.Name,
            listingContact.Email,
            listingContact.Phone
        );
    }
}

public record GetFormAnswerDTO(
    string Value,
    Guid Id,
    Guid FormQuestionId,
    GetFormQuestionDTO Question)
{
    public static GetFormAnswerDTO FromFormAnswer(FormAnswer formAnswer)
    {
        return new GetFormAnswerDTO(
            formAnswer.Answer,
            formAnswer.Id,
            formAnswer.FormQuestionId,
            GetFormQuestionDTO.FromFormQuestion(formAnswer.Question!)
        );
    }
}