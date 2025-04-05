using Bidro.DTOs.FormQuestionsDTOs;

namespace Bidro.Services;

public interface IFormQuestionsService
{
    public Task<GetFormQuestionDTO> AddFormQuestion(PostFormQuestionDTO postFormQuestionDTO);
    public Task<bool> UpdateFormQuestions(UpdateFormQuestionsDTO updateFormQuestionDTO);
    public Task<IEnumerable<GetFormQuestionDTO>> GetFormQuestionsBySubcategory(Guid subcategoryId);
}