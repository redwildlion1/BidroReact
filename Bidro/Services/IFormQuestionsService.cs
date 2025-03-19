using Bidro.DTOs.FormQuestionsDTOs;
using Bidro.EntityObjects;
using Microsoft.AspNetCore.Mvc;

namespace Bidro.Services;

public interface IFormQuestionsService
{
    public Task<IActionResult> AddFormQuestion(FormQuestion formQuestion);
    public Task<IActionResult> UpdateFormQuestion(UpdateDTOs.UpdateFormQuestionDTO updateFormQuestionDTO);
    public Task<IActionResult> GetFormQuestionsBySubcategory(Guid subcategoryId);
}