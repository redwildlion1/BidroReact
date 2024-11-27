namespace Bidro.FrontEndBuildBlocks.Forms;

public static class FormGetters
{
    public static List<FormQuestion> GetFormQuestionsBySubcategoryId(string subcategoryId)
    {
        return
        [
            new FormQuestion(Guid.NewGuid(), "Question 1", InputTypes.Text, true, 1, subcategoryId),
            new FormQuestion(Guid.NewGuid(), "Question 2", InputTypes.Text, true, 2, subcategoryId),
            new FormQuestion(Guid.NewGuid(), "Question 3", InputTypes.Text, true, 3, subcategoryId),
            new FormQuestion(Guid.NewGuid(), "Question 4", InputTypes.Text, true, 4, subcategoryId)
        ];
    }
}