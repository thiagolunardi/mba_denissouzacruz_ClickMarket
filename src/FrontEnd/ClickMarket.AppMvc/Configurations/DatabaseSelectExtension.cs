using ClickMarket.Data.Configurations;

namespace ClickMarket.AppMvc.Configurations
{
    public static class DatabaseSelectExtension
    {
        public static WebApplicationBuilder AddDatabaseSelector(this WebApplicationBuilder builder)
        {
            return DatabaseSelect.AddDatabaseSelector(builder);
        }
    }
}
