using Bidro.FrontEndBuildBlocks.Categories;
using Bidro.Listings;

namespace Bidro.FrontEndBuildBlocks.Forms;

public record FormQuestion
{
    public Guid Id { get; init; }
    private int Order { get; }

    private string Label { get; }

    private InputTypes InputType { get; }

    private bool Required { get; }
    
    public virtual required Subcategory Subcategory { get; init; }
    private string SubcategoryId { get; }
    public virtual required ICollection<ListingComponents.FormAnswer> Answers { get; init; }

    public FormQuestion( string label, InputTypes inputType, bool required, int order, string subcategoryId)
    {
        if(CheckFormQuestion(subcategoryId, order, subcategoryId))
        {
            Label = label;
            InputType = inputType;
            Required = required;
            Order = order;
            SubcategoryId = subcategoryId;
        }
        else
        {
            throw new ArgumentException("Invalid FormQuestion");
        }
    }
    
    private static bool CheckFormQuestion( string label, int order, string subcategoryId)
    {
        return 
               CheckLabelFormQuestion(label)
               && CheckOrderFormQuestion(order)
               && CheckSubcategoryIdFormQuestion(subcategoryId);
    }
    
    private static bool CheckSubcategoryIdFormQuestion(string subcategoryId)
    {
        return subcategoryId.Length == 3;
    }
    
    private static bool CheckOrderFormQuestion(int order)
    {
        return order > 0;
    }
    
    
    private static bool CheckLabelFormQuestion(string label)
    {
        return label.Length > 0;
    }
    
    public override string ToString()
    {
        return $"Label: {Label}, InputType: {InputType}, Required: {Required}";
    }
}