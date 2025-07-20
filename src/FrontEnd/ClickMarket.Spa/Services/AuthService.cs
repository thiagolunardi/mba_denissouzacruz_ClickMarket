using ClickMarket.Spa.Models;
using ClickMarket.Spa.Security;
using Microsoft.AspNetCore.Components;
using System.Security.Claims;
using System.Text.Json;

namespace ClickMarket.Spa.Services;

public class AuthService(AccessTokenService accessTokenService, NavigationManager nav, IHttpClientFactory httpClientFactory, JWTAuthenticationStateProvider authStateProvider)
{
    private readonly HttpClient _httpClient = httpClientFactory.CreateClient("ClickMarketAPI");
    private ClaimsPrincipal _user;

    public async Task<bool> Login(string email, string password)
    {
        var response = await _httpClient.PostAsJsonAsync("conta/login", new { email, password });
        var retornoJson = await response.Content.ReadAsStringAsync();
        try
        {
            var retornoViewModel = JsonSerializer.Deserialize<RetornoViewModel>(retornoJson, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            if (!retornoViewModel.Success)
            {
                return false;
            }
            var token = retornoViewModel.Data.ToString();
            await accessTokenService.AdicionarToken(token);
            return true;
        }
        catch
        {
            return false;
        }
    }

    public async Task<RetornoViewModel> Registrar(string name, string email, string password, string confirmPassword)
    {
        var response = await _httpClient.PostAsJsonAsync("conta/registrar", new { name, email, password, confirmPassword });
        var retornoJson = await response.Content.ReadAsStringAsync();
        try
        {
            var retornoViewModel = JsonSerializer.Deserialize<RetornoViewModel>(retornoJson, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            if (!retornoViewModel.Success)
            {
                return new RetornoViewModel
                {
                    Success = false,
                    Errors = retornoViewModel.Errors ?? ["Erro ao registrar usuário."]
                };
            }

            var token = retornoViewModel.Data.ToString();
            await accessTokenService.AdicionarToken(token);
            return retornoViewModel;

        }
        catch
        {
            return new RetornoViewModel
            {
                Success = false,
                Errors = ["Erro ao processar a resposta do servidor."]
            };
        }
    }

    public async Task<string> ObterUserEmail()
    {
        if (!await IsAuthenticated())
            return null;

        return _user.Claims.FirstOrDefault(c => c.Type == "email")?.Value;
    }

    public async Task<string> ObterUserRole()
    {
        if (!await IsAuthenticated())
            return null;

        return _user.Claims.FirstOrDefault(c => c.Type == "role")?.Value;
    }

    public async Task Logout()
    {
        await accessTokenService.RemoverToken();
        nav.NavigateTo("/", true);
    }

    private async Task<bool> IsAuthenticated()
    {
        var state = await authStateProvider.GetAuthenticationStateAsync();
        _user = state.User;
        return _user.Identity.IsAuthenticated;
    }
}
