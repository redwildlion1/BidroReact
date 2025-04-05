using Bidro.DTOs.ReviewDTOs;

namespace Bidro.Validation.ValidationObjects;

public class ReviewValidityObject(PostReviewDTO reviewDTO)
{
    public string Content { get; } = reviewDTO.ReviewText;
    public int Rating { get; } = reviewDTO.Rating;

    public Guid UserId { get; set; } = reviewDTO.UserId;
    public Guid FirmId { get; set; } = reviewDTO.FirmId;
}