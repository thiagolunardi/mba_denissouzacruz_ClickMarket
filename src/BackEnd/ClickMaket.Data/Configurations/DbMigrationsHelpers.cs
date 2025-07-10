using ClickMarket.Data.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ClickMarket.Data.Configurations
{
    public static class DbMigrationsHelpers
    {
        public static async Task EnsureSeedData(WebApplication serviceScope)
        {
            var services = serviceScope.Services.CreateScope().ServiceProvider;
            await EnsureSeedData(services);
        }

        public static async Task EnsureSeedData(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope();
            var env = scope.ServiceProvider.GetRequiredService<IWebHostEnvironment>();

            var contextID = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var contextClick = scope.ServiceProvider.GetRequiredService<ClickDbContext>();

            if (env.IsDevelopment())
            {

                await contextID.Database.MigrateAsync();
                await contextClick.Database.MigrateAsync();

                await EnsureSeedProducts(contextID, contextClick);
            }
        }

        private static async Task EnsureSeedProducts(ApplicationDbContext identityDb, ClickDbContext clickDb)
        {
            //Realiza a carga inicial dos dados
            if (clickDb.Categorias.Any())
                return;

            var idUsuario = Guid.NewGuid();
            await identityDb.Users.AddAsync(new Microsoft.AspNetCore.Identity.IdentityUser
            {
                Id = idUsuario.ToString(),
                UserName = "teste@teste.com",
                NormalizedUserName = "TESTE@TESTE.COM",
                Email = "teste@teste.com",
                NormalizedEmail = "TESTE@TESTE.COM",
                AccessFailedCount = 0,
                LockoutEnabled = false,
                PasswordHash = "AQAAAAIAAYagAAAAEA8BzmHCVEcOD+VNHR7Z91SjCRm9Zc4yodRPaowNC98ttq1IuwawRlqBzwUPidXCnw==",
                TwoFactorEnabled = false,
                ConcurrencyStamp = Guid.NewGuid().ToString(),
                EmailConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString()
            });

            await identityDb.SaveChangesAsync();

            clickDb.Vendedores.Add(new Business.Models.Vendedor() { Id = idUsuario, Nome = "Teste", Email = "teste@teste.com" });
            await clickDb.SaveChangesAsync();

            var categoriaPapelaria = Guid.NewGuid();
            var categoriaInformatica = Guid.NewGuid();

            clickDb.Categorias.AddRange(
                new Business.Models.Categoria()
                {
                    Id = categoriaPapelaria,
                    Nome = "Papelaria",
                    Descricao = "Materiais Acadêmicos"
                },
                new Business.Models.Categoria()
                {
                    Id = categoriaInformatica,
                    Nome = "Informática",
                    Descricao = "Artigos eletrônicos e informática em geral"
                }
            );
            await clickDb.SaveChangesAsync();

            if (clickDb.Produtos.Any())
                return;

            clickDb.Produtos.AddRange(
                new Business.Models.Produto()
                {
                    Id = Guid.NewGuid(),
                    Nome = "Livro A Cabana",
                    Descricao = "Livro A Cabana / Literatura Estrangeira",
                    CategoriaId = categoriaPapelaria,
                    VendedorId = idUsuario,
                    Imagem = "5be38508-8799-4625-8c68-77100b36c7fc-Acabana.jpg",
                    Valor = 30,
                    QuantidadeEstoque = 245
                },
                new Business.Models.Produto()
                {
                    Id = Guid.NewGuid(),
                    Nome = "Teclado Logi",
                    Descricao = "Teclado Logi, sem bluetooth",
                    CategoriaId = categoriaInformatica,
                    VendedorId = idUsuario,
                    Imagem = "6afb183a-744b-4f99-b394-6180f9d3b1dd-teclado_logi.png",
                    Valor = 122,
                    QuantidadeEstoque = 198
                }
            );

            await clickDb.SaveChangesAsync();
        }
    }

}
