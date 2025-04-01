using Bidro.DTOs.FormQuestionsDTOs;

namespace Bidro.Validation.ValidationObjects;

public class FormQuestionValidityObject
{
    public FormQuestionValidityObject(PostDTOs.PostFormQuestionDTO formQuestionDTO)
    {
        Label = formQuestionDTO.Label;
        DefaultAnswer = formQuestionDTO.DefaultAnswer;
        SubcategoryId = formQuestionDTO.SubcategoryId;
    }

    public FormQuestionValidityObject(UpdateDTOs.UpdateFormQuestionDTO formQuestionDTO)
    {
        Label = formQuestionDTO.Label;
        DefaultAnswer = formQuestionDTO.DefaultAnswer;
        SubcategoryId = formQuestionDTO.SubcategoryId;
    }

    public string Label { get; }
    public string DefaultAnswer { get; }
    public Guid SubcategoryId { get; }
}