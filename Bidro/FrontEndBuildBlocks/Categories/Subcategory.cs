using System.ComponentModel.DataAnnotations;
using Bidro.Config;

namespace Bidro.FrontEndBuildBlocks.Categories;

public record Subcategory
{
    [Required]
    [StringLength(Constants.CategoryIdLength)]
    public string ParentCategoryId { get; }

    [Required]
    [StringLength(Constants.SubcategoryIdLength)]
    public string Id { get; }

    [Required]
    [StringLength(50)]
    public string Name { get; }

    [Required]
    [StringLength(50)]
    public string Icon { get; }
    
    public Subcategory(string parentCategoryId, string id, string name, string icon)
    {
        if(CheckIdSubcategory(id) && CheckNameSubcategory(name) && CheckIconSubcategory(icon))
        {
            ParentCategoryId = parentCategoryId;
            Id = id;
            Name = name;
            Icon = icon;
        }
        else
        {
            throw new ArgumentException("Invalid Subcategory");
        }
    }
    
    private static bool CheckIdSubcategory(string id)
    {
        return id.Length == Constants.SubcategoryIdLength;
    }
    
    private static bool CheckNameSubcategory(string name)
    {
        return name.Length > 0;
    }
    
    private static bool CheckIconSubcategory(string icon)
    {
        return icon.Length > 0;
    }
    
    public override string ToString()
    {
        return $"ParentCategoryId: {ParentCategoryId}, Id: {Id}, Name: {Name}, Icon: {Icon}";
    }
    
}
