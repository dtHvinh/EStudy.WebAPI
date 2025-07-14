using FluentValidation;

namespace WebAPI.Endpoints.StorageEndpoints.DeleteVideo;

public sealed class DeleteVideoRequest
{
    public string VideoUrl { get; set; } = default!;

    public sealed class Validator : FastEndpoints.Validator<DeleteVideoRequest>
    {
        public Validator()
        {
            RuleFor(x => x.VideoUrl).NotEmpty().WithMessage("Video URL cannot be empty");
        }
    }
}
