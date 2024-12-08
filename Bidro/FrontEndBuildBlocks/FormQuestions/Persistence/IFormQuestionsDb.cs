using Microsoft.AspNetCore.Mvc;

namespace Bidro.FrontEndBuildBlocks.FormQuestions.Persistence;

public interface IFormQuestionsDb
{
    public Task<IActionResult> AddFormQuestion(FormQuestion formQuestion);
    public Task<IActionResult> GetFormQuestionsBySubcategory(string subcategoryId);
}