using System.Data;
using Bidro.DTOs.ListingDTOs;
using Bidro.EntityObjects;
using Dapper;

namespace Bidro.Services.Implementations;

public class ListingsService(IDbConnection db) : IListingsService
{
    public async Task<Guid> AddListing(PostDTOs.PostListingDTO listing)
    {
        using var transaction = db.BeginTransaction();
        try
        {
            const string addListingQuery =
                "INSERT INTO \"Listings\" (\"Title\", \"SubcategoryId\", \"UserId\") " +
                "VALUES (@Title, @SubcategoryId, @UserId) " +
                "RETURNING \"Id\"";
            var listingId = await db.ExecuteScalarAsync<Guid>(addListingQuery, listing.ListingBase, transaction);

            var location = listing.Location.ToListingLocation();
            location.ListingId = listingId;

            const string addLocationQuery =
                "INSERT INTO \"Locations\" (\"CountyId\", \"CityId\", \"Address\", \"PostalCode\", \"ListingId\") " +
                "VALUES (@CountyId, @CityId, @Address, @PostalCode, @ListingId)";
            await db.ExecuteAsync(addLocationQuery, location, transaction);

            var contact = listing.Contact.ToListingContact();
            contact.ListingId = listingId;

            const string addContactQuery =
                "INSERT INTO \"Contacts\" (\"Name\", \"Email\", \"Phone\", \"ListingId\") " +
                "VALUES (@Name, @Email, @Phone, @ListingId)";
            await db.ExecuteAsync(addContactQuery, contact, transaction);

            foreach (var formAnswer in listing.FormAnswers.Select(answer =>
                         new FormAnswer(answer.Value, answer.FormQuestionId, listingId)))
            {
                const string addFormAnswerQuery =
                    "INSERT INTO \"FormAnswers\" (\"Answer\", \"FormQuestionId\", \"ListingId\") " +
                    "VALUES (@Value, @FormQuestionId, @ListingId)";
                await db.ExecuteAsync(addFormAnswerQuery, formAnswer, transaction);
            }

            transaction.Commit();
            return listingId;
        }
        catch
        {
            transaction.Rollback();
            throw;
        }
    }

    public async Task<GetDTOs.GetListingDTO> GetListingById(Guid listingId)
    {
        var sqlListing = @"
            SELECT l.*, loc.*, c.* 
            FROM ""Listings"" l
            JOIN ""Locations"" loc ON l.""Id"" = loc.""ListingId""
            JOIN ""Contacts"" c ON l.""Id"" = c.""ListingId""
            WHERE l.""Id"" = @ListingId";

        var sqlFormAnswers = @"
            SELECT fa.*, fq.*
            FROM ""FormAnswers"" fa
            JOIN ""FormQuestions"" fq ON fa.""FormQuestionId"" = fq.""Id""
            WHERE fa.""ListingId"" = @ListingId";

        var listing = await db.QueryAsync<Listing, ListingLocation, ListingContact, Listing>(
            sqlListing,
            (l, loc, c) =>
            {
                l.Location = loc;
                l.Contact = c;
                return l;
            },
            new { ListingId = listingId },
            splitOn: "CountyId,Name"
        );

        var formAnswers = await db.QueryAsync<FormAnswer, FormQuestion, FormAnswer>(
            sqlFormAnswers,
            (fa, fq) =>
            {
                fa.Question = fq;
                return fa;
            },
            new { ListingId = listingId },
            splitOn: "Id"
        );

        var listingResult = listing.FirstOrDefault();
        if (listingResult == null) throw new KeyNotFoundException($"Listing with ID {listingId} not found.");
        listingResult.FormAnswers = formAnswers.ToList();
        return GetDTOs.GetListingDTO.FromListing(listingResult);
    }


    public async Task<bool> DeleteListing(Guid listingId)
    {
        // Should be deleted in reverse order of creation
        const string sqlDeleteFormAnswers = "DELETE FROM \"FormAnswers\" WHERE \"ListingId\" = @ListingId";
        const string sqlDeleteLocation = "DELETE FROM \"Locations\" WHERE \"ListingId\" = @ListingId";
        const string sqlDeleteContact = "DELETE FROM \"Contacts\" WHERE \"ListingId\" = @ListingId";
        const string sqlDeleteListing = "DELETE FROM \"Listings\" WHERE \"Id\" = @ListingId";

        using var transaction = db.BeginTransaction();
        try
        {
            await db.ExecuteAsync(sqlDeleteFormAnswers, new { ListingId = listingId }, transaction);
            await db.ExecuteAsync(sqlDeleteLocation, new { ListingId = listingId }, transaction);
            await db.ExecuteAsync(sqlDeleteContact, new { ListingId = listingId }, transaction);
            var rowsAffected = await db.ExecuteAsync(sqlDeleteListing, new { ListingId = listingId }, transaction);
            transaction.Commit();
            return rowsAffected > 0;
        }
        catch
        {
            transaction.Rollback();
            throw;
            throw new ArgumentNullException(nameof(sqlDeleteContact));
        }
    }
}