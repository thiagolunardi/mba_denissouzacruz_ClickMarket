using Microsoft.JSInterop;

namespace ClickMarket.Spa.Services;

public class CookieService(IJSRuntime jSRuntime)
{
    public async Task<string> Obter(string chave)
    {
        return await jSRuntime.InvokeAsync<string>("obterCookie", chave);
    }

    public async Task Adicionar(string chave, string valor, int expiraEmDias)
    {
        await jSRuntime.InvokeVoidAsync("adicionarCookie", chave, valor, expiraEmDias);
    }

    public async Task Remover(string chave)
    {
        await jSRuntime.InvokeVoidAsync("removerCookie", chave);
    }
}
