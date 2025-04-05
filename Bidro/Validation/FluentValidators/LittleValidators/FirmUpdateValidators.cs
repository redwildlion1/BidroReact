using Bidro.Config;
using Bidro.DTOs.FirmDTOs;
using Dapper;
using FluentValidation;

namespace Bidro.Validation.FluentValidators.LittleValidators;

public class FirmNameValidator : AbstractValidator<UpdateFirmNameDTO>
{
    public FirmNameValidator(PgConnectionPool pgConnectionPool)
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Firm name cannot be empty.")
            .Length(3, 50)
            .WithMessage("Firm name must be between 3 and 50 characters long.");

        RuleFor(x => x.Name)
            .MustAsync(async (name, cancellation) =>
            {
                using var connection = await pgConnectionPool.RentAsync();
                const string query = "SELECT COUNT(*) FROM \"Firms\" WHERE \"Name\" = @Name";
                var count = await connection.ExecuteScalarAsync<int>(query, new { Name = name });
                return count == 0;
            })
            .WithMessage("Name already exists in the database");
    }
}

public class FirmDescriptionValidator : AbstractValidator<UpdateFirmDescriptionDTO>
{
    public FirmDescriptionValidator()
    {
        RuleFor(x => x.Description)
            .NotEmpty()
            .WithMessage("Firm description cannot be empty.")
            .Length(10, 500)
            .WithMessage("Firm description must be between 10 and 500 characters long.");
    }
}

public class FirmWebsiteValidator : AbstractValidator<UpdateFirmWebsiteDTO>
{
    public FirmWebsiteValidator()
    {
        RuleFor(x => x.Website)
            .NotEmpty()
            .WithMessage("Firm website cannot be empty.")
            .Matches(@"^(http|https)://[^\s/$.?#].[^\s]*$")
            .WithMessage("Firm website must be a valid URL.");
    }
}