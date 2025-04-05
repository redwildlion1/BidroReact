using Bidro.DTOs.CategoryDTOs;

namespace Bidro.Validation.ValidationObjects;

public class CategoryValidityObjectDb
{
    public CategoryValidityObjectDb(PostCategoryDTO categoryDTO)
    {
        Name = categoryDTO.Name;
        Icon = categoryDTO.Icon;
        Identifier = categoryDTO.Identifier;
    }

    public CategoryValidityObjectDb(UpdateCategoryDTO categoryDTO)
    {
        Name = categoryDTO.Name;
        Icon = categoryDTO.Icon;
        Identifier = categoryDTO.Identifier;
    }

    public string Name { get; set; }
    public string Icon { get; set; }
    public string Identifier { get; set; }
}

public class CategoryValidityObject
{
    public CategoryValidityObject(PostCategoryDTO categoryDTO)
    {
        Name = categoryDTO.Name;
        Icon = categoryDTO.Icon;
        Identifier = categoryDTO.Identifier;
    }

    public CategoryValidityObject(UpdateCategoryDTO categoryDTO)
    {
        Name = categoryDTO.Name;
        Icon = categoryDTO.Icon;
        Identifier = categoryDTO.Identifier;
    }

    public string Name { get; }
    public string Icon { get; }
    public string Identifier { get; }
}