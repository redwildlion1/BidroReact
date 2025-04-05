using Bidro.Config;
using Bidro.DTOs.FormQuestionsDTOs;
using Bidro.Services;
using Bidro.Validation.FluentValidators;
using Bidro.Validation.ValidationObjects;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Bidro.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FormQuestionsController(IFormQuestionsService formsService, PgConnectionPool pgConnectionPool)
    : ControllerBase
{
    [HttpPost("addFormQuestion")]
    [SwaggerOperation(Summary = "Add a new form question")]
    public async Task<IResult> AddFormQuestion(PostFormQuestionDTO formQuestion)
    {
        var validator = new FormQuestionValidator(pgConnectionPool);
        var validityObject = new FormQuestionValidityObject(formQuestion);
        var validationResult = await validator.ValidateAsync(validityObject);
        if (!validationResult.IsValid) return Results.BadRequest(validationResult.Errors);

        var result = await formsService.AddFormQuestion(formQuestion);
        return Results.Created($"/api/formQuestions/{result.Id}", result);
    }

    [HttpGet("getFormQuestionsBySubcategory")]
    [SwaggerOperation(Summary = "Get form questions by subcategory")]
    public async Task<IResult> GetFormQuestionsBySubcategory(Guid subcategoryId)
    {
        var result = await formsService.GetFormQuestionsBySubcategory(subcategoryId);
        return Results.Ok(result);
    }
}