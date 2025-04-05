using Bidro.DTOs.CategoryDTOs;

namespace Bidro.Services;

public interface ICategoriesService
{
    public Task<GetCategoryDTO> AddCategory(PostCategoryDTO postCategory);
    public Task<GetSubcategoryDTO> AddSubcategory(PostSubcategoryDTO postSubcategory);

    public Task<IEnumerable<GetCategoryDTO>> GetAllCategories();
    public Task<GetCategoryDTO> UpdateCategory(UpdateCategoryDTO category);
    public Task<GetSubcategoryDTO> UpdateSubcategory(UpdateSubcategoryDTO subcategory);
}