using WebAPI.Models.Base;

namespace WebAPI.Endpoints.TestEndpoints.GetDetails;

public sealed class GetTestDetailsRequest
{
    public int Id { get; set; }
}

public sealed class GetTestDetailsResponse
{
    public int Id { get; set; }
    public required string Title { get; set; }
    public string? Description { get; set; }
    public int Duration { get; set; }

    public int SectionCount { get; set; }
    public int AttemptCount { get; set; }
    public int CommentCount { get; set; }
    public int QuestionCount { get; set; }

    public List<GetTestDetailsSection> Sections { get; set; } = default!;
    public List<GetTestDetailsComment> Comments { get; set; } = default!;

    public sealed class GetTestDetailsComment : IReadonlySupportResponse
    {
        public int Id { get; set; }
        public string Text { get; set; } = default!;
        public GetTestDetailsCommentAuthor Author { get; set; } = default!;
        public bool IsReadOnly { get; set; }
        public DateTimeOffset CreationDate { get; set; }

        public sealed class GetTestDetailsCommentAuthor
        {
            public int Id { get; set; }
            public string ProfilePicture { get; set; } = default!;
            public string Name { get; set; } = default!;
        }
    }

    public sealed class GetTestDetailsSection
    {
        public int Id { get; set; }
        public string Title { get; set; } = default!;
        public string? Description { get; set; }
    }
}

