using Bidro.DTOs.CategoryDTOs;
using Bidro.Services;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Bidro.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoriesController(ICategoriesService categoriesService) : ControllerBase
{
    [HttpPost("addCategory")]
    [SwaggerOperation(Summary = "Add a new category")]
    public async Task<IActionResult> AddCategory(PostDTOs.PostCategoryDTO categoryDTO)
    {
        var category = categoryDTO.ToCategory();
        return await categoriesService.AddCategory(category);
    }

    [HttpPost("addSubcategory")]
    [SwaggerOperation(Summary = "Add a new subcategory")]
    public async Task<IActionResult> AddSubcategory(PostDTOs.PostSubcategoryDTO subcategoryDTO)
    {
        var subcategory = subcategoryDTO.ToSubcategory();
        return await categoriesService.AddSubcategory(subcategory);
    }

    [HttpGet("getAllCategories")]
    [SwaggerOperation(Summary = "Get all categories")]
    public async Task<IActionResult> GetAllCategories()
    {
        return await categoriesService.GetAllCategories();
    }

    [HttpPut("updateCategory")]
    [SwaggerOperation(Summary = "Update a category")]
    public async Task<IActionResult> UpdateCategory(UpdateDTOs.UpdateCategoryDTO categoryDTO)
    {
        return await categoriesService.UpdateCategory(categoryDTO);
    }

    [HttpPut("updateSubcategory")]
    [SwaggerOperation(Summary = "Update a subcategory")]
    public async Task<IActionResult> UpdateSubcategory(UpdateDTOs.UpdateSubcategoryDTO subcategoryDTO)
    {
        return await categoriesService.UpdateSubcategory(subcategoryDTO);
    }
}