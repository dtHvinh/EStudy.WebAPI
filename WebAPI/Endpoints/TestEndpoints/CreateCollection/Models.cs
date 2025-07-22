using FastEndpoints;
using FluentValidation;

namespace WebAPI.Endpoints.TestEndpoints.CreateCollection;

public sealed class CreateCollectionRequest
{
    public string Name { get; set; } = default!;
    public string? Description { get; set; } = default!;

    public sealed class CreateCollectionRequestValidator : Validator<CreateCollectionRequest>
    {
        public CreateCollectionRequestValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required.");
            RuleFor(x => x.Description).MaximumLength(500).WithMessage("Description cannot exceed 500 characters.");
        }
    }
}

public sealed class CreateCollectionResponse
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public string? Description { get; set; } = default!;
}