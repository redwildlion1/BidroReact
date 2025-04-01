using System.Data;
using Bidro.DTOs.CategoryDTOs;
using Bidro.EntityObjects;
using Dapper;

namespace Bidro.Services.Implementations;

public class CategoriesService(IDbConnection db) : ICategoriesService
{
    public async Task<GetDTOs.GetCategoryDTO> AddCategory(PostDTOs.PostCategoryDTO postCategory)
    {
        const string sql =
            "INSERT INTO \"Categories\" (\"Name\", \"Icon\", \"Identifier\") VALUES (@Name, @Icon, @Identifier) RETURNING \"Id\"";
        var id = await db.ExecuteScalarAsync<Guid>(sql, postCategory);
        return new GetDTOs.GetCategoryDTO(
            id,
            postCategory.Name,
            postCategory.Icon,
            postCategory.Identifier
        );
    }

    public async Task<GetDTOs.GetSubcategoryDTO> AddSubcategory(PostDTOs.PostSubcategoryDTO postSubcategory)
    {
        const string sql =
            "INSERT INTO \"Subcategories\" (\"Name\", \"Icon\", \"ParentCategoryId\", \"Identifier\") VALUES (@Name, @Icon, @ParentCategoryId, @Identifier) RETURNING \"Id\"";
        var id = await db.ExecuteScalarAsync<Guid>(sql, postSubcategory);
        return new GetDTOs.GetSubcategoryDTO(
            id,
            postSubcategory.Name,
            postSubcategory.Icon,
            postSubcategory.CategoryId,
            postSubcategory.Identifier
        );
    }

    public async Task<IEnumerable<GetDTOs.GetCategoryDTO>> GetAllCategories()
    {
        const string sql = "SELECT * FROM \"Categories\"";
        var categories = await db.QueryAsync<Category>(sql);
        return categories.Select(c => new GetDTOs.GetCategoryDTO(
            c.Id,
            c.Name,
            c.Icon,
            c.Identifier
        )).ToList();
    }

    public async Task<GetDTOs.GetCategoryDTO> UpdateCategory(UpdateDTOs.UpdateCategoryDTO category)
    {
        const string sql =
            "UPDATE \"Categories\" SET \"Name\" = @Name, \"Icon\" = @Icon, \"Identifier\" = @Identifier WHERE \"Id\" = @Id";
        return await db.ExecuteAsync(sql, category) > 0
            ? new GetDTOs.GetCategoryDTO(
                category.Id,
                category.Name,
                category.Icon,
                category.Identifier
            )
            : throw new KeyNotFoundException($"Category with ID {category.Id} not found.");
    }

    public async Task<GetDTOs.GetSubcategoryDTO> UpdateSubcategory(UpdateDTOs.UpdateSubcategoryDTO subcategory)
    {
        const string sql =
            "UPDATE \"Subcategories\" SET \"Name\" = @Name, \"Icon\" = @Icon, \"ParentCategoryId\" = @ParentCategoryId, \"Identifier\" = @Identifier WHERE \"Id\" = @Id";
        return await db.ExecuteAsync(sql, subcategory) > 0
            ? new GetDTOs.GetSubcategoryDTO(
                subcategory.Id,
                subcategory.Name,
                subcategory.Icon,
                subcategory.ParentCategoryId,
                subcategory.Identifier
            )
            : throw new KeyNotFoundException($"Subcategory with ID {subcategory.Id} not found.");
    }
}