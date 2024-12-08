using Bidro.FrontEndBuildBlocks.FormQuestions;
using Bidro.FrontEndBuildBlocks.FormQuestions.Persistence;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Bidro.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FormQuestionsController(IFormQuestionsDb formsDb) : ControllerBase
{
    [HttpPost ("addFormQuestion")]
    [SwaggerOperation (Summary = "Add a new form question")]
    public async Task<IActionResult> AddFormQuestion(FormQuestion formQuestion)
    {
        return await formsDb.AddFormQuestion(formQuestion);
    }
    
    [HttpGet ("getFormQuestionsBySubcategory")]
    [SwaggerOperation (Summary = "Get form questions by subcategory")]
    public async Task<IActionResult> GetFormQuestionsBySubcategory(string subcategoryId)
    {
        return await formsDb.GetFormQuestionsBySubcategory(subcategoryId);
    }
    
}