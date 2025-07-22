using ClickMarket.Api.Extensions;
using ClickMarket.Data.Context;
using Microsoft.AspNetCore.Identity;

namespace ClickMarket.Api.Configurations;

public static class IdentityConfig
{
    public static WebApplicationBuilder AddIdentityConfig(this WebApplicationBuilder builder)
    {
        builder.Services.AddIdentity<IdentityUser, IdentityRole>()
            .AddRoles<IdentityRole>()
            .AddErrorDescriber<IdentityMensagensPortugues>()
            .AddEntityFrameworkStores<ApplicationDbContext>();

        return builder;
    }
}
