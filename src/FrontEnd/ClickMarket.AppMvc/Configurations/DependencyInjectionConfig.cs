using ClickMarket.AppMvc.Extensions;
using ClickMarket.Business.Extensions;
using ClickMarket.Business.Interfaces;
using ClickMarket.Business.Services;
using ClickMarket.Data.Repository;
using Microsoft.AspNetCore.Mvc.DataAnnotations;

namespace ClickMarket.AppMvc.Configurations
{
    public static class DependencyInjectionConfig
    {
        public static WebApplicationBuilder RegisterServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<ICategoriaRepository, CategoriaRepository>();
            builder.Services.AddScoped<IProdutoRepository, ProdutoRepository>();
            builder.Services.AddScoped<IVendedorRepository, VendedorRepository>();

            builder.Services.AddScoped<IVendedorService, VendedorService>();
            builder.Services.AddScoped<IUser, AspNetUser>();

            builder.Services.AddSingleton<IValidationAttributeAdapterProvider, MoedaValidationAttributeAdapterProvider>();
            return builder;
        }
    }
}
