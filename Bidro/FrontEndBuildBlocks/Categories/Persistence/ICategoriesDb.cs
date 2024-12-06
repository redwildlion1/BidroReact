using Microsoft.AspNetCore.Mvc;

namespace Bidro.FrontEndBuildBlocks.Categories.Persistence;

public interface ICategoriesDb
{
    public Task<IActionResult> AddCategory(Category category);
    public Task<IActionResult> AddSubcategory(Subcategory subcategory);
}