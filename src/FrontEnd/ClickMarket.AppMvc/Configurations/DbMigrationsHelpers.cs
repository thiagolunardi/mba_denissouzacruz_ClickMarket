using ClickMarket.Data.Configurations;

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