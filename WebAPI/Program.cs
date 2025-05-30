using FastEndpoints;
using WebAPI.Utilities.Extensions;

var builder = WebApplication.CreateBuilder(args);

SeptupExtensions.Config = builder.Configuration;

builder.Services.AddOpenApi();
builder.Services.AddFastEndpoints();

builder.Services.ConfigureDatabase();
builder.Services.ConfigureJwtAuthentication();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.Run();
