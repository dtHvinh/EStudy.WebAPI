using FastEndpoints;
using FluentValidation;
using WebAPI.Utilities.Extensions;

namespace WebAPI.Endpoints.FlashCardSetEndpoints.CreateSet;

public sealed class CreateFlashCardSetRequest
{
    public string Title { get; set; } = default!;
    public string Description { get; set; } = default!;

    public sealed class Validator : Validator<CreateFlashCardSetRequest>
    {
        public Validator()
        {
            RuleFor(x => x.Title).NotEmpty().WithMessage("Flash card set name is required")
                .MaximumLength(50).WithMessage("Flash card set name is too long");

            RuleFor(x => x.Description)
                .Must(e => e.NothingOrShortThan(100)).WithMessage("Flash card set description can be up to 100 characters");
        }
    }
}

public sealed class CreateFlashCardSetResponse
{
    public int Id { get; set; }
    public string Title { get; set; } = default!;
    public string? Description { get; set; } = default!;
}
