using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Bidro.Config;

namespace Bidro.EntityObjects;

public record Category
{
    public Category(string name, string icon, string identifier)
    {
        Identifier = identifier;
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

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    [Key]
    [StringLength(Constants.CategoryIdLength)]
    public string Identifier { get; set; }

    [Required] [StringLength(50)] public string Name { get; set; }

    [Required] [StringLength(50)] public string Icon { get; set; }

    public List<Subcategory>? Subcategories { get; set; }

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