using Bidro.Validation.ValidationObjects;
using FluentValidation;

namespace Bidro.Validation.FluentValidators;

public class FirmValidator : AbstractValidator<FirmValidityObject>
{
    public FirmValidator()
    {
        RuleFor(x => x.FirmBase)
            .SetValidator(new FirmBaseValidator());

        RuleFor(x => x.FirmLocation)
            .SetValidator(new FirmLocationValidator());

        RuleFor(x => x.FirmContact)
            .SetValidator(new FirmContactValidator());
    }
}

public class FirmBaseValidator : AbstractValidator<FirmBaseValidityObject>
{
    public FirmBaseValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Name cannot be empty")
            .Length(1, 50)
            .WithMessage("Name must be between 1 and 50 characters");

        RuleFor(x => x.Description)
            .NotEmpty()
            .WithMessage("Description cannot be empty")
            .Length(1, 500)
            .WithMessage("Description must be between 1 and 500 characters");

        RuleFor(x => x.Logo)
            .NotEmpty()
            .WithMessage("Logo cannot be empty")
            .Length(1, 100)
            .WithMessage("Logo must be between 1 and 100 characters");
    }
}

public class FirmLocationValidator : AbstractValidator<FirmLocationValidityObject>
{
    public FirmLocationValidator()
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

public class FirmContactValidator : AbstractValidator<FirmContactValidityObject>
{
    public FirmContactValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage("Email cannot be empty")
            .EmailAddress()
            .WithMessage("Invalid email format");

        RuleFor(x => x.Phone)
            .NotEmpty()
            .WithMessage("Phone cannot be empty")
            .Length(1, 20)
            .WithMessage("Phone must be between 1 and 20 characters");
    }
}