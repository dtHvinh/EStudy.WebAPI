using FastEndpoints;

namespace WebAPI.Endpoints.AdminEndpoints;

public class AdminGroup : Group
{
    public AdminGroup()
    {
        Configure("admin", cf =>
        {
        });
    }
}
