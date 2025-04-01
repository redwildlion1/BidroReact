using Bidro.DTOs.FormQuestionsDTOs;

namespace Bidro.Services;

public interface IFormQuestionsService
{
    public Task<GetDTOs.GetFormQuestionDTO> AddFormQuestion(PostDTOs.PostFormQuestionDTO postFormQuestionDTO);
    public Task<bool> UpdateFormQuestions(UpdateDTOs.UpdateFormQuestionsDTO updateFormQuestionDTO);
    public Task<IEnumerable<GetDTOs.GetFormQuestionDTO>> GetFormQuestionsBySubcategory(Guid subcategoryId);
}