using Bidro.Config;
using Bidro.DTOs.FirmDTOs;
using Bidro.EntityObjects;
using Dapper;

namespace Bidro.Services.Implementations;

public class FirmsService(PgConnectionPool pgConnectionPool) : IFirmsService
{
    public async Task<GetFirmDTO> GetFirmById(Guid firmId)
    {
        // Uses DapperAOT for better performance
        const string sql =
            "SELECT \"Firms\".*, \"FirmLocations\".*, \"FirmContacts\".* FROM \"Firms\" " +
            "LEFT JOIN \"FirmLocations\" ON \"Firms\".\"LocationId\" = \"FirmLocations\".\"Id\" " +
            "LEFT JOIN \"FirmContacts\" ON \"Firms\".\"ContactId\" = \"FirmContacts\".\"Id\" " +
            "WHERE \"Firms\".\"Id\" = @Id";

        using var db = await pgConnectionPool.RentAsync();
        var firm = await db.QuerySingleOrDefaultAsync<Firm>(sql, new { Id = firmId });
        if (firm == null) throw new NullReferenceException("Firm not found");
        return GetFirmDTO.FromFirm(firm);
    }

    public async Task<IEnumerable<GetFirmDTO>> GetFirmsInCategory(Guid categoryId)
    {
        const string sql =
            "SELECT \"Firms\".*, \"FirmLocations\".*, \"FirmContacts\".* FROM \"Firms\" " +
            "LEFT JOIN \"FirmLocations\" ON \"Firms\".\"LocationId\" = \"FirmLocations\".\"Id\" " +
            "LEFT JOIN \"FirmContacts\" ON \"Firms\".\"ContactId\" = \"FirmContacts\".\"Id\" " +
            "INNER JOIN \"FirmSubcategories\" ON \"Firms\".\"Id\" = \"FirmSubcategories\".\"FirmId\" " +
            "INNER JOIN \"Subcategories\" ON \"FirmSubcategories\".\"SubcategoryId\" = \"Subcategories\".\"Id\" " +
            "WHERE \"Subcategories\".\"ParentCategoryId\" = @CategoryId";


        using var db = await pgConnectionPool.RentAsync();
        var firms = await db.QueryAsync<Firm>(sql, new { CategoryId = categoryId });
        var firmList = firms.ToList();
        return firmList.Select(GetFirmDTO.FromFirm).ToList();
    }

    public async Task<IEnumerable<GetFirmDTO>> GetFirmsInSubcategory(Guid subcategoryId)
    {
        const string sql =
            "SELECT \"Firms\".*, \"FirmLocations\".*, \"FirmContacts\".* FROM \"Firms\" " +
            "LEFT JOIN \"FirmLocations\" ON \"Firms\".\"LocationId\" = \"FirmLocations\".\"Id\" " +
            "LEFT JOIN \"FirmContacts\" ON \"Firms\".\"ContactId\" = \"FirmContacts\".\"Id\" " +
            "INNER JOIN \"FirmSubcategories\" ON \"Firms\".\"Id\" = \"FirmSubcategories\".\"FirmId\" " +
            "WHERE \"FirmSubcategories\".\"SubcategoryId\" = @SubcategoryId";

        using var db = await pgConnectionPool.RentAsync();
        var firms = await db.QueryAsync<Firm>(sql, new { SubcategoryId = subcategoryId });
        var firmList = firms.ToList();
        return firmList.Select(GetFirmDTO.FromFirm).ToList();
    }

    // TODO: Refactor this into smaller methods
    public async Task<Guid> PostFirm(PostFirmDTO postFirmDTO)
    {
        using var db = await pgConnectionPool.RentAsync();
        using var transaction = db.BeginTransaction();
        try
        {
            const string postFirmLocationSql =
                "INSERT INTO \"FirmLocations\" (\"CityId\", \"CountyId\", \"Address\", \"PostalCode\", \"Latitude\", \"Longitude\") " +
                "VALUES (@CityId, @CountyId, @Address, @PostalCode, @Latitude, @Longitude) RETURNING \"Id\"";
            var locationId = await db.ExecuteScalarAsync<Guid>(postFirmLocationSql, postFirmDTO.Location, transaction);

            const string postFirmContactSql =
                "INSERT INTO \"FirmContacts\" (\"Email\", \"Phone\", \"Fax\") " +
                "VALUES (@Email, @Phone, @Fax) RETURNING \"Id\"";
            var contactId = await db.ExecuteScalarAsync<Guid>(postFirmContactSql, postFirmDTO.Contact, transaction);

            const string postFirmSql =
                "INSERT INTO \"Firms\" (\"Name\", \"Description\", \"Logo\", \"Website\", \"LocationId\", \"ContactId\") " +
                "VALUES (@Name, @Description, @Logo, @Website, @LocationId, @ContactId) RETURNING \"Id\"";
            var firmId = await db.ExecuteScalarAsync<Guid>(postFirmSql, new
            {
                postFirmDTO.Base.Name,
                postFirmDTO.Base.Description,
                postFirmDTO.Base.Logo,
                postFirmDTO.Base.Website,
                LocationId = locationId,
                ContactId = contactId
            }, transaction);

            const string postFirmCategorySql =
                "INSERT INTO \"FirmSubcategories\" (\"FirmId\", \"SubcategoryId\") " +
                "VALUES (@FirmId, @CategoryId)";
            foreach (var categoryId in postFirmDTO.Base.SubcategoryIds)
                await db.ExecuteAsync(postFirmCategorySql, new { FirmId = firmId, CategoryId = categoryId },
                    transaction);

            transaction.Commit();
            return firmId;
        }
        catch
        {
            transaction.Rollback();
            throw;
        }
    }

    public async Task<bool> UpdateFirmName(UpdateFirmNameDTO updateFirmNameDTO)
    {
        const string sql =
            "UPDATE \"Firms\" SET \"Name\" = @Name WHERE \"Id\" = @Id";

        using var db = await pgConnectionPool.RentAsync();
        var rowsAffected = await db.ExecuteAsync(sql, updateFirmNameDTO);
        return rowsAffected > 0;
    }

    public async Task<bool> UpdateFirmDescription(UpdateFirmDescriptionDTO updateFirmDescriptionDTO)
    {
        const string sql =
            "UPDATE \"Firms\" SET \"Description\" = @Description WHERE \"Id\" = @Id";

        using var db = await pgConnectionPool.RentAsync();
        var rowsAffected = await db.ExecuteAsync(sql, updateFirmDescriptionDTO);
        return rowsAffected > 0;
    }

    public async Task<bool> UpdateFirmLogo(UpdateFirmLogoDTO updateFirmLogoDTO)
    {
        const string sql =
            "UPDATE \"Firms\" SET \"Logo\" = @Logo WHERE \"Id\" = @Id";

        using var db = await pgConnectionPool.RentAsync();
        var rowsAffected = await db.ExecuteAsync(sql, updateFirmLogoDTO);
        return rowsAffected > 0;
    }

    public async Task<bool> UpdateFirmWebsite(UpdateFirmWebsiteDTO updateFirmWebsiteDTO)
    {
        const string sql =
            "UPDATE \"Firms\" SET \"Website\" = @Website WHERE \"Id\" = @Id";

        using var db = await pgConnectionPool.RentAsync();
        var rowsAffected = await db.ExecuteAsync(sql, updateFirmWebsiteDTO);
        return rowsAffected > 0;
    }

    public async Task<bool> UpdateFirmLocation(UpdateFirmLocationDTO updateFirmLocationDTO)
    {
        const string sql =
            "UPDATE \"FirmLocations\" SET \"CityId\" = @CityId, \"CountyId\" = @CountyId, \"Address\" = @Address, \"PostalCode\" = @PostalCode, \"Latitude\" = @Latitude, \"Longitude\" = @Longitude WHERE \"Id\" = @Id";

        using var db = await pgConnectionPool.RentAsync();
        var rowsAffected = await db.ExecuteAsync(sql, updateFirmLocationDTO);
        return rowsAffected > 0;
    }

    public async Task<bool> UpdateFirmContact(UpdateFirmContactDTO updateFirmContactDTO)
    {
        const string sql =
            "UPDATE \"FirmContacts\" SET \"Email\" = @Email, \"Phone\" = @Phone, \"Fax\" = @Fax WHERE \"Id\" = @Id";

        using var db = await pgConnectionPool.RentAsync();
        var rowsAffected = await db.ExecuteAsync(sql, updateFirmContactDTO);
        return rowsAffected > 0;
    }

    public async Task<bool> SuspendFirm(Guid firmId)
    {
        const string sql =
            "UPDATE \"Firms\" SET \"IsSuspended\" = true WHERE \"Id\" = @Id";

        using var db = await pgConnectionPool.RentAsync();
        var rowsAffected = await db.ExecuteAsync(sql, new { Id = firmId });
        return rowsAffected > 0;
    }

    public async Task<bool> UnsuspendFirm(Guid firmId)
    {
        const string sql =
            "UPDATE \"Firms\" SET \"IsSuspended\" = false WHERE \"Id\" = @Id";

        using var db = await pgConnectionPool.RentAsync();
        var rowsAffected = await db.ExecuteAsync(sql, new { Id = firmId });
        return rowsAffected > 0;
    }

    public async Task<bool> VerifyFirm(Guid firmId)
    {
        const string sql =
            "UPDATE \"Firms\" SET \"IsVerified\" = true WHERE \"Id\" = @Id";

        using var db = await pgConnectionPool.RentAsync();
        var rowsAffected = await db.ExecuteAsync(sql, new { Id = firmId });
        return rowsAffected > 0;
    }
}