using FastEndpoints;
using FluentValidation;

namespace WebAPI.Endpoints.TestEndpoints.SendComment;

public class SendCommentRequest
{
    public int TestId { get; set; }
    public string Text { get; set; } = default!;

    public sealed class Validator : Validator<SendCommentRequest>
    {
        public Validator()
        {
            RuleFor(x => x.TestId).NotEmpty().WithMessage("TestId is required");
            RuleFor(x => x.Text).NotEmpty().WithMessage("Comment can not be empty");
        }
    }
}

public class SendCommentResponse
{
    public int Id { get; set; }
    public string Text { get; set; } = default!;

    public CommentAuthor Author { get; set; } = default!;

    public sealed class CommentAuthor
    {
        public string Name { get; set; } = default!;
        public string ProfilePicture { get; set; } = default!;
    }
}