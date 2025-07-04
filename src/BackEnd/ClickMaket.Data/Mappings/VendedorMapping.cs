using ClickMarket.Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClickMarket.Data.Mappings
{
    public class VendedorMapping: IEntityTypeConfiguration<Vendedor>
    {
        public void Configure(EntityTypeBuilder<Vendedor> builder)
        {
            builder.ToTable("Vendedores")
                .HasKey("Id");

            builder.Property(p => p.Nome).HasColumnType("varchar(100)").IsRequired();
            builder.Property(p => p.Email).HasColumnType("varchar(100)").IsRequired();

            builder
                .HasMany(v => v.Produtos)
                .WithOne(p => p.Vendedor);
        }
    }
}
