using WebSearchMCPServer.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services
    .AddMcpServer()
    .WithHttpTransport()
    .WithToolsFromAssembly();

builder.Services.AddSingleton<SerpApiService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapMcp();

app.Run();

