using Bidro.EntityObjects;
using Microsoft.AspNetCore.Mvc;

namespace Bidro.Services;

public interface IReviewsService
{
    Task<IActionResult> GetReviewById(Guid reviewId);
    Task<IActionResult> GetReviewsByFirmId(Guid firmId);
    Task<IActionResult> CreateReview(Review review);
    Task<IActionResult> DeleteReview(Guid reviewId);
}