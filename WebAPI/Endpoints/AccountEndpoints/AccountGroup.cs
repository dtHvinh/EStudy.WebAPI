using FastEndpoints;

namespace WebAPI.Endpoints.AccountEndpoints;

public class AccountGroup : Group
{
    public AccountGroup()
    {
        Configure("account", cf =>
        {
        });
    }
}