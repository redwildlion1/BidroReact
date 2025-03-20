using Bidro.DTOs.FormQuestionsDTOs;

namespace Bidro.Validation.ValidationObjects;

public class FormQuestionValidityObject
{
    public FormQuestionValidityObject(PostDTOs.PostFormQuestionDTO formQuestionDTO)
    {
        Label = formQuestionDTO.Label;
        DefaultAnswer = formQuestionDTO.DefaultAnswer;
    }

    public FormQuestionValidityObject(UpdateDTOs.UpdateFormQuestionDTO formQuestionDTO)
    {
        Label = formQuestionDTO.Label;
        DefaultAnswer = formQuestionDTO.DefaultAnswer;
    }

    public string Label { get; }
    public string DefaultAnswer { get; }
}

public class FormQuestionValidityObjectDb(PostDTOs.PostFormQuestionDTO formQuestionDTO)
{
    public Guid SubcategoryId { get; } = formQuestionDTO.SubcategoryId;
}