using ClickMarket.Api.Extensions;
using ClickMarket.Business.Extensions;
using ClickMarket.Business.Interfaces;
using ClickMarket.Business.Notificacoes;
using ClickMarket.Business.Services;
using ClickMarket.Data.Repository;

namespace ClickMarket.Api.Configurations;

public static class DependencyInjectionConfig
{
    public static WebApplicationBuilder RegisterServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<ICategoriaRepository, CategoriaRepository>();
        builder.Services.AddScoped<IProdutoRepository, ProdutoRepository>();
        builder.Services.AddScoped<IVendedorRepository, VendedorRepository>();
        builder.Services.AddScoped<IClienteRepository, ClienteRepository>();
        builder.Services.AddScoped<IFavoritoRepository, FavoritoRepository>();

        builder.Services.AddScoped<INotificador, Notificador>();

        builder.Services.AddScoped<ICategoriaService, CategoriaService>();
        builder.Services.AddScoped<IProdutoService, ProdutoService>();
        builder.Services.AddScoped<IVendedorService, VendedorService>();
        builder.Services.AddScoped<IClienteService, ClienteService>();

        builder.Services.AddHttpContextAccessor();
        builder.Services.AddScoped<IUser, AspNetUser>();

        return builder;
    }
}
