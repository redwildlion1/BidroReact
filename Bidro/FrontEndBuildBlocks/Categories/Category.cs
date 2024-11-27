using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Bidro.Config;


namespace Bidro.FrontEndBuildBlocks.Categories;

public record Category
{
    [Required]
    [StringLength(Constants.CategoryIdLength)]
    public string Id { get; }

    [Required]
    [StringLength(50)]
    public string Name { get; }

    [Required]
    [StringLength(50)]
    public string Icon { get; }

    public Category(string id, string name, string icon)
    {
        if(CheckIdCategory(id) && CheckNameCategory(name) && CheckIconCategory(icon))
        {
            Id = id;
            Name = name;
            Icon = icon;
        }
        else
        {
            throw new ArgumentException("Invalid Category");
        }
    }

    private static bool CheckIdCategory(string id)
    {
        return id.Length == Constants.CategoryIdLength;
    }
    
    private static bool CheckNameCategory(string name)
    {
        return name.Length > 0;
    }
    
    private static bool CheckIconCategory(string icon)
    {
        return icon.Length > 0;
    }
    
    public override string ToString()
    {
        return $"Id: {Id}, Name: {Name}, Icon: {Icon}";
    }
    
    public readonly struct CategoryWithSubcategories(Category category, List<Subcategory> subcategories)
    {
        public Category Category { get; } = category;

        public List<Subcategory> Subcategories { get; } = subcategories;
    }
}
