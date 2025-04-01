using Bidro.DTOs.CategoryDTOs;

namespace Bidro.Services;

public interface ICategoriesService
{
    public Task<GetDTOs.GetCategoryDTO> AddCategory(PostDTOs.PostCategoryDTO postCategory);
    public Task<GetDTOs.GetSubcategoryDTO> AddSubcategory(PostDTOs.PostSubcategoryDTO postSubcategory);

    public Task<IEnumerable<GetDTOs.GetCategoryDTO>> GetAllCategories();
    public Task<GetDTOs.GetCategoryDTO> UpdateCategory(UpdateDTOs.UpdateCategoryDTO category);
    public Task<GetDTOs.GetSubcategoryDTO> UpdateSubcategory(UpdateDTOs.UpdateSubcategoryDTO subcategory);
}