using Bidro.Config;
using Bidro.DTOs.CategoryDTOs;

namespace Bidro.Validation.DomainValidators.PostValidators;

public class SubcategoryPostValidator
{
    public async Task<ValidationResult> ValidateSubcategoryAsync(PostDTOs.PostSubcategoryDTO subcategory)
    {
        var chainValidator = new ChainValidator<PostDTOs.PostSubcategoryDTO>()
            .AddValidator(
                new LengthValidator<PostDTOs.PostSubcategoryDTO>(3, 50, nameof(PostDTOs.PostSubcategoryDTO.Name)))
            .AddValidator(
                new LengthValidator<PostDTOs.PostSubcategoryDTO>(3, 50, nameof(PostDTOs.PostSubcategoryDTO.Icon)))
            .AddValidator(new LengthValidator<PostDTOs.PostSubcategoryDTO>(3, Constants.SubcategoryIdLength,
                nameof(PostDTOs.PostSubcategoryDTO.Identifier)));

        return await chainValidator.ValidateAsync(subcategory);
    }
}