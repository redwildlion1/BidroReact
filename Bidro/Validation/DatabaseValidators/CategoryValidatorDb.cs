using Bidro.Config;
using Bidro.EntityObjects;

namespace Bidro.Validation.DatabaseValidators;

public class CategoryValidatorDb(EntityDbContext dbContext)
{
    public async Task<ValidationResult> ValidateCategoryAsync(Category category)
    {
        var chainValidator = new ChainValidatorDb<Category>()
            .AddValidator(new IsUniqueValidatorDb<Category>(dbContext.Categories, nameof(Category.Name)))
            .AddValidator(new IsUniqueValidatorDb<Category>(dbContext.Categories, nameof(Category.Identifier)));

        return await chainValidator.ValidateAsync(category);
    }
}