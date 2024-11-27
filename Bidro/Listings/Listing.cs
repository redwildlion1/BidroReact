using System.ComponentModel.DataAnnotations;

namespace Bidro.Listings;

public sealed class Listing
{
    public Listing(Guid id, ListingComponents.Location location, ListingComponents.Contact contact, List<ListingComponents.FormAnswer> formAnswers, string title)
    {
        Id = id;
        Location = location;
        Contact = contact;
        FormAnswers = formAnswers;
        Title = title;
    }

    [Key]
    public Guid Id { get; init; }
    
    [Required]
    [StringLength(50)]
    public string Title { get; init; }
    
    [Required]
    public ListingComponents.Location Location { get; init; }
    
    [Required]
    public ListingComponents.Contact Contact { get; init; }
    
    [Required]
    public List<ListingComponents.FormAnswer> FormAnswers { get; init; }
}