using FastEndpoints;

namespace WebAPI.Endpoints.FlashCardEndpoints;

public class FlashCardGroup : Group
{
    public FlashCardGroup()
    {
        {
            Configure("flash-cards", cf =>
            {
            });
        }
    }
}