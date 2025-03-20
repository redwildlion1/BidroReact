using Bidro.DTOs.FormQuestionsDTOs;

namespace Bidro.Validation.DomainValidators.PostValidators;

public class FormQuestionPostValidator
{
    public async Task<ValidationResult> ValidateFormQuestionAsync(PostDTOs.PostFormQuestionDTO formQuestion)
    {
        var chainValidator = new ChainValidator<PostDTOs.PostFormQuestionDTO>()
            .AddValidator(
                new LengthValidator<PostDTOs.PostFormQuestionDTO>(3, 50, nameof(PostDTOs.PostFormQuestionDTO.Label)))
            .AddValidator(new LengthValidator<PostDTOs.PostFormQuestionDTO>(3, 50,
                nameof(PostDTOs.PostFormQuestionDTO.DefaultAnswer)));

        return await chainValidator.ValidateAsync(formQuestion);
    }
}