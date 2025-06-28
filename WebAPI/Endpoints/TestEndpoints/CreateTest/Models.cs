namespace WebAPI.Endpoints.TestEndpoints.CreateTest;

public sealed class CreateTestRequest
{
    public required string Title { get; set; }
    public string? Description { get; set; }
    public int Duration { get; set; }
    public int PassingScore { get; set; }
    public List<SectionRequest> Sections { get; set; } = default!;

    public sealed class SectionRequest
    {
        public required string Title { get; set; }
        public string? Description { get; set; }
        public List<QuestionRequest>? Questions { get; set; }

        public sealed class QuestionRequest
        {
            public required string Type { get; set; }
            public string? Text { get; set; }
            public int Points { get; set; }
            public List<AnswerRequest>? Answers { get; set; }

            public sealed class AnswerRequest
            {
                public required string Text { get; set; }
                public bool IsCorrect { get; set; }
            }
        }
    }
}

public sealed class CreateTestResponse
{
    public int Id { get; set; }
}
