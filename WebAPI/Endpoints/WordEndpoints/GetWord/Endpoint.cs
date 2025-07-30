using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.Utilities.Extensions;

namespace WebAPI.Endpoints.WordEndpoints.GetWord;

public sealed class GetWordsEndpoint(ApplicationDbContext context) : Endpoint<GetWordRequest, List<GetWordResponse>>
{
    private readonly ApplicationDbContext _context = context;

    public override void Configure()
    {
        Get("");
        Group<WordGroup>();
        AllowAnonymous();
    }

    public override async Task HandleAsync(GetWordRequest req, CancellationToken ct)
    {
        var q = _context.Words
            .OrderBy(w => w.Id).AsQueryable();

        if (!string.IsNullOrWhiteSpace(req.Name))
            q = q.Where(w => w.Text.Contains(req.Name.ToLower()));

        var words = await q
            .Select(w => new GetWordResponse
            {
                Id = w.Id,
                Text = w.Text,
            })
            .Paginate(req.Page, req.PageSize)
            .ToListAsync(ct);

        await SendOkAsync(words, ct);
    }
}
