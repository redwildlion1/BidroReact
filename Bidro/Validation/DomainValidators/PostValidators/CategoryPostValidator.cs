using Bidro.Config;
using Bidro.DTOs.CategoryDTOs;

namespace Bidro.Validation.DomainValidators.PostValidators;

public class CategoryPostValidator
{
    public async Task<ValidationResult> ValidateCategoryAsync(PostDTOs.PostCategoryDTO category)
    {
        var chainValidator = new ChainValidator<PostDTOs.PostCategoryDTO>()
            .AddValidator(new LengthValidator<PostDTOs.PostCategoryDTO>(3, 50, nameof(PostDTOs.PostCategoryDTO.Name)))
            .AddValidator(new LengthValidator<PostDTOs.PostCategoryDTO>(3, 50, nameof(PostDTOs.PostCategoryDTO.Icon)))
            .AddValidator(new LengthValidator<PostDTOs.PostCategoryDTO>(3, Constants.CategoryIdLength,
                nameof(PostDTOs.PostCategoryDTO.Identifier)));

        return await chainValidator.ValidateAsync(category);
    }

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