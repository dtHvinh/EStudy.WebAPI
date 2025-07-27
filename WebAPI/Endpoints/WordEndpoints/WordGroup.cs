using FastEndpoints;

namespace WebAPI.Endpoints.WordEndpoints;

public class WordGroup : Group
{
    public WordGroup()
    {
        Configure("words", cf =>
        {
            cf.Description(d => d.WithTags("Word"));
        });
    }
}
