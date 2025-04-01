using Bidro.DTOs.ReviewDTOs;
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
        var result = await reviewsService.GetReviewById(reviewId);
        return Ok(result);
    }

    [HttpGet("reviewsByFirmId/{firmId}")]
    [SwaggerOperation(Summary = "Get reviews by firm ID")]
    public async Task<IActionResult> GetReviewsByFirmId(Guid firmId)
    {
        var result = await reviewsService.GetReviewsByFirmId(firmId);
        return Ok(result);
    }

    [HttpPost("createReview")]
    [SwaggerOperation(Summary = "Create a new review")]
    public async Task<IActionResult> CreateReview(PostReviewDTO review)
    {
        var result = await reviewsService.CreateReview(review);
        return CreatedAtAction(nameof(GetReviewById), new { reviewId = result }, result);
    }

    [HttpDelete("review/{reviewId}")]
    [SwaggerOperation(Summary = "Delete a review")]
    public async Task<IActionResult> DeleteReview(Guid reviewId)
    {
        var result = await reviewsService.DeleteReview(reviewId);
        return Ok(result);
    }
}