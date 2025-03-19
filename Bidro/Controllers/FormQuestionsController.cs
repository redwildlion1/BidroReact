using Bidro.EntityObjects;
using Bidro.Services;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Bidro.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FormQuestionsController(IFormQuestionsService formsService) : ControllerBase
{
    [HttpPost("addFormQuestion")]
    [SwaggerOperation(Summary = "Add a new form question")]
    public async Task<IActionResult> AddFormQuestion(FormQuestion formQuestion)
    {
        return await formsService.AddFormQuestion(formQuestion);
    }

    [HttpGet("getFormQuestionsBySubcategory")]
    [SwaggerOperation(Summary = "Get form questions by subcategory")]
    public async Task<IActionResult> GetFormQuestionsBySubcategory(Guid subcategoryId)
    {
        return await formsService.GetFormQuestionsBySubcategory(subcategoryId);
    }
}