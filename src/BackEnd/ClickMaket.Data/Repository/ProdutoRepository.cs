using ClickMarket.Business.Interfaces;
using ClickMarket.Business.Models;
using ClickMarket.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace ClickMarket.Data.Repository
{
    public class ProdutoRepository : Repository<Produto>, IProdutoRepository
    {
        public ProdutoRepository(ClickDbContext clickDbContext) : base(clickDbContext)
        {

        }

        public async Task<IEnumerable<Produto>> ObterProdutoCategoria(Guid? clienteId = null)
        {
            return await _dbSet.AsNoTracking()
                    .Include(x => x.Categoria)
                    .Include(x => x.Vendedor)
                    .Include(x => x.Favorito)
                    .Where(x => !clienteId.HasValue || x.Favorito.ClienteId == clienteId || x.Favorito == null)
                    .ToListAsync();
        }

        public async Task<Produto> ObterProdutoCategoria(Guid id)
        {
            return await _dbSet
                        .AsNoTracking()
                        .Include(x => x.Categoria)
                        .Include(v => v.Vendedor)
                        .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<Produto>> ObterProdutoCategoriaPorVendedor(Guid idVendedor)
        {
            return await _dbSet
                        .AsNoTracking()
                        .Include(x => x.Categoria)
                        .Include(v => v.Vendedor)
                        .Where(x => x.VendedorId == idVendedor).ToListAsync();
        }

        public async Task<IEnumerable<Produto>> ObterProdutoPorCategoria(Guid idCategoria)
        {
            return await _dbSet
                        .AsNoTracking()
                        .Include(x => x.Categoria)
                        .Include(v => v.Vendedor)
                        .Where(x => x.CategoriaId == idCategoria).ToListAsync();
        }
    }
}
