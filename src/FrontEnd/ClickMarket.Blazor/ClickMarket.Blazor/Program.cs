using ClickMarket.Blazor.Components;
using ClickMarket.Blazor.Security;
using ClickMarket.Blazor.Services;
using Microsoft.AspNetCore.Components.Authorization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

builder.Services.AddScoped<CookieService>();
builder.Services.AddScoped<AccessTokenService>();
builder.Services.AddScoped<AuthService>();
builder.Services.AddHttpClient("ClickMarketAPI", client =>
{
    client.BaseAddress = new Uri("https://localhost:7251/api/");
});

builder.Services.AddAuthorization();
builder.Services.AddAuthentication()
    .AddScheme<CustomOptions, JWTAuthenticationHandler>(
        "JWTAuth", options => { }
    );
builder.Services.AddScoped<JWTAuthenticationStateProvider>();
builder.Services.AddScoped<AuthenticationStateProvider, JWTAuthenticationStateProvider>();
builder.Services.AddCascadingAuthenticationState();
//// Registra HttpClient para ser injetado nos componentes (pré-render e interativo)
//builder.Services.AddHttpClient("Api", client =>
//{
//    client.BaseAddress = new Uri("https://localhost:7251/");
//});
// Serviço padrão sem nome
builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("Api"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(ClickMarket.Blazor.Client._Imports).Assembly);

app.Run();
