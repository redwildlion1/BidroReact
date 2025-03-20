using System.Reflection;

namespace Bidro.Validation;

public interface IValidator<in T> where T : class
{
    Task<ValidationResult> ValidateAsync(T entity);
}

public class ChainValidator<T> : IValidator<T> where T : class
{
    private readonly List<IValidator<T>> _validators = new();

    public async Task<ValidationResult> ValidateAsync(T entity)
    {
        var validationTasks = _validators.Select(validator => validator.ValidateAsync(entity)).ToList();
        var results = await Task.WhenAll(validationTasks);

        var errors = results.Where(result => !result.IsValid)
            .SelectMany(result => result.Errors)
            .ToList();

        return new ValidationResult(errors.Count == 0, errors);
    }

    public ChainValidator<T> AddValidator(IValidator<T> validator)
    {
        _validators.Add(validator);
        return this;
    }
}

public class LengthValidator<T>(int minLength, int maxLength, string propertyName) : IValidator<T>
    where T : class
{
    public Task<ValidationResult> ValidateAsync(T entity)
    {
        var property = typeof(T).GetProperty(propertyName, BindingFlags.Public | BindingFlags.Instance);
        if (property == null)
            throw new ArgumentException($"Property '{propertyName}' not found on type '{typeof(T).Name}'.");

        var value = property.GetValue(entity) as string;
        if (value == null)
            throw new ArgumentException($"Property '{propertyName}' is not a string.");

        var validationResult = new ValidationResult { IsValid = true };

        if (value.Length < minLength)
        {
            validationResult.IsValid = false;
            validationResult.Errors.Add($"The value for '{propertyName}' is too short.");
        }

        if (value.Length <= maxLength) return Task.FromResult(validationResult);
        validationResult.IsValid = false;
        validationResult.Errors.Add($"The value for '{propertyName}' is too long.");

        return Task.FromResult(validationResult);
    }
}

public class EmailValidator<T>(string propertyName) : IValidator<T>
    where T : class
{
    public Task<ValidationResult> ValidateAsync(T entity)
    {
        var property = typeof(T).GetProperty(propertyName, BindingFlags.Public | BindingFlags.Instance);
        if (property == null)
            throw new ArgumentException($"Property '{propertyName}' not found on type '{typeof(T).Name}'.");

        var value = property.GetValue(entity) as string;
        if (value == null)
            throw new ArgumentException($"Property '{propertyName}' is not a string.");

        var validationResult = new ValidationResult { IsValid = true };

        if (value.Contains("@")) return Task.FromResult(validationResult);
        validationResult.IsValid = false;
        validationResult.Errors.Add($"The value for '{propertyName}' is not a valid email address.");

        return Task.FromResult(validationResult);
    }
}

public class NotEmptyValidator<T> : IValidator<T> where T : class
{
    public Task<ValidationResult> ValidateAsync(T entity)
    {
        var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
        var validationResult = new ValidationResult { IsValid = true };

        foreach (var property in properties)
        {
            var value = property.GetValue(entity);
            if (value == null)
            {
                validationResult.IsValid = false;
                validationResult.Errors.Add($"The value for '{property.Name}' is null.");
            }
            else if (value is string stringValue && string.IsNullOrWhiteSpace(stringValue))
            {
                validationResult.IsValid = false;
                validationResult.Errors.Add($"The value for '{property.Name}' is empty.");
            }
        }

        return Task.FromResult(validationResult);
    }
}

public class RomanianPostalCodeValidator<T> : IValidator<T> where T : class
{
    public Task<ValidationResult> ValidateAsync(T entity)
    {
        var property = typeof(T).GetProperty("PostalCode", BindingFlags.Public | BindingFlags.Instance);
        if (property == null)
            throw new ArgumentException($"Property 'PostalCode' not found on type '{typeof(T).Name}'.");

        var value = property.GetValue(entity) as string;
        if (value == null)
            throw new ArgumentException("Property 'PostalCode' is not a string.");

        var validationResult = new ValidationResult { IsValid = true };

        if (value.Length == 6 && value.All(char.IsDigit)) return Task.FromResult(validationResult);
        validationResult.IsValid = false;
        validationResult.Errors.Add("The value for 'PostalCode' is not a valid Romanian postal code.");

        return Task.FromResult(validationResult);
    }
}

public class RomanianPhoneNumberValidator<T> : IValidator<T> where T : class
{
    public Task<ValidationResult> ValidateAsync(T entity)
    {
        var property = typeof(T).GetProperty("Phone", BindingFlags.Public | BindingFlags.Instance);
        if (property == null)
            throw new ArgumentException($"Property 'Phone' not found on type '{typeof(T).Name}'.");

        var value = property.GetValue(entity) as string;
        if (value == null)
            throw new ArgumentException("Property 'Phone' is not a string.");

        var validationResult = new ValidationResult { IsValid = true };

        if (value.Length == 10 && value.All(char.IsDigit)) return Task.FromResult(validationResult);
        validationResult.IsValid = false;
        validationResult.Errors.Add("The value for 'Phone' is not a valid Romanian phone number.");

        return Task.FromResult(validationResult);
    }
}