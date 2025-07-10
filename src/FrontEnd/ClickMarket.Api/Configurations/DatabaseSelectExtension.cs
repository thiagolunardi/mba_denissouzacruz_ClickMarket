using ClickMarket.Api.Context;
using ClickMarket.Data.Configurations;
using ClickMarket.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace ClickMarket.Api.Configurations
{
    public static class DatabaseSelectExtension
    {
        public static WebApplicationBuilder AddDatabaseSelector(this WebApplicationBuilder builder)
        {
            return DatabaseSelect.AddDatabaseSelector(builder);
        }
    }
}
