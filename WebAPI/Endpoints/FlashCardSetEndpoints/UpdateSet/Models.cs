using FastEndpoints;
using FluentValidation;

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
                .MaximumLength(50).WithMessage("Name is too long");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Please enter description")
                .MaximumLength(500).WithMessage("Description is too long");
        }
    }
}