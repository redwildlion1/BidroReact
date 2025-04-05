using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bidro.EntityObjects;

public class Review(string reviewText, int rating, DateTime date, Guid firmId, Guid userId)
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; init; }

    [Required] public Guid UserId { get; set; } = userId;

    public User? User { get; init; }

    [StringLength(100)] public string ReviewText { get; init; } = reviewText;

    public int Rating { get; init; } = rating;

    public DateTime Date { get; init; } = date;

    public Guid FirmId { get; init; } = firmId;
    public Firm? Firm { get; init; }
}