using Bidro.Config;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Bidro.FrontEndBuildBlocks.FormQuestions.Persistence;

public class FormQuestionsDb(DbContextOptions<EntityDbContext> options) : IFormQuestionsDb
{
    public async Task<IActionResult> AddFormQuestion(FormQuestion formQuestion)
    {
        await using var db = new EntityDbContext(options);
        await db.FormQuestions.AddAsync(formQuestion);
        await db.SaveChangesAsync();
        return new OkResult();
    }

    public async Task<IActionResult> GetFormQuestionsBySubcategory(string subcategoryId)
    {
        await using var db = new EntityDbContext(options);
        var formQuestions = await db.FormQuestions.Where(fq => fq.SubcategoryId == subcategoryId).ToListAsync();
        return new OkObjectResult(formQuestions);
    }
}