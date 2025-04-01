namespace Bidro.DTOs.CategoryDTOs;

public class GetDTOs
{
    public record GetCategoryDTO(
        Guid Id,
        string Name,
        string Icon,
        string Identifier)
    {
        public List<GetSubcategoryDTO> Subcategories { get; set; } = [];
    }

    public record GetSubcategoryDTO(
        Guid Id,
        string Name,
        string Icon,
        Guid CategoryId,
        string Identifier)
    {
    }
}