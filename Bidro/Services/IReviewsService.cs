using Bidro.DTOs.ReviewDTOs;

namespace Bidro.Services;

public interface IReviewsService
{
    Task<GetReviewDTO> GetReviewById(Guid reviewId);
    Task<IEnumerable<GetReviewDTO>> GetReviewsByFirmId(Guid firmId);
    Task<bool> CreateReview(PostReviewDTO review);
    Task<bool> DeleteReview(Guid reviewId);
}