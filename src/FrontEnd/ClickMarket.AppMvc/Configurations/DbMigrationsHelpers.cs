using ClickMarket.AppMvc.Data;
using ClickMarket.Data.Context;
using ClickMarket.Data.Configurations;
using Microsoft.EntityFrameworkCore;

namespace ClickMarket.AppMvc.Configurations
{
    public static class DbMigrationsHelpersExtension
    {
        public static void UseDbMigrationHelper(this WebApplication app)
        {
            DbMigrationsHelpers.EnsureSeedData(app).Wait();
        }
    }

    public class DbMigrationsHelpers
    {
        public static async Task EnsureSeedData(WebApplication serviceScope)
        {
            var services = serviceScope.Services.CreateScope().ServiceProvider;
            await EnsureSeedData(services);
        }
        
        public static async Task EnsureSeedData(IServiceProvider serviceProvider)
        {
            try
            {
                await SharedDbMigrationsHelper.EnsureIdentityDatabase<ApplicationDbContext>(
                    serviceProvider, "AppMvc");

                await SharedDbMigrationsHelper.EnsureSharedDatabaseInitialized(
                    serviceProvider, "AppMvc", SeedSharedData);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[AppMvc] Erro geral: {ex.Message}");
                throw;
            }
        }

        private static async Task SeedSharedData(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope();
            
            var identityContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var clickContext = scope.ServiceProvider.GetRequiredService<ClickDbContext>();

            await EnsureSeedProducts(identityContext, clickContext);
        }

        private static async Task EnsureSeedProducts(ApplicationDbContext identityContext, ClickDbContext clickDb)
        {
            try
            {
                Console.WriteLine("[AppMvc] Inserindo dados do AppMvc...");

                // Verifica se já existem dados
                var hasCategories = await clickDb.Categorias.AnyAsync();
                if (hasCategories)
                {
                    Console.WriteLine("[AppMvc] Categorias já existem, pulando inserção.");
                    return;
                }

                // USUÁRIO MVC
                var idUsuario = Guid.NewGuid();
                var user = new Microsoft.AspNetCore.Identity.IdentityUser
                {
                    Id = idUsuario.ToString(),
                    UserName = "mvc@teste.com",
                    NormalizedUserName = "MVC@TESTE.COM",
                    Email = "mvc@teste.com",
                    NormalizedEmail = "MVC@TESTE.COM",
                    AccessFailedCount = 0,
                    LockoutEnabled = false,
                    PasswordHash = "AQAAAAIAAYagAAAAEA8BzmHCVEcOD+VNHR7Z91SjCRm9Zc4yodRPaowNC98ttq1IuwawRlqBzwUPidXCnw==",
                    TwoFactorEnabled = false,
                    ConcurrencyStamp = Guid.NewGuid().ToString(),
                    EmailConfirmed = true,
                    SecurityStamp = Guid.NewGuid().ToString()
                };

                await identityContext.Users.AddAsync(user);
                await identityContext.SaveChangesAsync();
                Console.WriteLine("[AppMvc] Usuário MVC inserido.");

                // VENDEDOR
                var vendedor = new Business.Models.Vendedor() 
                { 
                    Id = idUsuario, 
                    Nome = "Vendedor MVC", 
                    Email = "mvc@teste.com" 
                };
                
                clickDb.Vendedores.Add(vendedor);
                await clickDb.SaveChangesAsync();
                Console.WriteLine("[AppMvc] Vendedor inserido.");

                // CATEGORIAS
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
                Console.WriteLine("[AppMvc] Categorias inseridas.");

                // PRODUTOS
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
                Console.WriteLine("[AppMvc] Produtos inseridos.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[AppMvc] Erro ao inserir dados: {ex.Message}");
                throw;
            }
        }
    }
}