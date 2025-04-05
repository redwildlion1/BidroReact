using Bidro.Config;
using Bidro.DTOs.ReviewDTOs;
using Bidro.Services;
using Bidro.Validation.FluentValidators;
using Bidro.Validation.ValidationObjects;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Bidro.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReviewsController(IReviewsService reviewsService, PgConnectionPool pgConnectionPool) : ControllerBase
{
    [HttpGet("review/{reviewId}")]
    [SwaggerOperation(Summary = "Get a review by its ID")]
    public async Task<IResult> GetReviewById(Guid reviewId)
    {
        var result = await reviewsService.GetReviewById(reviewId);
        return Results.Ok(result);
    }

    [HttpGet("reviewsByFirmId/{firmId}")]
    [SwaggerOperation(Summary = "Get reviews by firm ID")]
    public async Task<IResult> GetReviewsByFirmId(Guid firmId)
    {
        var result = await reviewsService.GetReviewsByFirmId(firmId);
        return Results.Ok(result);
    }

    [HttpPost("createReview")]
    [SwaggerOperation(Summary = "Create a new review")]
    public async Task<IResult> CreateReview(PostReviewDTO review)
    {
        var validator = new ReviewValidator(pgConnectionPool);
        var validityObject = new ReviewValidityObject(review);
        var validationResult = await validator.ValidateAsync(validityObject);
        if (!validationResult.IsValid) return Results.BadRequest(validationResult.Errors);

        var result = await reviewsService.CreateReview(review);
        return Results.Created($"/api/reviews/{result}", result);
    }

    [HttpDelete("review/{reviewId}")]
    [SwaggerOperation(Summary = "Delete a review")]
    public async Task<IResult> DeleteReview(Guid reviewId)
    {
        var result = await reviewsService.DeleteReview(reviewId);
        return Results.Ok(result);
    }
}