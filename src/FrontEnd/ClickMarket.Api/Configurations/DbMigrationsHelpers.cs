using ClickMarket.Data.Configurations;

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