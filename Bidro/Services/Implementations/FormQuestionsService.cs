using System.Data;
using Bidro.DTOs.FormQuestionsDTOs;
using Dapper;

namespace Bidro.Services.Implementations;

public class FormQuestionsService(IDbConnection db) : IFormQuestionsService
{
    public async Task<GetDTOs.GetFormQuestionDTO> AddFormQuestion(PostDTOs.PostFormQuestionDTO formQuestion)
    {
        var sql =
            "INSERT INTO \"FormQuestions\" (\"Label\", \"InputType\", \"OrderInForm\", \"IsRequired\", \"SubcategoryId\", \"DefaultAnswer\") VALUES (@Label, @InputType, @OrderInForm, @IsRequired, @SubcategoryId, @DefaultAnswer) RETURNING \"Id\"";
        var id = await db.ExecuteScalarAsync<Guid>(sql, formQuestion);
        return new GetDTOs.GetFormQuestionDTO(
            id,
            formQuestion.Label,
            formQuestion.InputType,
            formQuestion.OrderInForm,
            formQuestion.IsRequired,
            formQuestion.SubcategoryId,
            formQuestion.DefaultAnswer
        );
    }

    public async Task<bool> UpdateFormQuestions(UpdateDTOs.UpdateFormQuestionsDTO formQuestions)
    {
        using var transaction = db.BeginTransaction();
        try
        {
            foreach (var formQuestion in formQuestions.FormQuestions)
            {
                const string sql =
                    "UPDATE \"FormQuestions\" SET \"Label\" = @Label, \"InputType\" = @InputType, \"OrderInForm\" = @OrderInForm, \"IsRequired\" = @IsRequired, \"DefaultAnswer\" = @DefaultAnswer WHERE \"Id\" = @Id";
                await db.ExecuteAsync(sql, formQuestion, transaction);
            }

            transaction.Commit();
            return true;
        }
        catch (Exception)
        {
            transaction.Rollback();
            return false;
        }
    }


    public async Task<IEnumerable<GetDTOs.GetFormQuestionDTO>> GetFormQuestionsBySubcategory(Guid subcategoryId)
    {
        const string sql =
            "SELECT * FROM \"FormQuestions\" WHERE \"SubcategoryId\" = @SubcategoryId ORDER BY \"OrderInForm\"";
        var formQuestions = await db.QueryAsync<GetDTOs.GetFormQuestionDTO>(sql, new { SubcategoryId = subcategoryId });
        return formQuestions.ToList();
    }
}