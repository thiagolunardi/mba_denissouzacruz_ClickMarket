using ClickMarket.Spa.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ClickMarket.Spa.Services;

public class ProdutoService(AccessTokenService accessTokenService, IHttpClientFactory httpClientFactory, IWebHostEnvironment webHostEnvironment)
{
    private readonly IWebHostEnvironment _env = webHostEnvironment;
    private readonly HttpClient _httpClient = httpClientFactory.CreateClient("ClickMarketAPI");

    public async Task<List<ProdutoListViewModel>> ObterProdutos()
    {
        try
        {
            var token = await accessTokenService.ObterToken();
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            var produtos = await _httpClient.GetFromJsonAsync<List<ProdutoListViewModel>>("produtos");
            return produtos ?? [];
        }
        catch
        {
            return [];
        }
    }
    public string ObterImagem(string imagem)
    {
        var path = Path.Combine(_env.WebRootPath, @"imagens");

        var fileExists = Path.Exists($"{path}\\{imagem}");

        if (!fileExists)
        {
            return @"imagens/" + Path.GetFileName($"{path}\\no-image.png");
        }

        return @"imagens/" + Path.GetFileName($"{path}\\{imagem}");
    }
}
