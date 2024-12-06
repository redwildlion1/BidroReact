using Bidro.Config;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Bidro.FrontEndBuildBlocks.Categories.Persistence;

public class CategoriesDb(DbContextOptions<EntityDbContext> options) : ICategoriesDb
{

    public async Task<IActionResult> AddCategory(Category category)
    {
        await using var db = new EntityDbContext(options);
        await db.Categories.AddAsync(category);
        await db.SaveChangesAsync();
        return new OkResult();
    }

    public async Task<IActionResult> AddSubcategory(Subcategory subcategory)
    {
        await using var db = new EntityDbContext(options);
        await db.Subcategories.AddAsync(subcategory);
        await db.SaveChangesAsync();
        return new OkResult();
    }
}