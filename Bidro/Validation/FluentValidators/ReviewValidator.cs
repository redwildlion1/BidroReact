using Bidro.Config;
using Bidro.Validation.ValidationObjects;
using Dapper;
using FluentValidation;

namespace Bidro.Validation.FluentValidators;

public class ReviewValidator : AbstractValidator<ReviewValidityObject>
{
    public ReviewValidator(PgConnectionPool pgConnectionPool)
    {
        RuleFor(x => x.Content)
            .NotEmpty()
            .WithMessage("Review text cannot be empty")
            .Length(1, 500)
            .WithMessage("Review text must be between 1 and 500 characters");

        RuleFor(x => x.Rating)
            .InclusiveBetween(1, 5)
            .WithMessage("Rating must be between 1 and 5");

        RuleFor(x => x.UserId)
            .NotEmpty()
            .WithMessage("UserId cannot be empty");

        RuleFor(x => x.FirmId)
            .NotEmpty()
            .WithMessage("FirmId cannot be empty");

        RuleFor(x => x.UserId)
            .MustAsync(async (userId, cancellation) =>
            {
                using var connection = await pgConnectionPool.RentAsync();
                const string query = "SELECT COUNT(*) FROM \"Users\" WHERE \"Id\" = @UserId";
                var count = await connection.ExecuteScalarAsync<int>(query, new { UserId = userId });
                return count > 0;
            })
            .WithMessage("UserId does not exist in the database");

        RuleFor(x => x.FirmId)
            .MustAsync(async (firmId, cancellation) =>
            {
                using var connection = await pgConnectionPool.RentAsync();
                const string query = "SELECT COUNT(*) FROM \"Firms\" WHERE \"Id\" = @FirmId";
                var count = await connection.ExecuteScalarAsync<int>(query, new { FirmId = firmId });
                return count > 0;
            })
            .WithMessage("FirmId does not exist in the database");
    }
}