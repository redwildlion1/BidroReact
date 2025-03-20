using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Bidro.Types;

namespace Bidro.EntityObjects;

public sealed class Listing(
    string title,
    Guid subcategoryId,
    Guid userId)
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; init; }

    public Guid SubcategoryId { get; init; } = subcategoryId;
    public Guid UserId { get; init; } = userId;

    [Required] [StringLength(50)] public string Title { get; init; } = title;

    public ListingLocation? Location { get; init; }

    public ListingContact? Contact { get; init; }

    public List<FormAnswer>? FormAnswers { get; init; }

    public UserTypes.UserAccount? User { get; init; }

    public Subcategory? Subcategory { get; init; }
}