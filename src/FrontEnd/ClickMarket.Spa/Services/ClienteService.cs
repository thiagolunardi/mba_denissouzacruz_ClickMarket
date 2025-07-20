using ClickMarket.Spa.Models;
using System.Text.Json;

namespace ClickMarket.Spa.Services;

public class ClienteService(AccessTokenService accessTokenService, IHttpClientFactory httpClientFactory, IWebHostEnvironment webHostEnvironment)
{
    private readonly IWebHostEnvironment _env = webHostEnvironment;
    private readonly HttpClient _httpClient = httpClientFactory.CreateClient("ClickMarketAPI");

    public async Task<RetornoViewModel> SalvarNaLista(Guid produtoId)
    {
        var token = await accessTokenService.ObterToken();

        _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

        var response = await _httpClient.PostAsJsonAsync($"clientes/favoritos/{produtoId}", new { });
        var retornoJson = await response.Content.ReadAsStringAsync();
        try
        {
            var retornoViewModel = JsonSerializer.Deserialize<RetornoViewModel>(retornoJson, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            if (!retornoViewModel.Success)
            {
                return new RetornoViewModel
                {
                    Success = false,
                    Errors = retornoViewModel.Errors ?? ["Erro ao registrar o produto como favorito."]
                };
            }

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

    public async Task<RetornoViewModel> RemoverDaLista(Guid produtoId)
    {
        var token = await accessTokenService.ObterToken();
        _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

        var response = await _httpClient.DeleteAsync($"clientes/favoritos/{produtoId}");
        var retornoJson = await response.Content.ReadAsStringAsync();
        try
        {
            var retornoViewModel = JsonSerializer.Deserialize<RetornoViewModel>(retornoJson, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            if (!retornoViewModel.Success)
            {
                return new RetornoViewModel
                {
                    Success = false,
                    Errors = retornoViewModel.Errors ?? ["Erro ao remover o produto da lista de favoritos."]
                };
            }

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

}
