using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Bidro.Listings;

namespace Bidro.Reviews;

public class Review
{
    public Review(Guid id, string content, int rating, DateTime date, Guid listingId)
    {
        Id = id;
        Content = content;
        Rating = rating;
        Date = date;
        ListingId = listingId;
    }

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; init; }
    
    public string Content { get; init; }
    
    [Required]
    public int Rating { get; init; }
    
    [Required]
    public DateTime Date { get; init; }
    
    [Required]
    public Guid ListingId { get; init; }
    
    public virtual Listing Listing { get; init; }
}