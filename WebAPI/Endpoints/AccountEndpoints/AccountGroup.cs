using FastEndpoints;

namespace WebAPI.Endpoints.AccountEndpoints;

public sealed class AccountGroup : Group
{
    public AccountGroup()
    {
        Configure("account", ep =>
        {
            ep.Description(d => d.WithTags("Account"));
        });
    }
}