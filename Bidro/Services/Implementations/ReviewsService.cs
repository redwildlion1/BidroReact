using Bidro.Config;
using Bidro.EntityObjects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Bidro.Services.Implementations;

public class ReviewsService(EntityDbContext db) : IReviewsService
{
    public async Task<IActionResult> GetReviewById(Guid reviewId)
    {
        var review = await db.Reviews.FirstOrDefaultAsync(r => r.Id == reviewId);
        if (review == null) return new NotFoundResult();
        return new OkObjectResult(review);
    }

    public async Task<IActionResult> GetReviewsByFirmId(Guid firmId)
    {
        var reviews = await db.Reviews.Where(r => r.FirmId == firmId).ToListAsync();
        if (reviews == null!) return new NotFoundResult();
        return new OkObjectResult(reviews);
    }

    public async Task<IActionResult> CreateReview(Review review)
    {
        await db.Reviews.AddAsync(review);
        await db.SaveChangesAsync();
        return new OkResult();
    }

    public async Task<IActionResult> DeleteReview(Guid reviewId)
    {
        var review = await db.Reviews.FirstOrDefaultAsync(r => r.Id == reviewId);
        if (review != null) db.Reviews.Remove(review);
        await db.SaveChangesAsync();
        return new OkResult();
    }
}