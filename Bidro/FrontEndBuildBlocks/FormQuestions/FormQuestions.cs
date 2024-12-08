using Bidro.FrontEndBuildBlocks.Categories;
using Bidro.Listings;

namespace Bidro.FrontEndBuildBlocks.FormQuestions;

public sealed record FormQuestion(
    string Label,
    InputTypes InputType,
    int Order,
    bool IsRequired,
    string SubcategoryId,
    Guid Id,
    string Value)
{
    public Guid Id { get; set; } = Id;
    public int Order { get; set; } = Order;
    public InputTypes InputType { get; set; } = InputType;
    public bool IsRequired { get; set; } = IsRequired;
    public string Value { get; set; } = Value;
    public string SubcategoryId { get; set; } = SubcategoryId;
    public Subcategory? Subcategory { get; set; }
    public ICollection<ListingComponents.FormAnswer>? Answers { get; set; }
    
    public override string ToString()
    {
        return $"Label: {Label}, InputType: {InputType}, Required: {IsRequired}";
    }
}