using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bidro.EntityObjects;

public sealed record ListingLocation(
    Guid CountyId,
    Guid CityId,
    string Address,
    string PostalCode)
{
    [Required] public Guid ListingId { get; set; }
    [Required] public County? County { get; set; }

    [Required] public City? City { get; set; }

    [Required] public Listing? Listing { get; set; }
}

public sealed record ListingContact(string Name, string Email, string Phone)
{
    [Required] public Guid ListingId { get; set; }
    public Listing? Listing { get; set; }
}

public sealed record FormAnswer(
    string Answer,
    Guid FormQuestionId,
    Guid ListingId)
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    [Required] public FormQuestion? Question { get; set; }

    [Required] public Listing? Listing { get; set; }
}