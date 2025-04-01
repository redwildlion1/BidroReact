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
    public Guid Id { get; set; }

    public Guid SubcategoryId { get; set; } = subcategoryId;
    public Guid UserId { get; set; } = userId;

    [Required] [StringLength(50)] public string Title { get; set; } = title;

    public ListingLocation? Location { get; set; }

    public ListingContact? Contact { get; set; }

    public List<FormAnswer>? FormAnswers { get; set; }

    public UserTypes.UserAccount? User { get; set; }

    public Subcategory? Subcategory { get; set; }
}