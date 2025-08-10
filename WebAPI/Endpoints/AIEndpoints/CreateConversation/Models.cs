using FastEndpoints;
using FluentValidation;

namespace WebAPI.Endpoints.AIEndpoints.CreateConversation;

public sealed class CreateConversationRequest
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Context { get; set; } = string.Empty;

    public sealed class CreateConversationRequestValidator : Validator<CreateConversationRequest>
    {
        public CreateConversationRequestValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required.");
            RuleFor(x => x.Context).NotEmpty().WithMessage("Context is required.");
        }
    }
}
