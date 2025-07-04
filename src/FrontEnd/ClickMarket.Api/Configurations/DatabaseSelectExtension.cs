using ClickMarket.Api.Context;
using ClickMarket.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace ClickMarket.Api.Configurations
{
    public static class DatabaseSelectExtension
    {
        public static void AddDatabaseSelector(this WebApplicationBuilder builder)
        {
            if (builder.Environment.IsDevelopment())
            {
                var connectionString = builder.Configuration.GetConnectionString("DefaultConnectionLite") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
                builder.Services.AddDbContext<ApiDbContext>(options =>
                options.UseSqlite(connectionString));

                builder.Services.AddDbContext<ClickDbContext>(options =>
                    options.UseSqlite(connectionString));
            }
            else
            {
                var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
                builder.Services.AddDbContext<ApiDbContext>(options =>
                    options.UseSqlServer(connectionString));

                builder.Services.AddDbContext<ClickDbContext>(options =>
                    options.UseSqlServer(connectionString));
            }

        }
    }
}
