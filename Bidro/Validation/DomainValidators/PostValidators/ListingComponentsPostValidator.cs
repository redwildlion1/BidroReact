using Bidro.DTOs.ListingDTOs;

namespace Bidro.Validation.DomainValidators.PostValidators;

public class ListingComponentsPostValidator
{
    public async Task<ValidationResult> ValidateListingLocationAsync(PostDTOs.PostLocationDTO postLocationDTO)
    {
        var chainValidator = new ChainValidator<PostDTOs.PostLocationDTO>()
            .AddValidator(
                new LengthValidator<PostDTOs.PostLocationDTO>(3, 50, nameof(PostDTOs.PostLocationDTO.Address)))
            .AddValidator(new RomanianPostalCodeValidator<PostDTOs.PostLocationDTO>());

        return await chainValidator.ValidateAsync(postLocationDTO);
    }

    public async Task<ValidationResult> ValidateListingContactAsync(PostDTOs.PostContactDTO postContactDTO)
    {
        var chainValidator = new ChainValidator<PostDTOs.PostContactDTO>()
            .AddValidator(new EmailValidator<PostDTOs.PostContactDTO>(nameof(PostDTOs.PostContactDTO.Email)))
            .AddValidator(new LengthValidator<PostDTOs.PostContactDTO>(3, 50, nameof(PostDTOs.PostContactDTO.Name)))
            .AddValidator(new RomanianPhoneNumberValidator<PostDTOs.PostContactDTO>());

        return await chainValidator.ValidateAsync(postContactDTO);
    }

    public async Task<ValidationResult> ValidateFormAnswerAsync(PostDTOs.PostFormAnswerDTO postFormAnswerDTO)
    {
        var chainValidator = new ChainValidator<PostDTOs.PostFormAnswerDTO>()
            .AddValidator(
                new LengthValidator<PostDTOs.PostFormAnswerDTO>(3, 50, nameof(PostDTOs.PostFormAnswerDTO.Value)));

        return await chainValidator.ValidateAsync(postFormAnswerDTO);
    }
}