using Dagidirli.Extensions.Microsoft.AspNetCore.Builders;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddFluid();
builder.AddDagidirliModules();

var app = builder.Build();
app.MapDagidirliModules();

app.UseStaticFiles();

app.Run();