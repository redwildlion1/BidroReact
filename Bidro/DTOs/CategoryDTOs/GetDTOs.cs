using Bidro.EntityObjects;

namespace Bidro.DTOs.CategoryDTOs;

public class GetDTOs
{
    public record GetCategoryDTO(
        string Name,
        string Icon,
        string Identifier,
        List<GetSubcategoryDTO> Subcategories)
    {
        public GetCategoryDTO FromCategory(Category category)
        {
            return new GetCategoryDTO(category.Name, category.Icon, category.Identifier,
                (category.Subcategories ?? []).Select(GetSubcategoryDTO.FromSubcategory).ToList());
        }
    }

    public record GetSubcategoryDTO(
        string Name,
        string Icon,
        Guid CategoryId,
        string Identifier)
    {
        public static GetSubcategoryDTO FromSubcategory(Subcategory subcategory)
        {
            return new GetSubcategoryDTO(subcategory.Name, subcategory.Icon, subcategory.ParentCategoryId,
                subcategory.Identifier);
        }
    }
}