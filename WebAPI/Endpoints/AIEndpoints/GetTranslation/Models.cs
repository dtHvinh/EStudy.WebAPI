using FastEndpoints;
using FluentValidation;

namespace WebAPI.Endpoints.AIEndpoints.GetTranslation;

public sealed class GetTranslationRequest
{
    public string? Context { get; set; } = default!;
    public string Text { get; set; } = default!;
    public string Language { get; set; } = default!;

    public sealed class Validator : Validator<GetTranslationRequest>
    {
        public Validator()
        {
            RuleFor(x => x.Text).NotEmpty().WithMessage("Text is required.");
            RuleFor(x => x.Language).NotEmpty().WithMessage("Language is required.");
        }
    }
}

public sealed class GetTranslationResponse
{
    public string TranslatedText { get; set; } = default!;
}
