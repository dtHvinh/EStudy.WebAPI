using FastEndpoints;

namespace WebAPI.Endpoints.WordEndpoints;

public class WordGroup : Group
{
    public WordGroup()
    {
        Configure("words", cf =>
        {
        });
    }
}
