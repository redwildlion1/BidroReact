using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Bidro.Firms;
namespace Bidro.Reviews;

public class Review(Guid id, string content, int rating, DateTime date, Guid firmId)
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; init; } = id;

    [StringLength(100)]
    public string Content { get; init; } = content;

    public int Rating { get; init; } = rating;

    public DateTime Date { get; init; } = date;

    public Guid FirmId { get; init; } = firmId;
    public Firm? Firm { get; init; }
}