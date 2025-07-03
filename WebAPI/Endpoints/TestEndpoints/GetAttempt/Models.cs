using WebAPI.Endpoints.TestEndpoints.RecordAttempt;

namespace WebAPI.Endpoints.TestEndpoints.GetAttempt;

public sealed class GetAttemptDetailsRequest
{
    public int AttemptId { get; set; }
    public int TestId { get; set; }
}

public sealed class GetAttemptDetailsResponse
{
    public int TimeSpent { get; set; } // in seconds
    public int EarnedPoints { get; set; }
    public List<RecordAttemptAnswerSelection> AnswerSelections { get; set; } = default!;
}