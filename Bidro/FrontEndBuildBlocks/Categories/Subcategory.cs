using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Bidro.Config;
using Bidro.FrontEndBuildBlocks.Categories.Persistence;

namespace Bidro.FrontEndBuildBlocks.Categories;

public record Subcategory
{
    [Key]
    [StringLength(Constants.SubcategoryIdLength)]
    public string Id { get; set; }
    
    [Required]
    [StringLength(50)]
    public string Name { get; }

    [Required]
    [StringLength(50)]
    public string Icon { get; }
    
    [Required]
    [StringLength(Constants.CategoryIdLength)]
    [ForeignKey(nameof(Category))]
    public string ParentCategoryId { get; set; }
    
    [Required]
    public required Category ParentCategory { get; set; } 
    
    public Subcategory(string parentCategoryId, string name, string icon, string id)
    {
        Id = id;
        if(CheckNameSubcategory(name) && CheckIconSubcategory(icon))
        {
            ParentCategoryId = parentCategoryId;
            Name = name;
            Icon = icon;
        }
        else
        {
            throw new ArgumentException("Invalid Subcategory");
        }
    }
    
    
    public static bool CheckNameSubcategory(string name)
    {
        return name.Length > 0;
    }
    
    public static bool CheckIconSubcategory(string icon)
    {
        return icon.Length > 0;
    }
    
    public override string ToString()
    {
        return $"ParentCategoryId: {ParentCategoryId}, Name: {Name}, Icon: {Icon}";
    }
    
}
