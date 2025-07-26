using FastEndpoints;
using FluentValidation;

namespace WebAPI.Endpoints.AdminEndpoints.UserManagement.BanUser;

public sealed class BanUserRequest
{
    public int UserId { get; set; }
    public int Days { get; set; }
    public string Action { get; set; } = default!;

    public sealed class BanUserRequestValidator : Validator<BanUserRequest>
    {
        public BanUserRequestValidator()
        {
            RuleFor(x => x.UserId).GreaterThan(0).WithMessage("UserId must be greater than 0");
        }
    }
}
