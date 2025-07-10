using ClickMarket.Api.Context;
using ClickMarket.Data.Context;
using ClickMarket.Data.Configurations;
using Microsoft.EntityFrameworkCore;

namespace ClickMarket.Api.Configurations
{
    public static class DbMigrationsHelpersExtension
    {
        public static void UseDbMigrationHelper(this WebApplication app)
        {
            DbMigrationsHelpers.EnsureSeedData(app).Wait();
        }
    }
}