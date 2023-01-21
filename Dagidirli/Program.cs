using Dagidirli.Extensions.Microsoft.AspNetCore.Builders;

var builder = WebApplication.CreateBuilder(args);
builder.AddDagidirliModules();

var app = builder.Build();
app.MapDagidirliModules();

app.Run();