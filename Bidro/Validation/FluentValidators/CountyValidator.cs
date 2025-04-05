using Bidro.Config;
using Bidro.Validation.ValidationObjects;
using Dapper;
using FluentValidation;

namespace Bidro.Validation.FluentValidators;

public class CountyValidator : AbstractValidator<CountyValidityObject>
{
    public CountyValidator(PgConnectionPool pgConnectionPool)
    {
        RuleFor(x => x.Code)
            .NotEmpty()
            .WithMessage("Code cannot be empty")
            .Length(1, 2)
            .WithMessage("Code must be between 1 and 2 characters");

        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Name cannot be empty")
            .Length(1, 50)
            .WithMessage("Name must be between 1 and 50 characters");

        RuleFor(x => x.Code)
            .MustAsync(async (code, cancellation) =>
            {
                using var connection = await pgConnectionPool.RentAsync();
                const string query = "SELECT COUNT(*) FROM \"Counties\" WHERE \"Code\" = @Code";
                var count = await connection.ExecuteScalarAsync<int>(query, new { Code = code });
                return count == 0;
            })
            .WithMessage("Code already exists in the database");

        RuleFor(x => x.Name)
            .MustAsync(async (name, cancellation) =>
            {
                using var connection = await pgConnectionPool.RentAsync();
                const string query = "SELECT COUNT(*) FROM \"Counties\" WHERE \"Name\" = @Name";
                var count = await connection.ExecuteScalarAsync<int>(query, new { Name = name });
                return count == 0;
            })
            .WithMessage("Name already exists in the database");
    }
}