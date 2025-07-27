using FastEndpoints;

namespace WebAPI.Endpoints.UserEndpoints;

public class UserGroup : Group
{
    public UserGroup()
    {
        {
            Configure("user", cf =>
            {
                cf.Description(d => d.WithTags("User"));
            });
        }
    }
}
