using Bidro.Config;
using Bidro.Validation.ValidationObjects;
using Dapper;
using FluentValidation;

namespace Bidro.Validation.FluentValidators;

public class CityValidator : AbstractValidator<CityValidityObject>
{
    public CityValidator(PgConnectionPool pgConnectionPool)
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Name cannot be empty")
            .Length(1, 50)
            .WithMessage("Name must be between 1 and 50 characters");

        RuleFor(x => x.CountyId)
            .NotEmpty()
            .WithMessage("CountyId cannot be empty");

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