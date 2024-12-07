using Microsoft.AspNetCore.Mvc;

namespace Bidro.Reviews.Persistence;

public interface IReviewsDb
{
    Task<IActionResult> GetReviewById(Guid reviewId);
    Task<IActionResult> GetReviewsByFirmId(Guid firmId);
    Task<IActionResult> CreateReview(Review review);
    Task<IActionResult> DeleteReview(Guid reviewId);
}