namespace Bidro.FrontEndBuildBlocks.Categories.DTOs;

public record AddCategoryDTO(string Name, string Icon, string Id)
{
    public override string ToString()
    {
        return $" Name: {Name}, Icon: {Icon}";
    }
    
    public Category ToCategory()
    {
        return new Category(Name, Icon, Id);
    }
}