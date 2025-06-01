using FastEndpoints;

namespace WebAPI.Endpoints.FlashCardSetEndpoints;

public class FlashCardSetGroup : Group
{
    public FlashCardSetGroup()
    {
        {
            Configure("flash-card-sets", cf =>
            {
            });
        }
    }
}