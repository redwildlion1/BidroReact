using Bidro.FrontEndBuildBlocks.FormQuestion;
using Bidro.FrontEndBuildBlocks.FormQuestions.Persistence;
using Microsoft.AspNetCore.Mvc;

namespace Bidro.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FormQuestionsController(IFormQuestionsDb formsDb) : ControllerBase
{
    [HttpPost ("addFormQuestion")]
    public async Task<IActionResult> AddFormQuestion(FormQuestion formQuestion)
    {
        return await formsDb.AddFormQuestion(formQuestion);
    }
    
    [HttpGet ("getFormQuestionsBySubcategory")]
    public async Task<IActionResult> GetFormQuestionsBySubcategory(string subcategoryId)
    {
        return await formsDb.GetFormQuestionsBySubcategory(subcategoryId);
    }
    
}