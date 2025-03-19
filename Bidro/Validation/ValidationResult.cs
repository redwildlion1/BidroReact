namespace Bidro.Validation;

public class ValidationResult
{
    public ValidationResult()
    {
    }

    public ValidationResult(bool isValid, List<string> errors)
    {
        IsValid = isValid;
        Errors = errors;
    }

    public bool IsValid { get; set; }
    public List<string> Errors { get; } = [];
}