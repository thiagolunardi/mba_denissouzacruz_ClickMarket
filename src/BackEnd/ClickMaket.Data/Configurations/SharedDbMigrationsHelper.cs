using Microsoft.EntityFrameworkCore;
using ClickMarket.Data.Context;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ClickMarket.Data.Configurations
{
    public static class SharedDbMigrationsHelper
    {
        private static readonly object _lock = new object();
        private static bool _isInitialized = false;
        private static bool _isInitializing = false;

        public static async Task EnsureSharedDatabaseInitialized(
            IServiceProvider serviceProvider, 
            string projectName,
            Func<IServiceProvider, Task> seedDataAction)
        {
            lock (_lock)
            {
                if (_isInitialized)
                {
                    Console.WriteLine($"[{projectName}] Banco já inicializado, pulando...");
                    return;
                }

                if (_isInitializing)
                {
                    Console.WriteLine($"[{projectName}] Aguardando inicialização em progresso...");
                    // Aguarda até a inicialização terminar
                    while (_isInitializing)
                    {
                        Thread.Sleep(100);
                    }
                    return;
                }

                _isInitializing = true;
            }

            try
            {
                //a
                Console.WriteLine($"[{projectName}] Iniciando inicialização do banco compartilhado...");
                
                using var scope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope();
                var env = scope.ServiceProvider.GetRequiredService<IWebHostEnvironment>();

                if (env.IsDevelopment())
                {
                    var clickContext = scope.ServiceProvider.GetRequiredService<ClickDbContext>();
                    
                    var canConnect = await clickContext.Database.CanConnectAsync();
                    
                    if (!canConnect)
                    {
                        Console.WriteLine($"[{projectName}] Banco não existe, criando...");
                        
                        await clickContext.Database.EnsureCreatedAsync();
                        Console.WriteLine($"[{projectName}] Banco ClickMarket criado.");
                        
                        await seedDataAction(serviceProvider);
                        Console.WriteLine($"[{projectName}] Dados iniciais inseridos.");
                    }
                    else
                    {
                        Console.WriteLine($"[{projectName}] Banco já existe, verificando dados...");
                        
                        try
                        {
                            var hasData = await clickContext.Categorias.AnyAsync();
                            if (!hasData)
                            {
                                Console.WriteLine($"[{projectName}] Banco vazio, inserindo dados...");
                                await seedDataAction(serviceProvider);
                            }
                            else
                            {
                                Console.WriteLine($"[{projectName}] Dados já existem.");
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"[{projectName}] Erro ao verificar dados: {ex.Message}");
                            Console.WriteLine($"[{projectName}] Tentando inserir dados mesmo assim...");
                            await seedDataAction(serviceProvider);
                        }
                    }
                }

                lock (_lock)
                {
                    _isInitialized = true;
                    _isInitializing = false;
                }

                Console.WriteLine($"[{projectName}] Inicialização concluída com sucesso!");
            }
            catch (Exception ex)
            {
                lock (_lock)
                {
                    _isInitializing = false;
                }
                
                Console.WriteLine($"[{projectName}] Erro durante inicialização: {ex.Message}");
                throw;
            }
        }

        public static async Task EnsureIdentityDatabase<TIdentityContext>(
            IServiceProvider serviceProvider, 
            string projectName) where TIdentityContext : DbContext
        {
            try
            {
                Console.WriteLine($"[{projectName}] Inicializando banco Identity...");
                
                using var scope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope();
                var identityContext = scope.ServiceProvider.GetRequiredService<TIdentityContext>();
                
                var canConnect = await identityContext.Database.CanConnectAsync();
                if (!canConnect)
                {
                    await identityContext.Database.EnsureCreatedAsync();
                    Console.WriteLine($"[{projectName}] Banco Identity criado.");
                }
                else
                {
                    Console.WriteLine($"[{projectName}] Banco Identity já existe.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[{projectName}] Erro ao criar banco Identity: {ex.Message}");
                throw;
            }
        }

        // Método para resetar o estado (útil para testes)
        public static void ResetInitializationState()
        {
            lock (_lock)
            {
                _isInitialized = false;
                _isInitializing = false;
            }
        }
    }
}