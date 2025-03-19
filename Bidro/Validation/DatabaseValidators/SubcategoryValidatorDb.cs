using Bidro.Config;
using Bidro.EntityObjects;

namespace Bidro.Validation.DatabaseValidators;

public class SubcategoryValidatorDb(EntityDbContext dbContext)
{
    public async Task<ValidationResult> ValidateSubcategoryAsync(Subcategory subcategory)
    {
        var chainValidator = new ChainValidatorDb<Subcategory>()
            .AddValidator(new IsUniqueValidatorDb<Subcategory>(dbContext.Subcategories, nameof(Subcategory.Name)))
            .AddValidator(
                new IsUniqueValidatorDb<Subcategory>(dbContext.Subcategories, nameof(Subcategory.Identifier)));

        return await chainValidator.ValidateAsync(subcategory);
    }
}