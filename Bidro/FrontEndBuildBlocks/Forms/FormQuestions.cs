namespace Bidro.FrontEndBuildBlocks.Forms;

public record FormQuestion
{
    public Guid Id { get; }
    
    public string SubcategoryId { get; }
    
    public int Order { get; }

    public string Label { get; }

    public InputTypes InputType { get; }

    public bool Required { get; }

    public FormQuestion(Guid id, string label, InputTypes inputType, bool required, int order, string subcategoryId)
    {
        if(CheckFormQuestion(id, subcategoryId, order, subcategoryId))
        {
            Id = id;
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
    
    private static bool CheckFormQuestion(Guid id, string label, int order, string subcategoryId)
    {
        return CheckIdFormQuestion(id)
               && CheckLabelFormQuestion(label)
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

    private static bool CheckIdFormQuestion(Guid id)
    {
        return id != Guid.Empty;
    }
    
    private static bool CheckLabelFormQuestion(string label)
    {
        return label.Length > 0;
    }
    
    public override string ToString()
    {
        return $"Id: {Id}, Label: {Label}, InputType: {InputType}, Required: {Required}";
    }
}