using Bidro.DTOs.FirmDTOs;

namespace Bidro.Validation.DomainValidators.PostValidators;

public class FirmPostValidator
{
    public async Task<ValidationResult> ValidateFirmBaseAsync(PostDTOs.PostFirmBaseDTO firm)
    {
        var chainValidator = new ChainValidator<PostDTOs.PostFirmBaseDTO>()
            .AddValidator(new LengthValidator<PostDTOs.PostFirmBaseDTO>(3, 50, nameof(PostDTOs.PostFirmBaseDTO.Name)))
            .AddValidator(
                new LengthValidator<PostDTOs.PostFirmBaseDTO>(3, 500, nameof(PostDTOs.PostFirmBaseDTO.Description)));

        return await chainValidator.ValidateAsync(firm);
    }

    public async Task<ValidationResult> ValidateFirmContactAsync(PostDTOs.PostFirmContactDTO contact)
    {
        var chainValidator = new ChainValidator<PostDTOs.PostFirmContactDTO>()
            .AddValidator(new EmailValidator<PostDTOs.PostFirmContactDTO>(nameof(PostDTOs.PostFirmContactDTO.Email)))
            .AddValidator(new RomanianPhoneNumberValidator<PostDTOs.PostFirmContactDTO>());

        return await chainValidator.ValidateAsync(contact);
    }

    public async Task<ValidationResult> ValidateFirmLocationAsync(PostDTOs.PostFirmLocationDTO location)
    {
        var chainValidator = new ChainValidator<PostDTOs.PostFirmLocationDTO>()
            .AddValidator(
                new LengthValidator<PostDTOs.PostFirmLocationDTO>(3, 50, nameof(PostDTOs.PostFirmLocationDTO.Address)))
            .AddValidator(new RomanianPostalCodeValidator<PostDTOs.PostFirmLocationDTO>());

        return await chainValidator.ValidateAsync(location);
    }
}