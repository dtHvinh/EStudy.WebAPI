using FastEndpoints;
using FluentValidation;

namespace WebAPI.Endpoints.StorageEndpoints.UploadVideo;

public sealed class UploadVideoRequest
{
    public required IFormFile File { get; set; }

    public sealed class Validator : Validator<UploadVideoRequest>
    {
        public Validator()
        {
            RuleFor(e => e.File).NotNull().WithMessage("File is required.");
        }
    }
}

public sealed class UploadVideoResponse
{
    public required string VideoUrl { get; set; }
}