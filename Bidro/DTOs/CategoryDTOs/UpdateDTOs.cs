namespace Bidro.DTOs.CategoryDTOs;

public record UpdateCategoryDTO(
    Guid Id,
    string Name,
    string Icon,
    string Identifier);

public record UpdateSubcategoryDTO(
    Guid Id,
    string Name,
    string Icon,
    Guid ParentCategoryId,
    string Identifier);