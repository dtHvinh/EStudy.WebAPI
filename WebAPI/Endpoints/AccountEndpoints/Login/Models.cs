using FastEndpoints;
using FluentValidation;

namespace WebAPI.Endpoints.AccountEndpoints.Login;

public sealed class LoginRequest
{
    public string UserNameOrEmail { get; set; } = default!;
    public string Password { get; set; } = default!;

    public sealed class LoginRequestValidator : Validator<LoginRequest>
    {
        public LoginRequestValidator()
        {
            RuleFor(x => x.UserNameOrEmail)
                .NotEmpty().WithMessage("Username or email is required");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required");
        }
    }
}

public sealed record LoginResponse(string AccessToken);