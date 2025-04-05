using Bidro.Config;
using Bidro.DTOs.CategoryDTOs;
using Dapper;

namespace Bidro.Services.Implementations;

public class CategoriesService(
    PgConnectionPool pgConnectionPool) : ICategoriesService
{
    public async Task<GetCategoryDTO> AddCategory(PostCategoryDTO postCategory)
    {
        using var db = await pgConnectionPool.RentAsync();
        const string sql =
            "INSERT INTO \"Categories\" (\"Name\", \"Icon\", \"Identifier\") VALUES (@Name, @Icon, @Identifier) RETURNING \"Id\"";
        var id = await db.ExecuteScalarAsync<Guid>(sql, postCategory);
        return new GetCategoryDTO(
            id,
            postCategory.Name,
            postCategory.Icon,
            postCategory.Identifier
        );
    }

    public async Task<GetSubcategoryDTO> AddSubcategory(PostSubcategoryDTO postSubcategory)
    {
        using var db = await pgConnectionPool.RentAsync();
        const string sql =
            "INSERT INTO \"Subcategories\" (\"Name\", \"Icon\", \"ParentCategoryId\", \"Identifier\") VALUES (@Name, @Icon, @ParentCategoryId, @Identifier) RETURNING \"Id\"";
        var id = await db.ExecuteScalarAsync<Guid>(sql, postSubcategory);
        return new GetSubcategoryDTO(
            id,
            postSubcategory.Name,
            postSubcategory.Icon,
            postSubcategory.ParentCategoryId,
            postSubcategory.Identifier
        );
    }

    public async Task<IEnumerable<GetCategoryDTO>> GetAllCategories()
    {
        using var db = await pgConnectionPool.RentAsync();
        const string sql = "SELECT * FROM \"Categories\"";
        var categories = await db.QueryAsync<GetCategoryDTO>(sql);
        return categories.ToList();
    }

    public async Task<GetCategoryDTO> UpdateCategory(UpdateCategoryDTO category)
    {
        using var db = await pgConnectionPool.RentAsync();
        const string sql =
            "UPDATE \"Categories\" SET \"Name\" = @Name, \"Icon\" = @Icon, \"Identifier\" = @Identifier WHERE \"Id\" = @Id";
        return await db.ExecuteAsync(sql, category) > 0
            ? new GetCategoryDTO(
                category.Id,
                category.Name,
                category.Icon,
                category.Identifier
            )
            : throw new KeyNotFoundException($"Category with ID {category.Id} not found.");
    }

    public async Task<GetSubcategoryDTO> UpdateSubcategory(UpdateSubcategoryDTO subcategory)
    {
        using var db = await pgConnectionPool.RentAsync();
        const string sql =
            "UPDATE \"Subcategories\" SET \"Name\" = @Name, \"Icon\" = @Icon, \"ParentCategoryId\" = @ParentCategoryId, \"Identifier\" = @Identifier WHERE \"Id\" = @Id";
        return await db.ExecuteAsync(sql, subcategory) > 0
            ? new GetSubcategoryDTO(
                subcategory.Id,
                subcategory.Name,
                subcategory.Icon,
                subcategory.ParentCategoryId,
                subcategory.Identifier
            )
            : throw new KeyNotFoundException($"Subcategory with ID {subcategory.Id} not found.");
    }
}