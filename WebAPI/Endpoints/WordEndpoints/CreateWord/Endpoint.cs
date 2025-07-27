using FastEndpoints;
using WebAPI.Data;
using WebAPI.Models._words;

namespace WebAPI.Endpoints.WordEndpoints.CreateWord;

public class Endpoint(ApplicationDbContext context) : Endpoint<CreateWordRequest, CreateWordResponse>
{
    private readonly ApplicationDbContext _context = context;

    public override void Configure()
    {
        Post("");
        Group<WordGroup>();
    }

    public override async Task HandleAsync(CreateWordRequest req, CancellationToken ct)
    {
        var w = new Word { Text = req.Text };
        _context.Words.Add(w);
        await _context.SaveChangesAsync(ct);
        await SendOkAsync(new CreateWordResponse { Id = w.Id, Text = w.Text }, ct);
    }
}