using Bidro.Reviews;
using Bidro.Reviews.Persistence;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Bidro.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReviewsController(IReviewsDb reviewsDb) : ControllerBase
{
    [HttpGet("review/{reviewId}")]
    [SwaggerOperation (Summary = "Get a review by its ID")]
    public async Task<IActionResult> GetReviewById(Guid reviewId)
    {
        return await reviewsDb.GetReviewById(reviewId);
    }

    [HttpGet("reviewsByFirmId/{firmId}")]
    [SwaggerOperation (Summary = "Get reviews by firm ID")]
    public async Task<IActionResult> GetReviewsByFirmId(Guid firmId)
    {
        return await reviewsDb.GetReviewsByFirmId(firmId);
    }

    [HttpPost("createReview")]
    [SwaggerOperation (Summary = "Create a new review")]
    public async Task<IActionResult> CreateReview(Review review)
    {
        return await reviewsDb.CreateReview(review);
    }

    [HttpDelete("review/{reviewId}")]
    [SwaggerOperation (Summary = "Delete a review")]
    public async Task<IActionResult> DeleteReview(Guid reviewId)
    {
        return await reviewsDb.DeleteReview(reviewId);
    }
}
