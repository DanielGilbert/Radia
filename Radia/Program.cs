using Radia.Extensions.Microsoft.AspNetCore.Builders;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddFluid();
builder.AddLocalServices();
builder.AddRadiaModules();

var app = builder.Build();
app.MapRadiaModules();

app.UseStaticFiles();

app.Run();