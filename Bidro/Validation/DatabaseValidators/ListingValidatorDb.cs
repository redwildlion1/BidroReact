using Bidro.Config;
using Bidro.EntityObjects;
using Bidro.Validation.ValidationObjects;

namespace Bidro.Validation.DatabaseValidators;

public class ListingValidatorDb(EntityDbContext dbContext)
{
    public async Task<ValidationResult> ValidateListingAsync(ListingValidityObjectDb listing)
    {
        var chainValidator = new ChainValidatorDb<ListingValidityObjectDb>()
            .AddValidator(new ExistsValidatorDb<ListingValidityObjectDb, Subcategory>(dbContext.Subcategories,
                nameof(Listing.SubcategoryId), l => l.SubcategoryId));

        return await chainValidator.ValidateAsync(listing);
    }
}