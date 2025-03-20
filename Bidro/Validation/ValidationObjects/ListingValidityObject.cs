using Bidro.DTOs.ListingDTOs;

namespace Bidro.Validation.ValidationObjects;

public class ListingBaseValidityObject(PostDTOs.PostListingBaseDTO listingDTO)
{
    public string Title { get; } = listingDTO.Title;
}

public class ListingLocationValidityObject(PostDTOs.PostLocationDTO listingLocationDTO)
{
    public string Address { get; } = listingLocationDTO.Address;
    public string PostalCode { get; } = listingLocationDTO.PostalCode;
}

public class ListingContactValidityObject(PostDTOs.PostContactDTO listingContactDTO)
{
    public string Name { get; } = listingContactDTO.Name;
    public string Email { get; } = listingContactDTO.Email;
    public string Phone { get; } = listingContactDTO.Phone;
}

//Database validation objects
public class ListingValidityObjectDb(PostDTOs.PostListingDTO listingDTO)
{
    public ListingBaseValidityObjectDb ListingBase { get; } = new(listingDTO.ListingBase);
    public ListingLocationValidityObjectDb Location { get; } = new(listingDTO.Location);
    public List<FormAnswerValidityObjectDb> FormAnswers { get; } = 
        listingDTO.FormAnswers?.Select(f => new FormAnswerValidityObjectDb(f)).ToList()!;
}

public class ListingBaseValidityObjectDb(PostDTOs.PostListingBaseDTO listingDTO)
{
    public Guid SubcategoryId { get; } = listingDTO.SubcategoryId;
    public Guid UserId { get; set; } = listingDTO.UserId;
}

public class ListingLocationValidityObjectDb(PostDTOs.PostLocationDTO listingLocationDTO)
{
    public Guid CountyId { get; } = listingLocationDTO.CountyId;
    public Guid CityId { get; } = listingLocationDTO.CityId;
}

public class FormAnswerValidityObjectDb(PostDTOs.PostFormAnswerDTO formAnswerDTO)
{
    public Guid FormQuestionId { get; } = formAnswerDTO.FormQuestionId;
}

//Not needed
/*public class ListingContactValidityObjectDb(PostDTOs.PostContactDTO listingContactDTO)
{
    public string Name { get; } = listingContactDTO.Name;
    public string Email { get; } = listingContactDTO.Email;
    public string Phone { get; } = listingContactDTO.Phone;
}*/