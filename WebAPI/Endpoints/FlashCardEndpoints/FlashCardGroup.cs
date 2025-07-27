using FastEndpoints;

namespace WebAPI.Endpoints.FlashCardEndpoints;

public class FlashCardGroup : Group
{
    public FlashCardGroup()
    {
        {
            Configure("flash-cards", cf =>
            {
                cf.Description(d => d.WithTags("Flash card"));
            });
        }
    }
}