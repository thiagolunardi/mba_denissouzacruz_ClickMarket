using ClickMarket.Data.Mappings;
using ClickMarket.Business.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClickMarket.Data.Context
{
    public class ClickDbContext : IdentityDbContext
    {
        public ClickDbContext(DbContextOptions<ClickDbContext> options): base(options)
        {

        }

        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Vendedor> Vendedores { get; set; }
        public DbSet<Categoria> Categorias { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ProdutoMapping());
            modelBuilder.ApplyConfiguration(new CategoriaMapping());
            modelBuilder.ApplyConfiguration(new VendedorMapping());
            base.OnModelCreating(modelBuilder);
        }
    }
}
