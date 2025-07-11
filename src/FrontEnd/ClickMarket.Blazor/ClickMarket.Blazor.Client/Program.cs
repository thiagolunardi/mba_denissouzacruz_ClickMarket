using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

// Configura HttpClient apontando para ClickMarket.Api
builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri("https://localhost:7251/") // ajuste conforme porta da API
});

var host = builder.Build();
await host.RunAsync();
