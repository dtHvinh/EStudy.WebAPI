using FastEndpoints;

namespace WebAPI.Endpoints.AccountEndpoints;

public sealed class AccountGroup : Group
{
    public AccountGroup()
    {
        Configure("account", ep =>
        {
        });
    }
}