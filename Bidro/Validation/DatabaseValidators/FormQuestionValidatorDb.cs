using Bidro.Config;
using Bidro.EntityObjects;

namespace Bidro.Validation.DatabaseValidators;

public class FormQuestionValidatorDb(EntityDbContext dbContext)
{
    public async Task<ValidationResult> ValidateFormQuestionAsync(FormQuestion formQuestion)
    {
        var chainValidator = new ChainValidatorDb<FormQuestion>()
            .AddValidator(new ExistsValidatorDb<FormQuestion, Subcategory>(dbContext.Subcategories,
                nameof(FormQuestion.SubcategoryId), fq => fq.SubcategoryId));

        return await chainValidator.ValidateAsync(formQuestion);
    }
}