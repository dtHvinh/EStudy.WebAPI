using FastEndpoints;
using FluentValidation;

namespace WebAPI.Endpoints.AIEndpoints.GetSentenceFeedback;

public sealed class GetSentenceFeedbackRequest
{
    public string Sentence { get; set; } = string.Empty;

    public sealed class Validator : Validator<GetSentenceFeedbackRequest>
    {
        public Validator()
        {
            RuleFor(x => x.Sentence)
                .NotEmpty().WithMessage("Sentence cannot be empty.")
                .MaximumLength(500).WithMessage("Sentence cannot exceed 500 characters.");
        }
    }
}

public sealed class GetSentenceFeedbackResponse
{
    public string Sentence { get; set; } = default!;
    public List<string> GrammarIssues { get; set; } = default!;
    public List<string> Suggestions { get; set; } = default!;
    public string Explanation { get; set; } = default!;
    public string NewSentence { get; set; } = default!;
}