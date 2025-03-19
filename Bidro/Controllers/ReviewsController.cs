using Bidro.EntityObjects;
using Bidro.Services;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Bidro.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReviewsController(IReviewsService reviewsService) : ControllerBase
{
    [HttpGet("review/{reviewId}")]
    [SwaggerOperation(Summary = "Get a review by its ID")]
    public async Task<IActionResult> GetReviewById(Guid reviewId)
    {
        return await reviewsService.GetReviewById(reviewId);
    }

    [HttpGet("reviewsByFirmId/{firmId}")]
    [SwaggerOperation(Summary = "Get reviews by firm ID")]
    public async Task<IActionResult> GetReviewsByFirmId(Guid firmId)
    {
        return await reviewsService.GetReviewsByFirmId(firmId);
    }

    [HttpPost("createReview")]
    [SwaggerOperation(Summary = "Create a new review")]
    public async Task<IActionResult> CreateReview(Review review)
    {
        return await reviewsService.CreateReview(review);
    }

    [HttpDelete("review/{reviewId}")]
    [SwaggerOperation(Summary = "Delete a review")]
    public async Task<IActionResult> DeleteReview(Guid reviewId)
    {
        return await reviewsService.DeleteReview(reviewId);
    }
}