using Bidro.Config;
using Bidro.EntityObjects;
using Bidro.Validation.ValidationObjects;

namespace Bidro.Validation.DatabaseValidators;

public class FormQuestionValidatorDb(EntityDbContext dbContext)
{
    public async Task<ValidationResult> ValidateFormQuestionAsync(FormQuestionValidityObjectDb formQuestion)
    {
        var chainValidator = new ChainValidatorDb<FormQuestionValidityObjectDb>()
            .AddValidator(new ExistsValidatorDb<FormQuestionValidityObjectDb, Subcategory>(dbContext.Subcategories,
                nameof(FormQuestionValidityObjectDb.SubcategoryId), fq => fq.SubcategoryId));

        return await chainValidator.ValidateAsync(formQuestion);
    }
}