using Bidro.Config;
using Bidro.EntityObjects;
using Bidro.Services.Implementations;

namespace Bidro.Validation.DatabaseValidators;

public class FirmValidatorDb(EntityDbContext dbContext)
{
    public async Task<ValidationResult> ValidateFirmAsync(Firm firm)
    {
        var chainValidator = new ChainValidatorDb<Firm>()
            .AddValidator(new IsUniqueValidatorDb<Firm>(dbContext.Firms, nameof(Firm.Name)));

        return await chainValidator.ValidateAsync(firm);
    }

    public async Task<ValidationResult> ValidateFirmLocationAsync(FirmLocation firmLocation)
    {
        var chainValidator = new ChainValidatorDb<FirmLocation>()
            .AddValidator(new ExistsValidatorDb<FirmLocation, City>(dbContext.Cities, nameof(FirmLocation.CityId),
                fl => fl.CityId))
            .AddValidator(new ExistsValidatorDb<FirmLocation, County>(dbContext.Counties, nameof(FirmLocation.CountyId),
                fl => fl.CountyId));

        return await chainValidator.ValidateAsync(firmLocation);
    }

    public async Task<ValidationResult> ValidateFirmContactAsync(FirmContact firmContact)
    {
        var chainValidator = new ChainValidatorDb<FirmContact>()
            .AddValidator(new IsUniqueValidatorDb<FirmContact>(dbContext.FirmContacts, nameof(FirmContact.Email)))
            .AddValidator(new IsUniqueValidatorDb<FirmContact>(dbContext.FirmContacts, nameof(FirmContact.Phone)));

        return await chainValidator.ValidateAsync(firmContact);
    }
}