using FastEndpoints;

namespace WebAPI.Endpoints.CommentEndpoints;

public class CommentGroup : Group
{
    public CommentGroup()
    {
        Configure("comments", ep =>
        {
            ep.Description(d => d.WithTags("Comment"));
        });
    }
}
