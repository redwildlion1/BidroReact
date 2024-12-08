using Bidro.FrontEndBuildBlocks.Categories.DTOs;
using Bidro.FrontEndBuildBlocks.Categories.Persistence;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Bidro.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoriesController(ICategoriesDb categoriesDb) : ControllerBase
{
    [HttpPost("addCategory")]
    [SwaggerOperation (Summary = "Add a new category")]
    public async Task<IActionResult> AddCategory(AddCategoryDTO categoryDTO)
    {
        var category = categoryDTO.ToCategory();
        return await categoriesDb.AddCategory(category);
    }
    
}