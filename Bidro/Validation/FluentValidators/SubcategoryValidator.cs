using Bidro.Config;
using Bidro.Validation.ValidationObjects;
using FluentValidation;

namespace Bidro.Validation.FluentValidators;

public class SubcategoryValidator : AbstractValidator<SubcategoryValidityObject>
{
    public SubcategoryValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Name cannot be empty")
            .Length(1, 50)
            .WithMessage("Name must be between 1 and 50 characters");

        RuleFor(x => x.Icon)
            .NotEmpty()
            .WithMessage("Icon cannot be empty")
            .Length(1, 50)
            .WithMessage("Icon must be between 1 and 50 characters");

        RuleFor(x => x.Identifier)
            .NotEmpty()
            .WithMessage("Identifier cannot be empty")
            .Length(ValidationConstants.SubcategoryIdentifierLength)
            .WithMessage($"Identifier must be {ValidationConstants.SubcategoryIdentifierLength} characters long");
    }
}