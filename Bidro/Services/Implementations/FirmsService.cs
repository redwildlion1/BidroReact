using System.Data;
using Bidro.DTOs.FirmDTOs;
using Bidro.EntityObjects;
using Dapper;

namespace Bidro.Services.Implementations;

public class FirmsService(IDbConnection db) : IFirmsService
{
    public async Task<GetDTOs.GetFirmDTO> GetFirmById(Guid firmId)
    {
        // Uses DapperAOT for better performance
        const string sql =
            "SELECT \"Firms\".*, \"Locations\".*, \"Contacts\".* FROM \"Firms\" " +
            "LEFT JOIN \"Locations\" ON \"Firms\".\"LocationId\" = \"Locations\".\"Id\" " +
            "LEFT JOIN \"Contacts\" ON \"Firms\".\"ContactId\" = \"Contacts\".\"Id\" " +
            "WHERE \"Firms\".\"Id\" = @Id";

        var firm = await db.QuerySingleOrDefaultAsync<Firm>(sql, new { Id = firmId });
        if (firm == null) throw new NullReferenceException("Firm not found");
        return GetDTOs.GetFirmDTO.FromFirm(firm);
    }

    public async Task<IEnumerable<GetDTOs.GetFirmDTO>> GetFirmsInCategory(Guid categoryId)
    {
        const string sql =
            "SELECT \"Firms\".*, \"Locations\".*, \"Contacts\".* FROM \"Firms\" " +
            "LEFT JOIN \"Locations\" ON \"Firms\".\"LocationId\" = \"Locations\".\"Id\" " +
            "LEFT JOIN \"Contacts\" ON \"Firms\".\"ContactId\" = \"Contacts\".\"Id\" " +
            "WHERE \"Firms\".\"CategoryId\" = @CategoryId";
        var firms = await db.QueryAsync<Firm>(sql, new { CategoryId = categoryId });
        var firmList = firms.ToList();
        return firmList.Select(GetDTOs.GetFirmDTO.FromFirm).ToList();
    }

    public async Task<IEnumerable<GetDTOs.GetFirmDTO>> GetFirmsInSubcategory(Guid subcategoryId)
    {
        const string sql =
            "SELECT \"Firms\".*, \"Locations\".*, \"Contacts\".* FROM \"Firms\" " +
            "LEFT JOIN \"Locations\" ON \"Firms\".\"LocationId\" = \"Locations\".\"Id\" " +
            "LEFT JOIN \"Contacts\" ON \"Firms\".\"ContactId\" = \"Contacts\".\"Id\" " +
            "WHERE \"Firms\".\"SubcategoryId\" = @SubcategoryId";
        var firms = await db.QueryAsync<Firm>(sql, new { SubcategoryId = subcategoryId });
        var firmList = firms.ToList();
        return firmList.Select(GetDTOs.GetFirmDTO.FromFirm).ToList();
    }

    // TODO: Refactor this into smaller methods
    public async Task<Guid> PostFirm(PostDTOs.PostFirmDTO postFirmDTO)
    {
        using var transaction = db.BeginTransaction();
        try
        {
            const string postFirmLocationSql =
                "INSERT INTO \"Locations\" (\"CityId\", \"CountyId\", \"Address\", \"PostalCode\", \"Latitude\", \"Longitude\") " +
                "VALUES (@CityId, @CountyId, @Address, @PostalCode, @Latitude, @Longitude) RETURNING \"Id\"";
            var locationId = await db.ExecuteScalarAsync<Guid>(postFirmLocationSql, postFirmDTO.Location, transaction);

            const string postFirmContactSql =
                "INSERT INTO \"Contacts\" (\"Email\", \"Phone\", \"Fax\") " +
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
                "INSERT INTO \"FirmCategories\" (\"FirmId\", \"CategoryId\") " +
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

    public async Task<bool> UpdateFirmName(UpdateDTOs.UpdateFirmNameDTO updateFirmNameDTO)
    {
        const string sql =
            "UPDATE \"Firms\" SET \"Name\" = @Name WHERE \"Id\" = @Id";
        var rowsAffected = await db.ExecuteAsync(sql, updateFirmNameDTO);
        return rowsAffected > 0;
    }

    public async Task<bool> UpdateFirmDescription(UpdateDTOs.UpdateFirmDescriptionDTO updateFirmDescriptionDTO)
    {
        const string sql =
            "UPDATE \"Firms\" SET \"Description\" = @Description WHERE \"Id\" = @Id";
        var rowsAffected = await db.ExecuteAsync(sql, updateFirmDescriptionDTO);
        return rowsAffected > 0;
    }

    public async Task<bool> UpdateFirmLogo(UpdateDTOs.UpdateFirmLogoDTO updateFirmLogoDTO)
    {
        const string sql =
            "UPDATE \"Firms\" SET \"Logo\" = @Logo WHERE \"Id\" = @Id";
        var rowsAffected = await db.ExecuteAsync(sql, updateFirmLogoDTO);
        return rowsAffected > 0;
    }

    public async Task<bool> UpdateFirmWebsite(UpdateDTOs.UpdateFirmWebsiteDTO updateFirmWebsiteDTO)
    {
        const string sql =
            "UPDATE \"Firms\" SET \"Website\" = @Website WHERE \"Id\" = @Id";
        var rowsAffected = await db.ExecuteAsync(sql, updateFirmWebsiteDTO);
        return rowsAffected > 0;
    }

    public async Task<bool> UpdateFirmLocation(UpdateDTOs.UpdateFirmLocationDTO updateFirmLocationDTO)
    {
        const string sql =
            "UPDATE \"Locations\" SET \"CityId\" = @CityId, \"CountyId\" = @CountyId, \"Address\" = @Address, \"PostalCode\" = @PostalCode, \"Latitude\" = @Latitude, \"Longitude\" = @Longitude WHERE \"Id\" = @Id";
        var rowsAffected = await db.ExecuteAsync(sql, updateFirmLocationDTO);
        return rowsAffected > 0;
    }

    public async Task<bool> UpdateFirmContact(UpdateDTOs.UpdateFirmContactDTO updateFirmContactDTO)
    {
        const string sql =
            "UPDATE \"Contacts\" SET \"Email\" = @Email, \"Phone\" = @Phone, \"Fax\" = @Fax WHERE \"Id\" = @Id";
        var rowsAffected = await db.ExecuteAsync(sql, updateFirmContactDTO);
        return rowsAffected > 0;
    }

    public async Task<bool> SuspendFirm(Guid firmId)
    {
        const string sql =
            "UPDATE \"Firms\" SET \"IsSuspended\" = true WHERE \"Id\" = @Id";
        var rowsAffected = await db.ExecuteAsync(sql, new { Id = firmId });
        return rowsAffected > 0;
    }

    public async Task<bool> UnsuspendFirm(Guid firmId)
    {
        const string sql =
            "UPDATE \"Firms\" SET \"IsSuspended\" = false WHERE \"Id\" = @Id";
        var rowsAffected = await db.ExecuteAsync(sql, new { Id = firmId });
        return rowsAffected > 0;
    }

    public async Task<bool> VerifyFirm(Guid firmId)
    {
        const string sql =
            "UPDATE \"Firms\" SET \"IsVerified\" = true WHERE \"Id\" = @Id";
        var rowsAffected = await db.ExecuteAsync(sql, new { Id = firmId });
        return rowsAffected > 0;
    }
}