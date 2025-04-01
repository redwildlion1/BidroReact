using Bidro.DTOs.FormQuestionsDTOs;
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
    public async Task<IActionResult> AddFormQuestion(PostDTOs.PostFormQuestionDTO formQuestion)
    {
        var result = await formsService.AddFormQuestion(formQuestion);
        return CreatedAtAction(nameof(AddFormQuestion), new { formQuestionId = result }, result);
    }

    [HttpGet("getFormQuestionsBySubcategory")]
    [SwaggerOperation(Summary = "Get form questions by subcategory")]
    public async Task<IActionResult> GetFormQuestionsBySubcategory(Guid subcategoryId)
    {
        var result = await formsService.GetFormQuestionsBySubcategory(subcategoryId);
        return Ok(result);
    }
}