using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Bidro.Types;

namespace Bidro.EntityObjects;

public record FormQuestion(
    string Label,
    int OrderInForm,
    InputTypes InputType,
    bool IsRequired,
    Guid SubcategoryId,
    string DefaultAnswer)
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    [StringLength(50)] public string Label { get; set; } = Label;

    public int OrderInForm { get; set; } = OrderInForm;
    public InputTypes InputType { get; set; } = InputType;
    public bool IsRequired { get; set; } = IsRequired;
    public Guid SubcategoryId { get; set; } = SubcategoryId;

    [StringLength(50)] public string DefaultAnswer { get; set; } = DefaultAnswer;

    public Subcategory? Subcategory { get; set; }
    public ICollection<ListingComponents.FormAnswer>? Answers { get; set; }
}