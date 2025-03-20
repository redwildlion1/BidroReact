using Bidro.Config;
using Bidro.EntityObjects;
using Bidro.Services.Implementations;
using Bidro.Validation.ValidationObjects;

namespace Bidro.Validation.DatabaseValidators;

public class ListingComponentsValidatorDb(EntityDbContext dbContext)
{
    public async Task<ValidationResult> ValidateListingLocationAsync(ListingLocationValidityObjectDb listingLocation)
    {
        var chainValidator = new ChainValidatorDb<ListingLocationValidityObjectDb>()
            .AddValidator(new ExistsValidatorDb<ListingLocationValidityObjectDb, City>(dbContext.Cities,
                nameof(ListingLocation.CityId), ll => ll.CityId))
            .AddValidator(new ExistsValidatorDb<ListingLocationValidityObjectDb, County>(dbContext.Counties,
                nameof(ListingLocation.CountyId), ll => ll.CountyId));

        return await chainValidator.ValidateAsync(listingLocation);
    }

    public async Task<ValidationResult> ValidateFormAnswerAsync(FormAnswer formAnswer)
    {
        var chainValidator = new ChainValidatorDb<FormAnswer>()
            .AddValidator(new ExistsValidatorDb<FormAnswer, FormQuestion>(dbContext.FormQuestions,
                nameof(FormAnswer.FormQuestionId), fa => fa.FormQuestionId));

        return await chainValidator.ValidateAsync(formAnswer);
    }
}