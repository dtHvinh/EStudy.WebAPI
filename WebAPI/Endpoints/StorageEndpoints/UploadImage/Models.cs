using FluentValidation;

namespace WebAPI.Endpoints.StorageEndpoints.UploadImage;

public sealed class UploadImageRequest
{
    public IFormFileCollection Files { get; set; } = default!;

    public sealed class Validator : FastEndpoints.Validator<UploadImageRequest>
    {
        public Validator()
        {
            RuleFor(x => x.Files).NotEmpty().WithMessage("Files cannot be empty.");
        }
    }
}
