using FastEndpoints;
using FluentValidation;
using WebAPI.Utilities.Extensions;

namespace WebAPI.Endpoints.FlashCardSetEndpoints.UpdateSet;

public sealed class UpdateSetRequest
{
    public int Id { get; set; }
    public string Title { get; set; } = default!;
    public string Description { get; set; } = default!;

    public sealed class Validator : Validator<UpdateSetRequest>
    {
        public Validator()
        {
            RuleFor(x => x.Title).NotEmpty().WithMessage("Please enter name")
                .MaximumLength(12).WithMessage("Name is too long (12 characters max)");
            RuleFor(x => x.Description).Must(e => e.NothingOrShortThan(50)).WithMessage("Description is too long");
        }
    }
}