using FastEndpoints;
using FluentValidation;

namespace WebAPI.Endpoints.AccountEndpoints.Register;


public sealed class RegisterRequest
{
    public string Name { get; set; } = default!;
    public string UserName { get; set; } = default!;
    public string Password { get; set; } = default!;

    public sealed class RegisterRequestValidator : Validator<RegisterRequest>
    {
        public RegisterRequestValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required");

            RuleFor(x => x.UserName)
                .NotEmpty().WithMessage("Must not leave empty")
                .Must(x => !x.Contains(' ')).WithMessage("The user name must not contain spaces");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required");
        }
    }
}

public sealed record RegisterResponse(string AccessToken, string RefreshToken);