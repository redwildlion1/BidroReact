using Bidro.EntityObjects;

namespace Bidro.DTOs.ReviewDTOs;

public class GetReviewDTO
{
    public Guid Id { get; set; }
    public string ReviewText { get; set; } = string.Empty;
    public int Rating { get; set; }
    public Guid UserId { get; set; }
    public DateTime Date { get; set; } = DateTime.UtcNow;

    public static GetReviewDTO FromReview(Review review)
    {
        return new GetReviewDTO
        {
            Id = review.Id,
            ReviewText = review.ReviewText,
            Rating = review.Rating,
            UserId = review.UserId,
            Date = review.Date
        };
    }
}