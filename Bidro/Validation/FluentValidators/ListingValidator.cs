using Bidro.Config;
using Bidro.Validation.ValidationObjects;
using Dapper;
using FluentValidation;

namespace Bidro.Validation.FluentValidators;

public class ListingValidator : AbstractValidator<ListingValidityObject>
{
    public ListingValidator(PgConnectionPool pgConnectionPool)
    {
        RuleFor(x => x.ListingBase)
            .SetValidator(new ListingBaseValidator(pgConnectionPool));

        RuleFor(x => x.Location)
            .SetValidator(new ListingLocationValidator(pgConnectionPool));

        RuleFor(x => x.FormAnswers)
            .NotEmpty()
            .WithMessage("Form answers cannot be empty")
            .Must(x => x.Count > 0)
            .WithMessage("Form answers must contain at least one answer");
    }
}

public class ListingBaseValidator : AbstractValidator<ListingBaseValidityObject>
{
    public ListingBaseValidator(PgConnectionPool pgConnectionPool)
    {
        RuleFor(x => x.Title)
            .NotEmpty()
            .WithMessage("Title cannot be empty")
            .Length(1, 100)
            .WithMessage("Title must be between 1 and 100 characters");

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

public class ListingLocationValidator : AbstractValidator<ListingLocationValidityObject>
{
    public ListingLocationValidator(PgConnectionPool pgConnectionPool)
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

public class ListingContactValidator : AbstractValidator<ListingContactValidityObject>
{
    public ListingContactValidator(PgConnectionPool pgConnectionPool)
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

        RuleFor(x => x.Email)
            .MustAsync(async (email, cancellation) =>
            {
                using var connection = await pgConnectionPool.RentAsync();
                const string query = "SELECT COUNT(*) FROM \"ListingContacts\" WHERE \"Email\" = @Email";
                var count = await connection.ExecuteScalarAsync<int>(query, new { Email = email });
                return count == 0;
            })
            .WithMessage("Email already exists in the database");

        RuleFor(x => x.Phone)
            .MustAsync(async (phone, cancellation) =>
            {
                using var connection = await pgConnectionPool.RentAsync();
                const string query = "SELECT COUNT(*) FROM \"ListingContacts\" WHERE \"Phone\" = @Phone";
                var count = await connection.ExecuteScalarAsync<int>(query, new { Phone = phone });
                return count == 0;
            })
            .WithMessage("Phone already exists in the database");
    }
}

public class ListingAnswersValidator : AbstractValidator<List<FormAnswerValidityObject>>
{
    public ListingAnswersValidator(PgConnectionPool pgConnectionPool)
    {
        RuleForEach(x => x)
            .SetValidator(new FormAnswerValidator(pgConnectionPool));
    }
}

public class FormAnswerValidator : AbstractValidator<FormAnswerValidityObject>
{
    public FormAnswerValidator(PgConnectionPool pgConnectionPool)
    {
        RuleFor(x => x.FormQuestionId)
            .NotEmpty()
            .WithMessage("QuestionId cannot be empty");

        RuleFor(x => x.Value)
            .NotEmpty()
            .WithMessage("Answer cannot be empty")
            .Length(1, 500)
            .WithMessage("Answer must be between 1 and 500 characters");

        RuleFor(x => x.FormQuestionId)
            .MustAsync(async (questionId, cancellation) =>
            {
                using var connection = await pgConnectionPool.RentAsync();
                const string query = "SELECT COUNT(*) FROM \"FormQuestions\" WHERE \"Id\" = @QuestionId";
                var count = await connection.ExecuteScalarAsync<int>(query, new { QuestionId = questionId });
                return count > 0;
            })
            .WithMessage("QuestionId does not exist in the database");
    }
}