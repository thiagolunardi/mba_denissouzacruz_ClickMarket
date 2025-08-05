using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClickMarket.Spa.Models;

namespace ClickMarket.Spa.Services
{
    public class CategoriaService
    {
        private readonly HttpClient _httpClient;
        public CategoriaService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("ClickMarketAPI");
        }

        public async Task<List<CategoriaViewModel>> Listar()
        {
            try
            {
                var categorias = await _httpClient.GetFromJsonAsync<List<CategoriaViewModel>>("categorias");
                return categorias ?? [];
            }
            catch
            {
                return [];
            }
        }
    }
}