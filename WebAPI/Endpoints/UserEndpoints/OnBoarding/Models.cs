using FastEndpoints;
using FluentValidation;

namespace WebAPI.Endpoints.UserEndpoints.OnBoarding;

public sealed class OnBoardingRequest
{
    public string Role { get; set; } = default!;
    public string Discovery { get; set; } = default!;
    public string UseCase { get; set; } = default!;

    public class Validator : Validator<OnBoardingRequest>
    {
        public Validator()
        {
            RuleFor(x => x.Role).NotEmpty().WithMessage("Role is required");
        }
    }
}