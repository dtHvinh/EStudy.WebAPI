using FastEndpoints;
using FluentValidation;

namespace WebAPI.Endpoints.FlashCardSetEndpoints.CreateSet;

public sealed class CreateFlashCardSetRequest
{
    public string Title { get; set; } = default!;
    public string Description { get; set; } = default!;

    public sealed class Validator : Validator<CreateFlashCardSetRequest>
    {
        public Validator()
        {
            RuleFor(x => x.Title).NotEmpty().WithMessage("Flash card set name is required");
        }
    }
}

public sealed class CreateFlashCardSetResponse
{
    public int Id { get; set; }
    public string Title { get; set; } = default!;
}
