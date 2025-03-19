using Bidro.Config;
using Bidro.EntityObjects;
using Bidro.Services.Implementations;

namespace Bidro.Validation.DatabaseValidators;

public class ListingComponentsValidator(EntityDbContext dbContext)
{
    public async Task<ValidationResult> ValidateListingLocationAsync(ListingComponents.Location listingLocation)
    {
        var chainValidator = new ChainValidatorDb<ListingComponents.Location>()
            .AddValidator(new ExistsValidatorDb<ListingComponents.Location, City>(dbContext.Cities,
                nameof(ListingComponents.Location.CityId), ll => ll.CityId))
            .AddValidator(new ExistsValidatorDb<ListingComponents.Location, County>(dbContext.Counties,
                nameof(ListingComponents.Location.CountyId), ll => ll.CountyId));

        return await chainValidator.ValidateAsync(listingLocation);
    }

    public async Task<ValidationResult> ValidateFormAnswerAsync(ListingComponents.FormAnswer formAnswer)
    {
        var chainValidator = new ChainValidatorDb<ListingComponents.FormAnswer>()
            .AddValidator(new ExistsValidatorDb<ListingComponents.FormAnswer, FormQuestion>(dbContext.FormQuestions,
                nameof(ListingComponents.FormAnswer.FormQuestionId), fa => fa.FormQuestionId));

        return await chainValidator.ValidateAsync(formAnswer);
    }
}