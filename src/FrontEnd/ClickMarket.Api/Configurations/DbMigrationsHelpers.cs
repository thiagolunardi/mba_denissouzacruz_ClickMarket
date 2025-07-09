using ClickMarket.Api.Context;
using ClickMarket.Data.Context;
using ClickMarket.Data.Configurations;
using Microsoft.EntityFrameworkCore;

namespace ClickMarket.Api.Configurations
{
    public static class DbMigrationsHelpers
    {
        public static void UseDbMigrationHelper(this WebApplication app)
        {
            DbMigrationsHelpers.EnsureSeedData(app).Wait();
        }

        public static async Task EnsureSeedData(WebApplication serviceScope)
        {
            var services = serviceScope.Services.CreateScope().ServiceProvider;
            await EnsureSeedData(services);
        }

        public static async Task EnsureSeedData(IServiceProvider serviceProvider)
        {
            try
            {
                await SharedDbMigrationsHelper.EnsureIdentityDatabase<ApiDbContext>(
                    serviceProvider, "Api");

                await SharedDbMigrationsHelper.EnsureSharedDatabaseInitialized(
                    serviceProvider, "Api", SeedApiData);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Api] Erro geral: {ex.Message}");
                throw;
            }
        }

        private static async Task SeedApiData(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope();
            
            var identityContext = scope.ServiceProvider.GetRequiredService<ApiDbContext>();
            var clickContext = scope.ServiceProvider.GetRequiredService<ClickDbContext>();

            var hasData = await clickContext.Categorias.AnyAsync();
            if (hasData)
            {
                Console.WriteLine("[Api] Dados já inseridos pelo AppMvc, criando apenas usuário da API...");
                await EnsureApiUser(identityContext);
                return;
            }

            await EnsureSeedProducts(identityContext, clickContext);
        }

        private static async Task EnsureApiUser(ApiDbContext identityContext)
        {
            try
            {
                var hasApiUser = await identityContext.Users.AnyAsync(u => u.Email == "api@teste.com");
                if (hasApiUser)
                {
                    Console.WriteLine("[Api] Usuário da API já existe.");
                    return;
                }

                var idUsuario = Guid.NewGuid();
                await identityContext.Users.AddAsync(new Microsoft.AspNetCore.Identity.IdentityUser
                {
                    Id = idUsuario.ToString(),
                    UserName = "api@teste.com",
                    NormalizedUserName = "API@TESTE.COM",
                    Email = "api@teste.com",
                    NormalizedEmail = "API@TESTE.COM",
                    AccessFailedCount = 0,
                    LockoutEnabled = false,
                    PasswordHash = "AQAAAAIAAYagAAAAEA8BzmHCVEcOD+VNHR7Z91SjCRm9Zc4yodRPaowNC98ttq1IuwawRlqBzwUPidXCnw==",
                    TwoFactorEnabled = false,
                    ConcurrencyStamp = Guid.NewGuid().ToString(),
                    EmailConfirmed = true,
                    SecurityStamp = Guid.NewGuid().ToString()
                });

                await identityContext.SaveChangesAsync();
                Console.WriteLine("[Api] Usuário da API criado.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Api] Erro ao criar usuário da API: {ex.Message}");
            }
        }

        private static async Task EnsureSeedProducts(ApiDbContext identityContext, ClickDbContext clickDb)
        {
            try
            {
                Console.WriteLine("[Api] A API está inserindo os dados iniciais...");

                var idUsuario = Guid.NewGuid();
                await identityContext.Users.AddAsync(new Microsoft.AspNetCore.Identity.IdentityUser
                {
                    Id = idUsuario.ToString(),
                    UserName = "api@teste.com",
                    NormalizedUserName = "API@TESTE.COM",
                    Email = "api@teste.com",
                    NormalizedEmail = "API@TESTE.COM",
                    AccessFailedCount = 0,
                    LockoutEnabled = false,
                    PasswordHash = "AQAAAAIAAYagAAAAEA8BzmHCVEcOD+VNHR7Z91SjCRm9Zc4yodRPaowNC98ttq1IuwawRlqBzwUPidXCnw==",
                    TwoFactorEnabled = false,
                    ConcurrencyStamp = Guid.NewGuid().ToString(),
                    EmailConfirmed = true,
                    SecurityStamp = Guid.NewGuid().ToString()
                });

                await identityContext.SaveChangesAsync();

                clickDb.Vendedores.Add(new Business.Models.Vendedor() { Id = idUsuario, Nome = "Vendedor API", Email = "api@teste.com" });
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
                Console.WriteLine("[Api] Dados da API inseridos com sucesso.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Api] Erro ao inserir dados: {ex.Message}");
                throw;
            }
        }
    }
}