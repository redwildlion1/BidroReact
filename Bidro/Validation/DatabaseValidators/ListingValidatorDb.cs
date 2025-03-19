using Bidro.Config;
using Bidro.EntityObjects;

namespace Bidro.Validation.DatabaseValidators;

public class ListingValidatorDb(EntityDbContext dbContext)
{
    public async Task<ValidationResult> ValidateListingAsync(Listing listing)
    {
        var chainValidator = new ChainValidatorDb<Listing>()
            .AddValidator(new ExistsValidatorDb<Listing, Subcategory>(dbContext.Subcategories,
                nameof(Listing.SubcategoryId), l => l.SubcategoryId));

        return await chainValidator.ValidateAsync(listing);
    }
}