using Bidro.Validation.ValidationObjects;
using FluentValidation;

namespace Bidro.Validation.FluentValidators;

public class FormQuestionValidator : AbstractValidator<FormQuestionValidityObject>
{
    public FormQuestionValidator()
    {
        RuleFor(x => x.Label)
            .NotEmpty()
            .WithMessage("Label cannot be empty")
            .Length(1, 50)
            .WithMessage("Label must be between 1 and 50 characters");

        RuleFor(x => x.DefaultAnswer)
            .NotEmpty()
            .WithMessage("Default answer cannot be empty")
            .Length(1, 500)
            .WithMessage("Default answer must be between 1 and 500 characters");

        RuleFor(x => x.SubcategoryId)
            .NotEmpty()
            .WithMessage("SubcategoryId cannot be empty");
    }
}