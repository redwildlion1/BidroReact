using Bidro.DTOs.CategoryDTOs;

namespace Bidro.Validation.ValidationObjects;

public class SubcategoryValidityObjectDb
{
    public SubcategoryValidityObjectDb(PostDTOs.PostSubcategoryDTO subcategoryDTO)
    {
        Name = subcategoryDTO.Name;
        Identifier = subcategoryDTO.Identifier;
        Icon = subcategoryDTO.Icon;
    }

    public SubcategoryValidityObjectDb(UpdateDTOs.UpdateSubcategoryDTO subcategoryDTO)
    {
        Name = subcategoryDTO.Name;
        Identifier = subcategoryDTO.Identifier;
        Icon = subcategoryDTO.Icon;
    }

    public string Name { get; }
    public string Identifier { get; }
    public string Icon { get; }
}