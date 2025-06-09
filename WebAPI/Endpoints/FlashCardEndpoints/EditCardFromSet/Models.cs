using FastEndpoints;
using FluentValidation;

namespace WebAPI.Endpoints.FlashCardEndpoints.EditCardFromSet;

public sealed class EditCardFromSetRequest
{
    public int CardId { get; set; }
    public int SetId { get; set; }
    public string Term { get; set; } = default!;
    public string Definition { get; set; } = default!;

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