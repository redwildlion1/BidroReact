using Bidro.Config;
using Bidro.EntityObjects;
using Bidro.Services.Implementations;
using Bidro.Validation.ValidationObjects;

namespace Bidro.Validation.DatabaseValidators;

public class ListingValidatorDb(EntityDbContext dbContext)
{
    public async Task<ValidationResult> ValidateListingAsync(ListingValidityObjectDb listing)
    {
        var listingBaseTask = ValidateListingBaseAsync(listing.ListingBase);
        var listingLocationTask = ValidateListingLocationAsync(listing.Location);
        var formAnswerTasks = listing.FormAnswers.Select(ValidateFormAnswerAsync);
        
        var results = await Task.WhenAll(listingBaseTask, listingLocationTask)
            .ContinueWith(t => t.Result.Concat(Task.WhenAll(formAnswerTasks).Result).ToArray());
        var validationResult = new ValidationResult
        {
            IsValid = results.All(result => result.IsValid)
        };

        foreach (var result in results) validationResult.Errors.AddRange(result.Errors);

        return validationResult;
    }

    private async Task<ValidationResult> ValidateListingBaseAsync(ListingBaseValidityObjectDb listingBase)
    {
        var chainValidator = new ChainValidatorDb<ListingBaseValidityObjectDb>()
            .AddValidator(new ExistsValidatorDb<ListingBaseValidityObjectDb, Subcategory>(dbContext.Subcategories,
                nameof(Listing.SubcategoryId), l => l.SubcategoryId));

        return await chainValidator.ValidateAsync(listingBase);
    }

    private async Task<ValidationResult> ValidateListingLocationAsync(ListingLocationValidityObjectDb listingLocation)
    {
        var chainValidator = new ChainValidatorDb<ListingLocationValidityObjectDb>()
            .AddValidator(new ExistsValidatorDb<ListingLocationValidityObjectDb, City>(dbContext.Cities,
                nameof(ListingLocation.CityId), ll => ll.CityId))
            .AddValidator(new ExistsValidatorDb<ListingLocationValidityObjectDb, County>(dbContext.Counties,
                nameof(ListingLocation.CountyId), ll => ll.CountyId));

        return await chainValidator.ValidateAsync(listingLocation);
    }

    //No need for listing contact validation

    private async Task<ValidationResult> ValidateFormAnswerAsync(FormAnswerValidityObjectDb formAnswer)
    {
        var chainValidator = new ChainValidatorDb<FormAnswerValidityObjectDb>()
            .AddValidator(new ExistsValidatorDb<FormAnswerValidityObjectDb, FormQuestion>(dbContext.FormQuestions,
                nameof(FormAnswerValidityObjectDb.FormQuestionId), fa => fa.FormQuestionId));

        return await chainValidator.ValidateAsync(formAnswer);
    }
}