using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Bidro.Services.Implementations;

namespace Bidro.EntityObjects;

public sealed record ListingLocation(
    Guid CountyId,
    Guid CityId,
    string Address,
    string PostalCode)
{
    [Required] public Guid ListingId { get; set; }
    [Required] public County? County { get; init; }

    [Required] public City? City { get; init; }

    [Required] public Listing? Listing { get; init; }
}

public sealed record ListingContact(string Name, string Email, string Phone)
{
    [Required] public Guid ListingId { get; set; }
    public Listing? Listing { get; init; }
}

public sealed record FormAnswer(
    string Value,
    [property: DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    Guid Id,
    Guid FormQuestionId,
    Guid ListingId)
{
    [Required] public FormQuestion? Question { get; init; }

    [Required] public Listing? Listing { get; init; }
}