using ClickMarket.AppMvc.Data;
using ClickMarket.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace ClickMarket.AppMvc.Configurations
{
    public static class DatabaseSelectExtension
    {
        public static void AddDatabaseSelector(this WebApplicationBuilder builder)
        {
            if (builder.Environment.IsDevelopment())
            {
                var connectionString = builder.Configuration.GetConnectionString("DefaultConnectionLite") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
                builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlite(connectionString));

                builder.Services.AddDbContext<ClickDbContext>(options =>
                    options.UseSqlite(connectionString));
            }
            else
            {
                var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
                builder.Services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseSqlServer(connectionString));

                builder.Services.AddDbContext<ClickDbContext>(options =>
                    options.UseSqlServer(connectionString));
            }

        }
    }
}
