using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace ClickMarket.Data.Context
{
    public class ClickDbContextFactory : IDesignTimeDbContextFactory<ClickDbContext>
    {
        public ClickDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ClickDbContext>();

            optionsBuilder.UseSqlite("Data Source=BancoAutomatico.Db");

            return new ClickDbContext(optionsBuilder.Options);
        }
    }
}