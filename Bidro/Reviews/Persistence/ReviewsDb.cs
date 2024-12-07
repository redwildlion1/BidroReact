using Bidro.Config;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Bidro.Reviews.Persistence;

public class ReviewsDb(DbContextOptions<EntityDbContext> options) : IReviewsDb
{
    public async Task<IActionResult> GetReviewById(Guid reviewId)
    {
        await using var db = new EntityDbContext(options);
        var review = await db.Reviews.FirstOrDefaultAsync(r => r.Id == reviewId);
        if (review == null) return new NotFoundResult();
        return new OkObjectResult(review);
    }

    public async Task<IActionResult> GetReviewsByFirmId(Guid firmId)
    {
        await using var db = new EntityDbContext(options);
        List<Review> reviews = await db.Reviews.Where(r => r.FirmId == firmId).ToListAsync();
        if (reviews == null!) return new NotFoundResult();
        return new OkObjectResult(reviews);
    }

    public async Task<IActionResult> CreateReview(Review review)
    {
        await using var db = new EntityDbContext(options);
        await db.Reviews.AddAsync(review);
        await db.SaveChangesAsync();
        return new OkResult();
    }

    public async Task<IActionResult> DeleteReview(Guid reviewId)
    {
        await using var db = new EntityDbContext(options);
        var review = await db.Reviews.FirstOrDefaultAsync(r => r.Id == reviewId);
        if (review != null) db.Reviews.Remove(review);
        await db.SaveChangesAsync();
        return new OkResult();
    }
}