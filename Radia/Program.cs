using Microsoft.Extensions.Primitives;
using Radia.Extensions.Microsoft.AspNetCore.Builders;

var builder = WebApplication.CreateBuilder(args);
builder.WebHost.ConfigureKestrel(serverOptions =>
{
    serverOptions.AllowSynchronousIO = true;
});
builder.Services.AddMemoryCache();
builder.Services.AddHttpContextAccessor();
builder.AddLocalServices();
builder.AddRadiaModules();

var app = builder.Build();
app.MapRadiaModules();

app.Use(async (context, next) =>
{
    context.Response.OnStarting(() =>
    {
        context.Response.Headers.Add("X-Clacks-Overhead", new StringValues(new string[] { "GNU Terry Pratchett", "GNU T. Gilbert", "GNU P. Gilbert", "GNU B. Gilbert", "GNU U. Gilbert" }));
        context.Response.Headers.Add("X-Clacks-Overhead-Note", new StringValues(new string[] { "As long as it clacks, no one will be forgotten." }));
        return Task.FromResult(0);
    });
    await next();
});

app.Run();