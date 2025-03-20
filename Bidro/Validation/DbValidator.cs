using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace Bidro.Validation;

public interface IValidatorDb<in T> where T : class
{
    Task<ValidationResult> ValidateAsync(T entity);
}

public class ChainValidatorDb<T> : IValidatorDb<T> where T : class
{
    private readonly List<IValidatorDb<T>> _validators = new();

    public async Task<ValidationResult> ValidateAsync(T entity)
    {
        var validationResult = new ValidationResult { IsValid = true };

        var validationTasks = _validators.Select(validator => validator.ValidateAsync(entity)).ToList();
        var results = await Task.WhenAll(validationTasks);

        foreach (var result in results)
        {
            if (result.IsValid) continue;
            validationResult.IsValid = false;
            validationResult.Errors.AddRange(result.Errors);
        }

        return validationResult;
    }

    public ChainValidatorDb<T> AddValidator(IValidatorDb<T> validatorDb)
    {
        _validators.Add(validatorDb);
        return this;
    }
}

public class IsUniqueValidatorDb<TParent, TChild>(
    DbSet<TChild> dbSet,
    string propertyName,
    Func<TParent, object> valueSelector)
    : IValidatorDb<TParent>
    where TParent : class
    where TChild : class
{
    public async Task<ValidationResult> ValidateAsync(TParent entity)
    {
        var value = valueSelector(entity);
        var property = typeof(TChild).GetProperty(propertyName, BindingFlags.Public | BindingFlags.Instance);
        if (property == null)
            throw new ArgumentException($"Property '{propertyName}' not found on type '{typeof(TChild).Name}'.");

        var existingEntity = await dbSet.FirstOrDefaultAsync(e => property.GetValue(e)!.Equals(value));
        var validationResult = new ValidationResult { IsValid = existingEntity == null };

        if (!validationResult.IsValid)
            validationResult.Errors.Add($"The value '{value}' for '{propertyName}' is not unique.");

        return validationResult;
    }
}

public class ExistsValidatorDb<TParent, TChild>(
    DbSet<TChild> dbSet,
    string propertyName,
    Func<TParent, object> valueSelector)
    : IValidatorDb<TParent>
    where TParent : class
    where TChild : class
{
    public async Task<ValidationResult> ValidateAsync(TParent entity)
    {
        var value = valueSelector(entity);
        var property = typeof(TChild).GetProperty(propertyName, BindingFlags.Public | BindingFlags.Instance);
        if (property == null)
            throw new ArgumentException($"Property '{propertyName}' not found on type '{typeof(TChild).Name}'.");

        var exists = await dbSet.AnyAsync(e => property.GetValue(e)!.Equals(value));
        var validationResult = new ValidationResult { IsValid = exists };

        if (!validationResult.IsValid)
            validationResult.Errors.Add(
                $"The value '{value}' for '{propertyName}' does not exist in '{typeof(TChild).Name}'.");

        return validationResult;
    }
}