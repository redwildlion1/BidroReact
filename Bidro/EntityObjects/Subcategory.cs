using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Bidro.Config;

namespace Bidro.EntityObjects;

public record Subcategory
{
    public Subcategory(Guid parentCategoryId, string name, string icon, string identifier)
    {
        Identifier = identifier;
        if (CheckNameSubcategory(name) && CheckIconSubcategory(icon))
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

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    [Key]
    [StringLength(Constants.SubcategoryIdLength)]
    public string Identifier { get; set; }

    [Required] [StringLength(50)] public string Name { get; set; }

    [Required] [StringLength(50)] public string Icon { get; set; }

    [Required]
    [StringLength(Constants.CategoryIdLength)]
    [ForeignKey(nameof(Category))]
    public Guid ParentCategoryId { get; set; }

    [Required] public Category? ParentCategory { get; set; }

    public IEnumerable<FormQuestion>? FormQuestions { get; set; }

    public List<Firm>? Firms { get; set; }

    public List<Listing>? Listings { get; set; }


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