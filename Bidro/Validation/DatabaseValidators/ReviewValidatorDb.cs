using Bidro.Config;
using Bidro.EntityObjects;
using Bidro.Types;

namespace Bidro.Validation.DatabaseValidators;

public class ReviewValidatorDb(EntityDbContext dbContext)
{
    public async Task<ValidationResult> ValidateAsync(Review review)
    {
        var chainValidator = new ChainValidatorDb<Review>()
            .AddValidator(new ExistsValidatorDb<Review, UserTypes.UserAccount>(dbContext.UserAccounts,
                nameof(Review.UserId), r => r.UserId))
            .AddValidator(new ExistsValidatorDb<Review, Firm>(dbContext.Firms,
                nameof(Review.FirmId), r => r.FirmId));

        return await chainValidator.ValidateAsync(review);
    }
}