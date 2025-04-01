using Bidro.Types;

namespace Bidro.DTOs.FormQuestionsDTOs;

public class UpdateDTOs
{
    public record UpdateFormQuestionDTO(
        Guid Id,
        string Label,
        InputTypes InputType,
        int OrderInForm,
        bool IsRequired,
        Guid SubcategoryId,
        string DefaultAnswer = "");

    public record UpdateFormQuestionsDTO(
        List<UpdateFormQuestionDTO> FormQuestions,
        Guid SubcategoryId);
}