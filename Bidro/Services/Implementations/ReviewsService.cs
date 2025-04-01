using System.Data;
using Bidro.DTOs.ReviewDTOs;
using Bidro.EntityObjects;
using Dapper;

namespace Bidro.Services.Implementations;

public class ReviewsService(IDbConnection db) : IReviewsService
{
    public async Task<GetReviewDTO> GetReviewById(Guid reviewId)
    {
        const string sql = "SELECT * FROM \"Reviews\" WHERE \"Id\" = @Id";
        var review = await db.QuerySingleOrDefaultAsync<Review>(sql, new { Id = reviewId });
        if (review == null) throw new KeyNotFoundException($"Review with ID {reviewId} not found.");
        return GetReviewDTO.FromReview(review);
    }

    public async Task<IEnumerable<GetReviewDTO>> GetReviewsByFirmId(Guid firmId)
    {
        const string sql = "SELECT * FROM \"Reviews\" WHERE \"FirmId\" = @FirmId";
        var reviews = await db.QueryAsync<Review>(sql, new { FirmId = firmId });
        return reviews.Select(GetReviewDTO.FromReview).ToList();
    }

    public async Task<bool> CreateReview(PostReviewDTO review)
    {
        const string sql =
            "INSERT INTO \"Reviews\" (\"FirmId\", \"UserId\", \"Rating\", \"Content\", \"Date\") VALUES (@FirmId, @UserId, @Rating, @ReviewText) RETURNING \"Id\"";
        var id = await db.ExecuteScalarAsync<Guid>(sql, review);
        if (id == Guid.Empty) throw new Exception("Failed to create review");
        return true;
    }

    public async Task<bool> DeleteReview(Guid reviewId)
    {
        const string sql = "DELETE FROM \"Reviews\" WHERE \"Id\" = @Id";
        var rowsAffected = await db.ExecuteAsync(sql, new { Id = reviewId });
        if (rowsAffected == 0) throw new KeyNotFoundException($"Review with ID {reviewId} not found.");
        return true;
    }
}