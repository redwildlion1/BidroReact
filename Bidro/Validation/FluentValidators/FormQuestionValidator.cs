using Bidro.Config;
using Bidro.Validation.ValidationObjects;
using Dapper;
using FluentValidation;

namespace Bidro.Validation.FluentValidators;

public class FormQuestionValidator : AbstractValidator<FormQuestionValidityObject>
{
    public FormQuestionValidator(PgConnectionPool pgConnectionPool)
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

        RuleFor(x => x.SubcategoryId)
            .MustAsync(async (subcategoryId, cancellation) =>
            {
                using var connection = await pgConnectionPool.RentAsync();
                const string query = "SELECT COUNT(*) FROM \"Subcategories\" WHERE \"Id\" = @SubcategoryId";
                var count = await connection.ExecuteScalarAsync<int>(query, new { SubcategoryId = subcategoryId });
                return count > 0;
            })
            .WithMessage("SubcategoryId does not exist in the database");
    }
}