using Bidro.Config;
using Bidro.DTOs.CategoryDTOs;
using Bidro.Validation.ValidationObjects;
using Dapper;
using FluentValidation;

namespace Bidro.Validation.FluentValidators;

public class CategoryValidator : AbstractValidator<CategoryValidityObject>
{
    public CategoryValidator(PgConnectionPool pgConnectionPool)
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
            .Length(ValidationConstants.CategoryIdentifierLength)
            .WithMessage($"Identifier must be {ValidationConstants.CategoryIdentifierLength} characters long");

        //Database validation 

        RuleFor(x => x.Identifier)
            .MustAsync(async (identifier, cancellation) =>
            {
                using var connection = await pgConnectionPool.RentAsync();
                const string query = "SELECT COUNT(*) FROM \"Categories\" WHERE \"Identifier\" = @Identifier";
                var count = await connection.ExecuteScalarAsync<int>(query, new { Identifier = identifier });
                return count == 0;
            })
            .WithMessage("Identifier already exists in the database");

        RuleFor(x => x.Name)
            .MustAsync(async (name, cancellation) =>
            {
                using var connection = await pgConnectionPool.RentAsync();
                const string query = "SELECT COUNT(*) FROM \"Categories\" WHERE \"Name\" = @Name";
                var count = await connection.ExecuteScalarAsync<int>(query, new { Name = name });
                return count == 0;
            })
            .WithMessage("Name already exists in the database");

        RuleFor(x => x.Icon)
            .MustAsync(async (icon, cancellation) =>
            {
                using var connection = await pgConnectionPool.RentAsync();
                const string query = "SELECT COUNT(*) FROM \"Categories\" WHERE \"Icon\" = @Icon";
                var count = await connection.ExecuteScalarAsync<int>(query, new { Icon = icon });
                return count == 0;
            })
            .WithMessage("Icon already exists in the database");
    }
}

public class UpdateCategoryValidator : AbstractValidator<UpdateCategoryDTO>
{
    public UpdateCategoryValidator(PgConnectionPool pgConnectionPool)
    {
        var categoryValidator = new CategoryValidator(pgConnectionPool);
        RuleFor(x => new CategoryValidityObject(x))
            .SetValidator(categoryValidator);

        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Id cannot be empty")
            .MustAsync(async (id, cancellation) =>
            {
                using var connection = await pgConnectionPool.RentAsync();
                const string query = "SELECT COUNT(*) FROM \"Categories\" WHERE \"Id\" = @Id";
                var count = await connection.ExecuteScalarAsync<int>(query, new { Id = id });
                return count > 0;
            })
            .WithMessage("Category with this Id does not exist");
    }
}