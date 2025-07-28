using FastEndpoints;

namespace WebAPI.Endpoints.AIEndpoints;

public class AIGroup : Group
{
    public AIGroup()
    {
        Configure("ai", cf =>
        {
            cf.Description(e => e.WithTags("AI"));
        });
    }
}
