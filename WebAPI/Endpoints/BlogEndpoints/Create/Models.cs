using FastEndpoints;
using FluentValidation;

namespace WebAPI.Endpoints.BlogEndpoints.Create;

public sealed class CreateBlogRequest
{
    public string Title { get; set; } = default!;
    public string Content { get; set; } = default!;

    public sealed class Validator : Validator<CreateBlogRequest>
    {
        public Validator()
        {
            RuleFor(x => x.Title).NotEmpty().WithMessage("Title is required");
            RuleFor(x => x.Content).NotNull().WithMessage("Content is required");
        }
    }
}

public sealed class CreateBlogResponse
{
    public int Id { get; set; }
}
