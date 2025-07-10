using ClickMarket.Business.Interfaces;
using ClickMarket.Data.Repository;

namespace ClickMarket.AppMvc.Configurations
{
    public static class DependencyInjectionConfig
    {
        public static WebApplicationBuilder RegisterServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<ICategoriaRepository, CategoriaRepository>();
            builder.Services.AddScoped<IProdutoRepository, ProdutoRepository>();
            builder.Services.AddScoped<IVendedorRepository, VendedorRepository>();

            return builder;
        }
    }
}
