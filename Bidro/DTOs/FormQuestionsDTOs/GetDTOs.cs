using Bidro.EntityObjects;
using Bidro.Types;

namespace Bidro.DTOs.FormQuestionsDTOs;

public record GetFormQuestionDTO(
    Guid Id,
    string Label,
    InputTypes InputType,
    int OrderInForm,
    bool IsRequired,
    Guid SubcategoryId,
    string DefaultAnswer = "")
{
    public static GetFormQuestionDTO FromFormQuestion(FormQuestion formQuestion)
    {
        return new GetFormQuestionDTO(formQuestion.Id, formQuestion.Label, formQuestion.InputType,
            formQuestion.OrderInForm, formQuestion.IsRequired, formQuestion.SubcategoryId,
            formQuestion.DefaultAnswer);
    }
}