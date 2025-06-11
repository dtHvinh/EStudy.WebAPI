using FastEndpoints;
using FluentValidation;

namespace WebAPI.Endpoints.FlashCardEndpoints.EditCardFromSet;

public sealed class EditCardFromSetRequest
{
    public int CardId { get; set; }
    public int SetId { get; set; }
    public string Term { get; set; } = default!;
    public string Definition { get; set; } = default!;
    public string? PartOfSpeech { get; set; }
    public string? Example { get; set; }
    public string? Note { get; set; }
    public IFormFile? Image { get; set; }

    public class EditCardFromSetRequestValidator : Validator<EditCardFromSetRequest>
    {
        public EditCardFromSetRequestValidator()
        {
            RuleFor(x => x.Term).NotEmpty().WithMessage("Term is required");
            RuleFor(x => x.Definition).NotEmpty().WithMessage("Definition is required");
        }
    }
}

public sealed class EditCardFromSetResponse
{

}