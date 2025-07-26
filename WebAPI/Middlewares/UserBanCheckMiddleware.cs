using WebAPI.Constants;
using WebAPI.Middlewares.Contract;
using WebAPI.Services.Contract;
using WebAPI.Utilities.Extensions;

namespace WebAPI.Middlewares;

public class UserBanCheckMiddleware(RequestDelegate next,
                                    Serilog.ILogger logger,
                                    IBanService banService) : IApplicationMiddleware
{
    private readonly RequestDelegate _next = next;
    private readonly Serilog.ILogger _logger = logger;
    private readonly IBanService _banService = banService;

    public async Task InvokeAsync(HttpContext context)
    {
        var userId = context.GetId();
        if (userId is not null)
        {
            if (await _banService.IsUserBannedAsync(userId))
            {
                _logger.Warning("User is banned. Request denied.");
                context.Response.StatusCode = StatusCodesV2.Status701AccountBanned;
                await context.Response.WriteAsync("You are banned from accessing this resource.");
                return;
            }
        }
        _logger.Information("User not ban");
        await _next(context);
    }
}

