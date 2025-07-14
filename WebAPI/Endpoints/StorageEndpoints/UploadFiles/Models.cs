using FluentValidation;

namespace WebAPI.Endpoints.StorageEndpoints.UploadFiles;

public sealed class UploadFilesRequest
{
    public IFormFileCollection Files { get; set; } = default!;

    public sealed class Validator : FastEndpoints.Validator<UploadFilesRequest>
    {
        public Validator()
        {
            RuleFor(x => x.Files).NotEmpty().WithMessage("Files cannot be empty.");
        }
    }
}
