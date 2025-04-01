using Bidro.DTOs.ListingDTOs;

namespace Bidro.Validation.ValidationObjects;

public class ListingValidityObject(PostDTOs.PostListingDTO listingDTO)
{
    public ListingBaseValidityObject ListingBase { get; } = new(listingDTO.ListingBase);
    public ListingLocationValidityObject Location { get; } = new(listingDTO.Location);

    public List<FormAnswerValidityObject> FormAnswers { get; } =
        listingDTO.FormAnswers?.Select(f => new FormAnswerValidityObject(f)).ToList()!;
}

public class ListingBaseValidityObject(PostDTOs.PostListingBaseDTO listingDTO)
{
    public string Title { get; } = listingDTO.Title;

    public Guid SubcategoryId { get; } = listingDTO.SubcategoryId;
    public Guid UserId { get; set; } = listingDTO.UserId;
}

public class ListingLocationValidityObject(PostDTOs.PostLocationDTO listingLocationDTO)
{
    public string Address { get; } = listingLocationDTO.Address;
    public string PostalCode { get; } = listingLocationDTO.PostalCode;

    public Guid CountyId { get; } = listingLocationDTO.CountyId;
    public Guid CityId { get; } = listingLocationDTO.CityId;
}

public class ListingContactValidityObject(PostDTOs.PostContactDTO listingContactDTO)
{
    public string Name { get; } = listingContactDTO.Name;
    public string Email { get; } = listingContactDTO.Email;
    public string Phone { get; } = listingContactDTO.Phone;
}

public class FormAnswerValidityObject(PostDTOs.PostFormAnswerDTO formAnswerDTO)
{
    public Guid FormQuestionId { get; } = formAnswerDTO.FormQuestionId;
    public string Value { get; } = formAnswerDTO.Value;
}