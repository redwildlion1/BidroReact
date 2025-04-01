using Bidro.DTOs.CategoryDTOs;

namespace Bidro.Validation.ValidationObjects;

public class SubcategoryValidityObject
{
    public SubcategoryValidityObject(PostDTOs.PostSubcategoryDTO subcategoryDTO)
    {
        Name = subcategoryDTO.Name;
        Identifier = subcategoryDTO.Identifier;
        Icon = subcategoryDTO.Icon;
    }

    public SubcategoryValidityObject(UpdateDTOs.UpdateSubcategoryDTO subcategoryDTO)
    {
        Name = subcategoryDTO.Name;
        Identifier = subcategoryDTO.Identifier;
        Icon = subcategoryDTO.Icon;
    }

    public string Name { get; }
    public string Identifier { get; }
    public string Icon { get; }
}