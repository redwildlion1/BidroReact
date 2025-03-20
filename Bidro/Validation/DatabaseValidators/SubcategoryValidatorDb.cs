using Bidro.Config;
using Bidro.EntityObjects;
using Bidro.Validation.ValidationObjects;

namespace Bidro.Validation.DatabaseValidators;

public class SubcategoryValidatorDb(EntityDbContext dbContext)
{
    public async Task<ValidationResult> ValidateSubcategoryAsync(SubcategoryValidityObjectDb subDb)
    {
        var chainValidator = new ChainValidatorDb<SubcategoryValidityObjectDb>()
            .AddValidator(
                new IsUniqueValidatorDb<SubcategoryValidityObjectDb, Subcategory>
                    (dbContext.Subcategories, nameof(Subcategory.Name), validityObject => validityObject.Name))
            .AddValidator(
                new IsUniqueValidatorDb<SubcategoryValidityObjectDb, Subcategory>
                (dbContext.Subcategories, nameof(Subcategory.Identifier),
                    validityObject => validityObject.Identifier));

        return await chainValidator.ValidateAsync(subDb);
    }
}