using Bidro.Validation.ValidationObjects;
using FluentValidation;

namespace Bidro.Validation.FluentValidators;

public class ListingValidator : AbstractValidator<ListingValidityObject>
{
    public ListingValidator()
    {
        RuleFor(x => x.ListingBase)
            .SetValidator(new ListingBaseValidator());

        RuleFor(x => x.Location)
            .SetValidator(new ListingLocationValidator());

        RuleFor(x => x.FormAnswers)
            .NotEmpty()
            .WithMessage("Form answers cannot be empty")
            .Must(x => x.Count > 0)
            .WithMessage("Form answers must contain at least one answer");
    }
}

public class ListingBaseValidator : AbstractValidator<ListingBaseValidityObject>
{
    public ListingBaseValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty()
            .WithMessage("Title cannot be empty")
            .Length(1, 100)
            .WithMessage("Title must be between 1 and 100 characters");

        RuleFor(x => x.SubcategoryId)
            .NotEmpty()
            .WithMessage("SubcategoryId cannot be empty");
    }
}

public class ListingLocationValidator : AbstractValidator<ListingLocationValidityObject>
{
    public ListingLocationValidator()
    {
        RuleFor(x => x.CityId)
            .NotEmpty()
            .WithMessage("CityId cannot be empty");

        RuleFor(x => x.CountyId)
            .NotEmpty()
            .WithMessage("CountyId cannot be empty");

        RuleFor(x => x.Address)
            .NotEmpty()
            .WithMessage("Address cannot be empty")
            .Length(1, 100)
            .WithMessage("Address must be between 1 and 100 characters");

        RuleFor(x => x.PostalCode)
            .NotEmpty()
            .WithMessage("PostalCode cannot be empty")
            .Length(1, 20)
            .WithMessage("PostalCode must be between 1 and 20 characters");
    }
}

public class ListingContactValidator : AbstractValidator<ListingContactValidityObject>
{
    public ListingContactValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Name cannot be empty")
            .Length(1, 50)
            .WithMessage("Name must be between 1 and 50 characters");

        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage("Email cannot be empty")
            .EmailAddress()
            .WithMessage("Email is not valid");

        RuleFor(x => x.Phone)
            .NotEmpty()
            .WithMessage("Phone cannot be empty")
            .Length(1, 20)
            .WithMessage("Phone must be between 1 and 20 characters");
    }
}