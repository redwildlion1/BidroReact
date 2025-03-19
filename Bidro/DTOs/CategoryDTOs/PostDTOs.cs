using Bidro.EntityObjects;

namespace Bidro.DTOs.CategoryDTOs;

public class PostDTOs
{
    public record PostCategoryDTO(
        string Name,
        string Icon,
        string Identifier)
    {
        public Category ToCategory()
        {
            return new Category(Name, Icon, Identifier);
        }
    }

    public record PostSubcategoryDTO(
        string Name,
        string Icon,
        Guid CategoryId,
        string Identifier)
    {
        public Subcategory ToSubcategory()
        {
            return new Subcategory(CategoryId, Name, Icon, Identifier);
        }
    }
}