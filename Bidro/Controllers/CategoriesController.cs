using Bidro.FrontEndBuildBlocks.Categories;
using Bidro.FrontEndBuildBlocks.Categories.Persistence;
using Microsoft.AspNetCore.Mvc;

namespace Bidro.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoriesController(ICategoriesDb categoriesDb) : ControllerBase
{
    [HttpPost("addCategory")]
    public async Task<IActionResult> AddCategory(Category category)
    {
        return await categoriesDb.AddCategory(category);
    }
    
}