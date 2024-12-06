using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Bidro.Config;

namespace Bidro.FrontEndBuildBlocks.Categories;

public record Category
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [StringLength(Constants.CategoryIdLength)]
    public string Id { get; set; }
    
    [Required]
    [StringLength(50)]
    public string Name { get; set; }
    
    [Required]
    [StringLength(50)]
    public string Icon { get; set; }
    
    public virtual IEnumerable<Subcategory> Subcategories { get; set; }  = new List<Subcategory>();
    
    public Category(string name, string icon, string id)
    {
        Id = id;
        if (CheckNameCategory(name) && CheckIconCategory(icon))
        {
            Name = name;
            Icon = icon;
        }
        else
        {
            throw new ArgumentException("Invalid Category");
        }
    }

    public static bool CheckNameCategory(string name)
    {
        return name.Length > 0;
    }

    public static bool CheckIconCategory(string icon)
    {
        return icon.Length > 0;
    }

    public override string ToString()
    {
        return $" Name: {Name}, Icon: {Icon}";
    }
    
}

public readonly struct CategoryWithSubcategories(Category category, List<Subcategory> subcategories)
    {
        public Category Category { get; } = category;

        public List<Subcategory> Subcategories { get; } = subcategories;
    }
    
    