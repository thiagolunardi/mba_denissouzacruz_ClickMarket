using ClickMarket.AppMvc.Data;
using ClickMarket.Data.Context;
using ClickMarket.Data.Configurations;
using Microsoft.EntityFrameworkCore;

namespace ClickMarket.AppMvc.Configurations
{
    public static class DbMigrationsHelpersExtension
    {
        public static void UseDbMigrationHelper(this WebApplication app)
        {
            DbMigrationsHelpers.EnsureSeedData(app).Wait();
        }
    }
}