using Bidro.Config;
using Bidro.DTOs.FormQuestionsDTOs;
using Bidro.EntityObjects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Bidro.Services.Implementations;

public class FormQuestionsService(EntityDbContext db) : IFormQuestionsService
{
    public async Task<IActionResult> AddFormQuestion(FormQuestion formQuestion)
    {
        await db.FormQuestions.AddAsync(formQuestion);
        await db.SaveChangesAsync();
        return new OkResult();
    }

    public async Task<IActionResult> UpdateFormQuestion(UpdateDTOs.UpdateFormQuestionDTO updateFormQuestionDTO)
    {
        var formQuestion =
            await db.FormQuestions.FirstOrDefaultAsync(f => f.Id == updateFormQuestionDTO.Id);
        if (formQuestion == null) return new NotFoundResult();
        formQuestion.Label = updateFormQuestionDTO.Label;
        formQuestion.InputType = updateFormQuestionDTO.InputType;
        formQuestion.OrderInForm = updateFormQuestionDTO.OrderInForm;
        formQuestion.IsRequired = updateFormQuestionDTO.IsRequired;
        formQuestion.SubcategoryId = updateFormQuestionDTO.SubcategoryId;
        formQuestion.DefaultAnswer = updateFormQuestionDTO.DefaultAnswer;
        await db.SaveChangesAsync();
        return new OkResult();
    }


    public async Task<IActionResult> GetFormQuestionsBySubcategory(Guid subcategoryId)
    {
        var formQuestions = await db.FormQuestions
            .Where(f => f.SubcategoryId == subcategoryId)
            .ToListAsync();
        if (formQuestions.Count == 0) return new NotFoundResult();
        return new OkObjectResult(formQuestions);
    }
}