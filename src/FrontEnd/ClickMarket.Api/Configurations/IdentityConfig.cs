using ClickMarket.Api.Context;
using ClickMarket.Data.Context;
using Microsoft.AspNetCore.Identity;

namespace ClickMarket.Api.Configurations;

public static class IdentityConfig
{
    public static WebApplicationBuilder AddIdentityConfig(this WebApplicationBuilder builder)
    {
        builder.Services.AddIdentity<IdentityUser, IdentityRole>()
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>();

        return builder;
    }
}
