namespace ClickMarket.Spa.Services;

public class AccessTokenService(CookieService cookieService)
{
    private readonly string tokenKey = "access_token";

    public async Task<string> ObterToken()
    {
        return await cookieService.Obter(tokenKey);
    }

    public async Task AdicionarToken(string token)
    {
        await cookieService.Adicionar(tokenKey, token, 1);
    }

    public async Task RemoverToken()
    {
        await cookieService.Remover(tokenKey);
    }
}
