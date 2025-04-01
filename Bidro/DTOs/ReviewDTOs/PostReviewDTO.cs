namespace Bidro.DTOs.ReviewDTOs;

public class PostReviewDTO
{
    public Guid ListingId { get; set; }
    public string ReviewText { get; set; } = string.Empty;
    public int Rating { get; set; }
    public Guid UserId { get; set; }
    public DateTime Date { get; set; } = DateTime.UtcNow;
}