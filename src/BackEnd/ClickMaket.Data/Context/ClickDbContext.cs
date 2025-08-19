using ClickMarket.Business.Models;
using ClickMarket.Data.Mappings;
using Microsoft.EntityFrameworkCore;

namespace ClickMarket.Data.Context
{
    public class ClickDbContext(DbContextOptions<ClickDbContext> options) : DbContext(options)
    {
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Vendedor> Vendedores { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Favorito> Favoritos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ProdutoMapping());
            modelBuilder.ApplyConfiguration(new CategoriaMapping());
            modelBuilder.ApplyConfiguration(new VendedorMapping());

            base.OnModelCreating(modelBuilder);
        }
    }
}
