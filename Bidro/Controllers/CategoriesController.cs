using Bidro.Config;
using Bidro.DTOs.CategoryDTOs;
using Bidro.Services;
using Bidro.Validation.FluentValidators;
using Bidro.Validation.ValidationObjects;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Bidro.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoriesController(
    ICategoriesService categoriesService,
    PgConnectionPool connection
) : ControllerBase
{
    [HttpPost("addCategory")]
    [SwaggerOperation(Summary = "Add a new category")]
    public async Task<IResult> AddCategory(PostCategoryDTO categoryDTO)
    {
        var categoryValidator = new CategoryValidator(connection);
        var validationResult = await categoryValidator.ValidateAsync(new CategoryValidityObject(categoryDTO));
        if (!validationResult.IsValid) return Results.BadRequest(validationResult.Errors);

        var result = await categoriesService.AddCategory(categoryDTO);
        return Results.Ok(result);
    }

    [HttpPost("addSubcategory")]
    [SwaggerOperation(Summary = "Add a new subcategory")]
    public async Task<IResult> AddSubcategory(PostSubcategoryDTO subcategoryDTO)
    {
        var subcategoryValidator = new SubcategoryValidator(connection);
        var validationResult = await subcategoryValidator.ValidateAsync(new SubcategoryValidityObject(subcategoryDTO));
        if (!validationResult.IsValid) return Results.BadRequest(validationResult.Errors);

        var result = await categoriesService.AddSubcategory(subcategoryDTO);
        return Results.Ok(result);
    }

    [HttpGet("getAllCategories")]
    [SwaggerOperation(Summary = "Get all categories")]
    public async Task<IResult> GetAllCategories()
    {
        var result = await categoriesService.GetAllCategories();
        return Results.Ok(result);
    }

    [HttpPut("updateCategory")]
    [SwaggerOperation(Summary = "Update a category")]
    public async Task<IResult> UpdateCategory(UpdateCategoryDTO categoryDTO)
    {
        var updateCategoryValidator = new UpdateCategoryValidator(connection);
        var validationResult = await updateCategoryValidator.ValidateAsync(categoryDTO);
        if (!validationResult.IsValid) return Results.BadRequest(validationResult.Errors);

        var result = await categoriesService.UpdateCategory(categoryDTO);
        return Results.Ok(result);
    }

    [HttpPut("updateSubcategory")]
    [SwaggerOperation(Summary = "Update a subcategory")]
    public async Task<IResult> UpdateSubcategory(UpdateSubcategoryDTO subcategoryDTO)
    {
        var subcategoryValidator = new UpdateSubcategoryValidator(connection);
        var validationResult = await subcategoryValidator.ValidateAsync(subcategoryDTO);
        if (!validationResult.IsValid) return Results.BadRequest(validationResult.Errors);

        var result = await categoriesService.UpdateSubcategory(subcategoryDTO);
        return Results.Ok(result);
    }
}