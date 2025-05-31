using FastEndpoints;

namespace WebAPI.Endpoints.BlogEndpoints;

public class BlogGroup : Group
{
    public BlogGroup()
    {
        Configure("blogs", cf =>
        {
        });
    }
}
