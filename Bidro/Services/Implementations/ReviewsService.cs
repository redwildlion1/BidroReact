using Bidro.Config;
using Bidro.DTOs.ReviewDTOs;
using Bidro.EntityObjects;
using Dapper;

namespace Bidro.Services.Implementations;

public class ReviewsService(PgConnectionPool pgConnectionPool) : IReviewsService
{
    public async Task<GetReviewDTO> GetReviewById(Guid reviewId)
    {
        const string sql = "SELECT * FROM \"Reviews\" WHERE \"Id\" = @Id";

        using var db = await pgConnectionPool.RentAsync();
        var review = await db.QuerySingleOrDefaultAsync<Review>(sql, new { Id = reviewId });
        if (review == null) throw new KeyNotFoundException($"Review with ID {reviewId} not found.");
        return GetReviewDTO.FromReview(review);
    }

    public async Task<IEnumerable<GetReviewDTO>> GetReviewsByFirmId(Guid firmId)
    {
        const string sql = "SELECT * FROM \"Reviews\" WHERE \"FirmId\" = @FirmId";

        using var db = await pgConnectionPool.RentAsync();
        var reviews = await db.QueryAsync<Review>(sql, new { FirmId = firmId });
        return reviews.Select(GetReviewDTO.FromReview).ToList();
    }

    public async Task<bool> CreateReview(PostReviewDTO review)
    {
        using var db = await pgConnectionPool.RentAsync();
        using var transaction = db.BeginTransaction();
        Guid id;
        try
        {
            const string sql =
                "INSERT INTO \"Reviews\" (\"FirmId\", \"UserId\", \"Rating\", \"ReviewText\", \"Date\") VALUES (@FirmId, @UserId, @Rating, @ReviewText, @Date) RETURNING \"Id\"";
            id = await db.ExecuteScalarAsync<Guid>(sql, review, transaction);

            // Add to reviewCount and rating in Firms table
            const string updateFirmSql =
                "UPDATE \"Firms\" SET \"ReviewCount\" = \"ReviewCount\" + 1, \"Rating\" = (SELECT AVG(\"Rating\") FROM \"Reviews\" WHERE \"FirmId\" = @FirmId) WHERE \"Id\" = @FirmId";
            await db.ExecuteAsync(updateFirmSql, new { review.FirmId }, transaction);

            transaction.Commit();
        }
        catch
        {
            transaction.Rollback();
            throw;
        }

        if (id == Guid.Empty) throw new Exception("Failed to create review");
        return true;
    }

    public async Task<bool> DeleteReview(Guid reviewId)
    {
        const string sql = "DELETE FROM \"Reviews\" WHERE \"Id\" = @Id";

        using var db = await pgConnectionPool.RentAsync();
        var rowsAffected = await db.ExecuteAsync(sql, new { Id = reviewId });
        if (rowsAffected == 0) throw new KeyNotFoundException($"Review with ID {reviewId} not found.");
        return true;
    }
}