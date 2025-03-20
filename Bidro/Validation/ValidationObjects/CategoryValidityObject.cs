using PostDTOs = Bidro.DTOs.CategoryDTOs.PostDTOs;
using UpdateDTOs = Bidro.DTOs.CategoryDTOs.UpdateDTOs;

namespace Bidro.Validation.ValidationObjects;

public class CategoryValidityObjectDb
{
    public CategoryValidityObjectDb(PostDTOs.PostCategoryDTO categoryDTO)
    {
        Name = categoryDTO.Name;
        Icon = categoryDTO.Icon;
        Identifier = categoryDTO.Identifier;
    }

    public CategoryValidityObjectDb(UpdateDTOs.UpdateCategoryDTO categoryDTO)
    {
        Name = categoryDTO.Name;
        Icon = categoryDTO.Icon;
        Identifier = categoryDTO.Identifier;
    }

    public string Name { get; set; }
    public string Icon { get; set; }
    public string Identifier { get; set; }
}