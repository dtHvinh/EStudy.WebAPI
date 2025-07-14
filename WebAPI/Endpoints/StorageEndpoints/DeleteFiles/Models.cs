using FluentValidation;

namespace WebAPI.Endpoints.StorageEndpoints.DeleteFiles;

public sealed class DeleteFilesRequest
{
    public string[] FilePaths { get; set; } = default!;
    public sealed class Validator : FastEndpoints.Validator<DeleteFilesRequest>
    {
        public Validator()
        {
            RuleFor(x => x.FilePaths).NotEmpty().WithMessage("File URLs cannot be empty.");
        }
    }
}
