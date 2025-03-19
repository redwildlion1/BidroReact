using Bidro.DTOs.CategoryDTOs;
using Bidro.EntityObjects;
using Microsoft.AspNetCore.Mvc;

namespace Bidro.Services;

public interface ICategoriesService
{
    public Task<IActionResult> AddCategory(Category category);
    public Task<IActionResult> AddSubcategory(Subcategory subcategory);

    public Task<IActionResult> GetAllCategories();
    public Task<IActionResult> UpdateCategory(UpdateDTOs.UpdateCategoryDTO category);
    public Task<IActionResult> UpdateSubcategory(UpdateDTOs.UpdateSubcategoryDTO subcategory);
}