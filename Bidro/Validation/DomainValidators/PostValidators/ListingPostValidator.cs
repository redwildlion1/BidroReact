using Bidro.DTOs.ListingDTOs;

namespace Bidro.Validation.DomainValidators.PostValidators;

public class ListingPostValidator
{
    public async Task<ValidationResult> ValidateListingAsync(PostDTOs.PostListingDTO postListingDTO)
    {
        var chainValidator = new ChainValidator<PostDTOs.PostListingDTO>()
            .AddValidator(new LengthValidator<PostDTOs.PostListingDTO>(3, 50, nameof(PostDTOs.PostListingDTO.Title)));

        return await chainValidator.ValidateAsync(postListingDTO);
    }
}