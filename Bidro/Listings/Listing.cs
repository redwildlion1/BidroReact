using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bidro.Listings;

public sealed class Listing(
    Guid id,
    string title)
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; init; } = id;

    [Required]
    [StringLength(50)]
    public string Title { get; init; } = title;

    public required ListingComponents.Location Location { get; init; } 

    public  required ListingComponents.Contact Contact { get; init; }

    public required  List<ListingComponents.FormAnswer> FormAnswers { get; init; }
}