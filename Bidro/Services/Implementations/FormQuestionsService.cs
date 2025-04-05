using Bidro.Config;
using Bidro.DTOs.FormQuestionsDTOs;
using Dapper;

namespace Bidro.Services.Implementations;

public class FormQuestionsService(PgConnectionPool pgConnectionPool) : IFormQuestionsService
{
    public async Task<GetFormQuestionDTO> AddFormQuestion(PostFormQuestionDTO formQuestion)
    {
        const string sql =
            "INSERT INTO \"FormQuestions\" (\"Label\", \"InputType\", \"OrderInForm\", \"IsRequired\", \"SubcategoryId\", \"DefaultAnswer\") VALUES (@Label, @InputType, @OrderInForm, @IsRequired, @SubcategoryId, @DefaultAnswer) RETURNING \"Id\"";

        using var db = await pgConnectionPool.RentAsync();
        var id = await db.ExecuteScalarAsync<Guid>(sql, formQuestion);
        return new GetFormQuestionDTO(
            id,
            formQuestion.Label,
            formQuestion.InputType,
            formQuestion.OrderInForm,
            formQuestion.IsRequired,
            formQuestion.SubcategoryId,
            formQuestion.DefaultAnswer
        );
    }

    public async Task<bool> UpdateFormQuestions(UpdateFormQuestionsDTO formQuestions)
    {
        using var db = await pgConnectionPool.RentAsync();
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


    public async Task<IEnumerable<GetFormQuestionDTO>> GetFormQuestionsBySubcategory(Guid subcategoryId)
    {
        const string sql =
            "SELECT * FROM \"FormQuestions\" WHERE \"SubcategoryId\" = @SubcategoryId ORDER BY \"OrderInForm\"";

        using var db = await pgConnectionPool.RentAsync();
        var formQuestions = await db.QueryAsync<GetFormQuestionDTO>(sql, new { SubcategoryId = subcategoryId });
        return formQuestions.ToList();
    }
}