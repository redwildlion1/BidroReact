using Bidro.Config;
using Bidro.Validation.ValidationObjects;
using Dapper;
using FluentValidation;

namespace Bidro.Validation.FluentValidators;

public class FirmValidator : AbstractValidator<FirmValidityObject>
{
    public FirmValidator(PgConnectionPool pgConnectionPool)
    {
        RuleFor(x => x.FirmBase)
            .SetValidator(new FirmBaseValidator(pgConnectionPool));

        RuleFor(x => x.FirmLocation)
            .SetValidator(new FirmLocationValidator(pgConnectionPool));

        RuleFor(x => x.FirmContact)
            .SetValidator(new FirmContactValidator(pgConnectionPool));
    }
}

public class FirmBaseValidator : AbstractValidator<FirmBaseValidityObject>
{
    public FirmBaseValidator(PgConnectionPool pgConnectionPool)
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

        RuleFor(x => x.Website)
            .NotEmpty()
            .WithMessage("Website cannot be empty")
            .Length(1, 100)
            .WithMessage("Website must be between 1 and 100 characters");

        // Validate the website format
        RuleFor(x => x.Website)
            .Matches(@"^(http|https)://[^\s/$.?#].[^\s]*$")
            .WithMessage("Invalid website format");


        RuleFor(x => x.Name)
            .MustAsync(async (name, cancellation) =>
            {
                using var connection = await pgConnectionPool.RentAsync();
                const string query = "SELECT COUNT(*) FROM \"Firms\" WHERE \"Name\" = @Name";
                var count = await connection.ExecuteScalarAsync<int>(query, new { Name = name });
                return count == 0;
            })
            .WithMessage("Name already exists in the database");

        RuleFor(x => x.Website)
            .MustAsync(async (website, cancellation) =>
            {
                using var connection = await pgConnectionPool.RentAsync();
                const string query = "SELECT COUNT(*) FROM \"Firms\" WHERE \"Website\" = @Website";
                var count = await connection.ExecuteScalarAsync<int>(query, new { Website = website });
                return count == 0;
            });
    }
}

public class FirmLocationValidator : AbstractValidator<FirmLocationValidityObject>
{
    public FirmLocationValidator(PgConnectionPool pgConnectionPool)
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

        RuleFor(x => x.CityId)
            .MustAsync(async (cityId, cancellation) =>
            {
                using var connection = await pgConnectionPool.RentAsync();
                const string query = "SELECT COUNT(*) FROM \"Cities\" WHERE \"Id\" = @CityId";
                var count = await connection.ExecuteScalarAsync<int>(query, new { CityId = cityId });
                return count > 0;
            })
            .WithMessage("CityId does not exist in the database");

        RuleFor(x => x.CountyId)
            .MustAsync(async (countyId, cancellation) =>
            {
                using var connection = await pgConnectionPool.RentAsync();
                const string query = "SELECT COUNT(*) FROM \"Counties\" WHERE \"Id\" = @CountyId";
                var count = await connection.ExecuteScalarAsync<int>(query, new { CountyId = countyId });
                return count > 0;
            })
            .WithMessage("CountyId does not exist in the database");
    }
}

public class FirmContactValidator : AbstractValidator<FirmContactValidityObject>
{
    public FirmContactValidator(PgConnectionPool pgConnectionPool)
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

        RuleFor(x => x.Phone)
            .Matches(@"^(?:\+40|0)\d{9}$")
            .WithMessage("Phone must be a valid Romanian phone number format");

        RuleFor(x => x.Email)
            .MustAsync(async (email, cancellation) =>
            {
                using var connection = await pgConnectionPool.RentAsync();
                const string query = "SELECT COUNT(*) FROM \"FirmContacts\" WHERE \"Email\" = @Email";
                var count = await connection.ExecuteScalarAsync<int>(query, new { Email = email });
                return count == 0;
            })
            .WithMessage("Email already exists in the database");

        RuleFor(x => x.Phone)
            .MustAsync(async (phone, cancellation) =>
            {
                using var connection = await pgConnectionPool.RentAsync();
                const string query = "SELECT COUNT(*) FROM \"FirmContacts\" WHERE \"Phone\" = @Phone";
                var count = await connection.ExecuteScalarAsync<int>(query, new { Phone = phone });
                return count == 0;
            })
            .WithMessage("Phone already exists in the database");

        // Fax is optional, but if provided, it should be valid
        RuleFor(x => x.Fax)
            .Matches(@"^(?:\+40|0)\d{9}$")
            .WithMessage("Fax must be a valid Romanian phone number format")
            .When(x => !string.IsNullOrEmpty(x.Fax));

        RuleFor(x => x.Fax)
            .MustAsync(async (fax, cancellation) =>
            {
                using var connection = await pgConnectionPool.RentAsync();
                const string query = "SELECT COUNT(*) FROM \"FirmContacts\" WHERE \"Fax\" = @Fax";
                var count = await connection.ExecuteScalarAsync<int>(query, new { Fax = fax });
                return count == 0;
            })
            .WithMessage("Fax already exists in the database")
            .When(x => !string.IsNullOrEmpty(x.Fax));
    }
}