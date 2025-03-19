using Bidro.EntityObjects;
using Bidro.Types;

namespace Bidro.DTOs.FormQuestionsDTOs;

public class PostDTOs
{
    public record PostFormQuestionDTO(
        string Label,
        InputTypes InputType,
        int OrderInForm,
        bool IsRequired,
        Guid SubcategoryId,
        string DefaultAnswer = "")
    {
        public FormQuestion ToFormQuestion()
        {
            return new FormQuestion(
                Label,
                OrderInForm,
                InputType,
                IsRequired,
                SubcategoryId,
                DefaultAnswer
            );
        }
    }
}