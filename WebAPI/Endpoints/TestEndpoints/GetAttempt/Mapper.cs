using WebAPI.Endpoints.TestEndpoints.RecordAttempt;
using WebAPI.Models._testExam;

namespace WebAPI.Endpoints.TestEndpoints.GetAttempt;

public static class Mapper
{
    public static GetAttemptDetailsResponse ToResponse(this TestAttempt entity)
    {
        var groupedAnswers = entity.AnswerSelections
            .GroupBy(sel => sel.QuestionId)
            .Select(group => new RecordAttemptAnswerSelection
            {
                QuestionId = group.Key,
                SelectedAnswerIds = [.. group.Select(sel => sel.SelectedAnswerId)]
            }).ToList();

        return new GetAttemptDetailsResponse
        {
            TimeSpent = entity.TimeSpent,
            AnswerSelections = groupedAnswers
        };
    }
}
