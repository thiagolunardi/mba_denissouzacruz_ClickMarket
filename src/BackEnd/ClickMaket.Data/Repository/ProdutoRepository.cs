using ClickMarket.Business.Interfaces;
using ClickMarket.Business.Models;
using ClickMarket.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace ClickMarket.Data.Repository
{
    public class ProdutoRepository(ClickDbContext clickDbContext) : Repository<Produto>(clickDbContext), IProdutoRepository
    {
        public async Task<List<Produto>> ObterTodosProdutos()
        {
            return await _dbSet.AsNoTracking()
                    .Include(p => p.Categoria)
                    .Include(p => p.Vendedor)
                    .ToListAsync();
        }

        public async Task<IEnumerable<Produto>> ObterProdutoCategoria(Guid? clienteId = null)
        {
            return await _dbSet.AsNoTracking()
                    .Include(p => p.Categoria)
                    .Include(p => p.Vendedor)
                    .Include(p => p.Favorito)
                    .Where(p => !clienteId.HasValue || p.Favorito.ClienteId == clienteId || p.Favorito == null && p.Ativo && p.Vendedor.Ativo)
                    .ToListAsync();
        }

        public async Task<Produto> ObterProdutoCategoria(Guid id)
        {
            return await _dbSet
                        .AsNoTracking()
                        .Include(p => p.Categoria)
                        .Include(p => p.Vendedor)
                        .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<List<Produto>> ObterProdutoPorVendedor(Guid vendedorId)
        {
            return await _dbSet
                .AsNoTracking()
                .Include(p => p.Categoria)
                .Include(p => p.Vendedor)
                .Where(p => p.VendedorId == vendedorId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Produto>> ObterProdutosPorCategoriaId(Guid categoriaId)
        {
            return await _dbSet
        .AsNoTracking()
        .Include(p => p.Categoria)
        .Include(p => p.Vendedor)
        .Where(p => p.CategoriaId == categoriaId && p.Ativo && p.Vendedor.Ativo)
        .ToListAsync();
        }

        public async Task<List<Produto>> ObterTodosIncluindoFavoritos(Guid? clienteId = null)
        {
            var produtos = new List<Produto>();
            if (clienteId.HasValue)
            {
                var produtosFavoritados = await _dbSet
                                        .AsNoTracking()
                                        .Include(p => p.Favorito)
                                        .Where(p => p.Favorito.ClienteId == clienteId && p.Ativo && p.Vendedor.Ativo)
                                        .ToListAsync();

                var idsFavoritados = produtosFavoritados.Select(x => x.Id).ToList();

                var produtosTotais = await _dbSet
                                        .AsNoTracking()
                                        .Where(p => !idsFavoritados.Contains(p.Id) && p.Ativo && p.Vendedor.Ativo)
                                        .ToListAsync();

                produtos.AddRange(produtosFavoritados);
                produtos.AddRange(produtosTotais);

                return [.. produtos.OrderBy(a => a.Nome)];
            }

            produtos.AddRange(await _dbSet
                                        .AsNoTracking()
                                        .Where(p => p.Ativo && p.Vendedor.Ativo)
                                        .ToListAsync());

            return produtos;
        }

        public async Task<List<Produto>> ObterTodosApenasFavoritos(Guid clienteId)
        {
            if (clienteId == Guid.Empty)
                return [];

            return await _dbSet
                            .AsNoTracking()
                            .Include(p => p.Favorito)
                            .Where(p => p.Favorito.ClienteId == clienteId && p.Ativo && p.Vendedor.Ativo)
                            .ToListAsync();
        }

        public async Task<Produto> ObterProdutoFavorito(Guid produtoId, Guid clienteId)
        {
            return await _dbSet
                .AsNoTracking()
                .Include(x => x.Favorito)
                .FirstOrDefaultAsync(a => a.Favorito.ClienteId == clienteId && a.Id == produtoId);
        }
        public async Task<IEnumerable<Produto>> ObterProdutosPorCategoriaIncluindoFavoritos(Guid categoriaId, Guid clienteId)
        {
            var produtos = await _clickDb.Produtos
                .Where(p => p.CategoriaId == categoriaId && p.Ativo && p.Vendedor.Ativo)
                .ToListAsync();

            var favoritos = await _clickDb.Favoritos
                .Where(f => f.ClienteId == clienteId)
                .Select(f => f.ProdutoId)
                .ToListAsync();

            foreach (var produto in produtos)
            {
                if (favoritos.Contains(produto.Id))
                {
                    produto.Favorito = new Favorito { ProdutoId = produto.Id, ClienteId = clienteId };
                }
            }

            return produtos;
        }
    }
}
