using Microsoft.AspNetCore.Components;

namespace ClickMarket.Blazor.Services;

public class AuthService(AccessTokenService accessTokenService, NavigationManager navigationManager, IHttpClientFactory httpClientFactory)
{
    private readonly HttpClient _httpClient = httpClientFactory.CreateClient("ClickMarketAPI");

    public async Task<bool> Login(string email, string senha)
    {
        var response = await _httpClient.PostAsJsonAsync("api/conta/login", new { email, senha });
        if (response.IsSuccessStatusCode)
        {
            var token = await response.Content.ReadAsStringAsync();
            await accessTokenService.AdicionarToken(token);
            return true;
        }
        return false;
    }
}
