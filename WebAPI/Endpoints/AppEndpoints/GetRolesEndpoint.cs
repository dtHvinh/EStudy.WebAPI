using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data;

namespace WebAPI.Endpoints.AppEndpoints;

public sealed class GetRolesResponse
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
}

public class GetRolesEndpoint(ApplicationDbContext context) : EndpointWithoutRequest<List<GetRolesResponse>>
{
    private readonly ApplicationDbContext _context = context;

    public override void Configure()
    {
        Get("roles");
        AllowAnonymous();
        Throttle(20, 60);
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var roles = await _context.Roles
            .Select(role => new GetRolesResponse
            {
                Id = role.Id,
                Name = role.Name!
            })
            .ToListAsync(ct);

        await SendOkAsync(roles, ct);
    }
}
