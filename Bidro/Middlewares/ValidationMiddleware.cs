using FluentValidation;

namespace Bidro.Middlewares;

public class ValidationMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext context)
    {
        var endpoint = context.GetEndpoint();
        if (endpoint == null)
        {
            await next(context);
            return;
        }

        var validatorType = typeof(IValidator<>).MakeGenericType(endpoint.Metadata.GetMetadata<Type>());
        var validator = context.RequestServices.GetService(validatorType) as IValidator;
        if (validator == null)
        {
            await next(context);
            return;
        }

        var body = await context.Request.ReadFromJsonAsync(endpoint.Metadata.GetMetadata<Type>());
        if (body == null)
        {
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            await context.Response.WriteAsync("Invalid request body");
            return;
        }

        var validationResult = await validator.ValidateAsync(new ValidationContext<object>(body));
        if (!validationResult.IsValid)
        {
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            await context.Response.WriteAsJsonAsync(validationResult.Errors);
            return;
        }

        await next(context);
    }
}