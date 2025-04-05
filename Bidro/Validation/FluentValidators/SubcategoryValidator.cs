using Bidro.Config;
using Bidro.DTOs.CategoryDTOs;
using Bidro.Validation.ValidationObjects;
using Dapper;
using FluentValidation;

namespace Bidro.Validation.FluentValidators;

public class SubcategoryValidator : AbstractValidator<SubcategoryValidityObject>
{
    public SubcategoryValidator(PgConnectionPool pgConnectionPool)
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

        //Database validation

        RuleFor(x => x.Identifier)
            .MustAsync(async (identifier, cancellation) =>
            {
                using var connection = await pgConnectionPool.RentAsync();
                const string query = "SELECT COUNT(*) FROM \"Subcategories\" WHERE \"Identifier\" = @Identifier";
                var count = await connection.ExecuteScalarAsync<int>(query, new { Identifier = identifier });
                return count == 0;
            })
            .WithMessage("Identifier already exists in the database");
    }
}

public class UpdateSubcategoryValidator : AbstractValidator<UpdateSubcategoryDTO>
{
    public UpdateSubcategoryValidator(PgConnectionPool pgConnectionPool)
    {
        var subcategoryValidator = new SubcategoryValidator(pgConnectionPool);
        RuleFor(x => new SubcategoryValidityObject(x))
            .SetValidator(subcategoryValidator);

        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Id cannot be empty")
            .MustAsync(async (id, cancellation) =>
            {
                using var connection = await pgConnectionPool.RentAsync();
                const string query = "SELECT COUNT(*) FROM \"Subcategories\" WHERE \"Id\" = @Id";
                var count = await connection.ExecuteScalarAsync<int>(query, new { Id = id });
                return count > 0;
            })
            .WithMessage("Subcategory with this Id does not exist");
    }
}