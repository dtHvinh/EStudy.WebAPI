using FastEndpoints;
using FluentValidation;

namespace WebAPI.Endpoints.FlashCardSetEndpoints.UpdateSetName;

public sealed class UpdateSetNameRequest
{
    public int Id { get; set; }
    public string Title { get; set; } = default!;

    public sealed class Validator : Validator<UpdateSetNameRequest>
    {
        public Validator()
        {
            RuleFor(x => x.Title).NotEmpty().WithMessage("Please enter name");
        }
    }
}