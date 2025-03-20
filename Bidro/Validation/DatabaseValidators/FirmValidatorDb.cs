using Bidro.Config;
using Bidro.EntityObjects;
using Bidro.Services.Implementations;
using Bidro.Validation.ValidationObjects;

namespace Bidro.Validation.DatabaseValidators;

public class FirmValidatorDb(EntityDbContext dbContext)
{
    public async Task<ValidationResult> ValidateFirmAsync(FirmValidityObjectDb firm)
    {
        var firmBaseTask = ValidateFirmBaseAsync(firm.FirmBase);
        var firmLocationTask = ValidateFirmLocationAsync(firm.FirmLocation);
        var firmContactTask = ValidateFirmContactAsync(firm.FirmContact);

        var results = await Task.WhenAll(firmBaseTask, firmLocationTask, firmContactTask);

        var validationResult = new ValidationResult
        {
            IsValid = results.All(result => result.IsValid)
        };

        foreach (var result in results) validationResult.Errors.AddRange(result.Errors);

        return validationResult;
    }

    private async Task<ValidationResult> ValidateFirmBaseAsync(FirmBaseValidityObjectDb firmBase)
    {
        var chainValidator = new ChainValidatorDb<FirmBaseValidityObjectDb>()
            .AddValidator(new IsUniqueValidatorDb<FirmBaseValidityObjectDb, Firm>(dbContext.Firms, nameof(Firm.Name),
                validationObject => validationObject.Name));

        return await chainValidator.ValidateAsync(firmBase);
    }

    private async Task<ValidationResult> ValidateFirmLocationAsync(FirmLocationValidityObjectDb firmLocation)
    {
        var chainValidator = new ChainValidatorDb<FirmLocationValidityObjectDb>()
            .AddValidator(new ExistsValidatorDb<FirmLocationValidityObjectDb, City>(dbContext.Cities,
                nameof(FirmLocation.CityId),
                fl => fl.CityId))
            .AddValidator(new ExistsValidatorDb<FirmLocationValidityObjectDb, County>(dbContext.Counties,
                nameof(FirmLocation.CountyId),
                fl => fl.CountyId));

        return await chainValidator.ValidateAsync(firmLocation);
    }

    private async Task<ValidationResult> ValidateFirmContactAsync(FirmContactValidityObjectDb firmContact)
    {
        var chainValidator = new ChainValidatorDb<FirmContactValidityObjectDb>()
            .AddValidator(new IsUniqueValidatorDb<FirmContactValidityObjectDb, FirmContact>(dbContext.FirmContacts,
                nameof(FirmContact.Email),
                fc => fc.Email))
            .AddValidator(new IsUniqueValidatorDb<FirmContactValidityObjectDb, FirmContact>(dbContext.FirmContacts,
                nameof(FirmContact.Phone),
                fc => fc.Phone));

        return await chainValidator.ValidateAsync(firmContact);
    }
}