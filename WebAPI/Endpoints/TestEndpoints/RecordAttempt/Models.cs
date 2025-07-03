namespace WebAPI.Endpoints.TestEndpoints.RecordAttempt;

public sealed class RecordAttemptRequest
{
    public int TestExamId { get; set; }
    public int TimeSpent { get; set; } // in seconds
    public int EarnedPoints { get; set; }
    public List<RecordAttemptAnswerSelection> AnswerSelections { get; set; } = default!;
}

public sealed class RecordAttemptAnswerSelection
{
    public int QuestionId { get; set; }
    public List<int> SelectedAnswerIds { get; set; } = default!;
}