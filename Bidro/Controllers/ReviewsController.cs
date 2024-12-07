using Bidro.Reviews;
using Bidro.Reviews.Persistence;
using Microsoft.AspNetCore.Mvc;

namespace Bidro.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReviewsController(IReviewsDb reviewsDb) : ControllerBase
{
    [HttpGet("review/{reviewId}")]
    public async Task<IActionResult> GetReviewById(Guid reviewId)
    {
        return await reviewsDb.GetReviewById(reviewId);
    }

    [HttpGet("reviewsByFirmId/{firmId}")]
    public async Task<IActionResult> GetReviewsByFirmId(Guid firmId)
    {
        return await reviewsDb.GetReviewsByFirmId(firmId);
    }

    [HttpPost("createReview")]
    public async Task<IActionResult> CreateReview(Review review)
    {
        return await reviewsDb.CreateReview(review);
    }

    [HttpDelete("review/{reviewId}")]
    public async Task<IActionResult> DeleteReview(Guid reviewId)
    {
        return await reviewsDb.DeleteReview(reviewId);
    }
}
