using Bidro.DTOs.FormQuestionsDTOs;

namespace Bidro.Validation.ValidationObjects;

public class FormQuestionValidityObject
{
    public FormQuestionValidityObject(PostFormQuestionDTO formQuestionDTO)
    {
        Label = formQuestionDTO.Label;
        DefaultAnswer = formQuestionDTO.DefaultAnswer;
        SubcategoryId = formQuestionDTO.SubcategoryId;
    }

    public FormQuestionValidityObject(UpdateFormQuestionDTO formQuestionDTO)
    {
        Label = formQuestionDTO.Label;
        DefaultAnswer = formQuestionDTO.DefaultAnswer;
        SubcategoryId = formQuestionDTO.SubcategoryId;
    }

    public string Label { get; }
    public string DefaultAnswer { get; }
    public Guid SubcategoryId { get; }
}