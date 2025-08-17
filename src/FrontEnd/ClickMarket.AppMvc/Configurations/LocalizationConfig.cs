using ClickMarket.AppMvc.Adapters;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.DataAnnotations;
using Microsoft.Extensions.Localization;
using System.Globalization;

namespace ClickMarket.AppMvc.Configurations;

/// <summary>
/// Classe responsável por configurar a localização (localization) da aplicação.
/// </summary>
public static class LocalizationConfig
{
    public static void AddLocalizationConfig(this WebApplicationBuilder builder)
    {
        builder.Services.AddSingleton<IStringLocalizerFactory, ResourceManagerStringLocalizerFactory>();
        builder.Services.AddSingleton<IValidationAttributeAdapterProvider, CustomValidationAttributeAdapterProvider>();
    }

    /// <summary>
    /// Configura a localização da aplicação, definindo as culturas suportadas e a cultura padrão.
    /// </summary>
    /// <param name="app">Instância do WebApplicationBuilder utilizada para configurar os serviços da aplicação.</param>
    /// <exception cref="ArgumentNullException">Lançada se o parâmetro <paramref name="app"/> for nulo.</exception>
    public static void UseLocalizationConfig(this IApplicationBuilder app)
    {
        ArgumentNullException.ThrowIfNull(app);

        // Define as culturas suportadas pela aplicação.
        var culturasSuportadas = new[]
        {
            new CultureInfo("pt-BR")
        };

        // Configura as opções de localização.
        app.UseRequestLocalization(new RequestLocalizationOptions
        {
            DefaultRequestCulture = new RequestCulture("pt-BR"),
            SupportedCultures = culturasSuportadas,
            SupportedUICultures = culturasSuportadas
        });
    }

    public static WebApplication UseGlobalizationConfig(this WebApplication app)
    {
        ArgumentNullException.ThrowIfNull(app);

        var defaultCulture = new CultureInfo("pt-BR");

        // Configura as opções de localização.
        var useLocalizationOptions = (new RequestLocalizationOptions
        {
            DefaultRequestCulture = new RequestCulture(defaultCulture),
            SupportedCultures = new List<CultureInfo> { defaultCulture },
            SupportedUICultures = new List<CultureInfo> { defaultCulture }
        });
        app.UseRequestLocalization(useLocalizationOptions);
        return app;
    }
}