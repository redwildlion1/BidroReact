using Bidro.Config;
using Bidro.DTOs.CategoryDTOs;
using Bidro.EntityObjects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Bidro.Services.Implementations;

public class CategoriesService(EntityDbContext db) : ICategoriesService
{
    public async Task<IActionResult> AddCategory(Category category)
    {
        await db.Categories.AddAsync(category);
        await db.SaveChangesAsync();
        return new OkResult();
    }

    public async Task<IActionResult> AddSubcategory(Subcategory subcategory)
    {
        await db.Subcategories.AddAsync(subcategory);
        await db.SaveChangesAsync();
        return new OkResult();
    }

    public async Task<IActionResult> GetAllCategories()
    {
        var categories = await db.Categories
            .Include(c => c.Subcategories)
            .ToListAsync();
        if (categories.Count == 0) return new NotFoundResult();
        return new OkObjectResult(categories);
    }

    public async Task<IActionResult> UpdateCategory(UpdateDTOs.UpdateCategoryDTO category)
    {
        var categoryToUpdate = await db.Categories.FindAsync(category.Id);
        if (categoryToUpdate == null) return new NotFoundResult();
        categoryToUpdate.Name = category.Name;
        categoryToUpdate.Icon = category.Icon;
        categoryToUpdate.Identifier = category.Identifier;
        db.Categories.Update(categoryToUpdate);
        await db.SaveChangesAsync();
        return new OkResult();
    }

    public async Task<IActionResult> UpdateSubcategory(UpdateDTOs.UpdateSubcategoryDTO subcategory)
    {
        var subcategoryToUpdate = await db.Subcategories.FindAsync(subcategory.Id);
        if (subcategoryToUpdate == null) return new NotFoundResult();
        subcategoryToUpdate.Name = subcategory.Name;
        subcategoryToUpdate.Icon = subcategory.Icon;
        subcategoryToUpdate.ParentCategoryId = subcategory.ParentCategoryId;
        subcategoryToUpdate.Identifier = subcategory.Identifier;
        db.Subcategories.Update(subcategoryToUpdate);
        await db.SaveChangesAsync();
        return new OkResult();
    }
}