using Bidro.Config;
using Bidro.EntityObjects;
using Bidro.Validation.ValidationObjects;

namespace Bidro.Validation.DatabaseValidators;

public class CategoryValidatorDb(EntityDbContext dbContext)
{
    public async Task<ValidationResult> ValidateCategoryAsync(CategoryValidityObjectDb category)
    {
        var chainValidator = new ChainValidatorDb<CategoryValidityObjectDb>()
            .AddValidator(new IsUniqueValidatorDb<CategoryValidityObjectDb, Category>
                (dbContext.Categories, nameof(Category.Name), validityObject => validityObject.Name))
            .AddValidator(new IsUniqueValidatorDb<CategoryValidityObjectDb, Category>
                (dbContext.Categories, nameof(Category.Identifier), validityObject => validityObject.Identifier));

        return await chainValidator.ValidateAsync(category);
    }
}