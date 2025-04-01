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
        var result = await categoriesService.AddCategory(categoryDTO);
        return Ok(result);
    }

    [HttpPost("addSubcategory")]
    [SwaggerOperation(Summary = "Add a new subcategory")]
    public async Task<IActionResult> AddSubcategory(PostDTOs.PostSubcategoryDTO subcategoryDTO)
    {
        var result = await categoriesService.AddSubcategory(subcategoryDTO);
        return Ok(result);
    }

    [HttpGet("getAllCategories")]
    [SwaggerOperation(Summary = "Get all categories")]
    public async Task<IActionResult> GetAllCategories()
    {
        var result = await categoriesService.GetAllCategories();
        return Ok(result);
    }

    [HttpPut("updateCategory")]
    [SwaggerOperation(Summary = "Update a category")]
    public async Task<IActionResult> UpdateCategory(UpdateDTOs.UpdateCategoryDTO categoryDTO)
    {
        var result = await categoriesService.UpdateCategory(categoryDTO);
        return Ok(result);
    }

    [HttpPut("updateSubcategory")]
    [SwaggerOperation(Summary = "Update a subcategory")]
    public async Task<IActionResult> UpdateSubcategory(UpdateDTOs.UpdateSubcategoryDTO subcategoryDTO)
    {
        var result = await categoriesService.UpdateSubcategory(subcategoryDTO);
        return Ok(result);
    }
}