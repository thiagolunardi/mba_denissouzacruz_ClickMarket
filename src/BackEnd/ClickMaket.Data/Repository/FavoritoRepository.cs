using ClickMarket.Business.Interfaces;
using ClickMarket.Business.Models;
using ClickMarket.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace ClickMarket.Data.Repository;

public class FavoritoRepository(ClickDbContext context) : Repository<Favorito>(context), IFavoritoRepository
{
    public async Task<Favorito> ObterPorProdutoCliente(Guid produtoId, Guid clienteId)
    {
        return await _dbSet.AsNoTracking()
            .Include(a => a.Cliente)
            .FirstOrDefaultAsync(a => a.ProdutoId == produtoId && a.ClienteId == clienteId);
    }

    public async Task<List<Favorito>> ObterTodosAtivos(Guid clienteId)
    {
        return await _dbSet.AsNoTracking()
            .Include(a => a.Cliente)
            .Where(a => a.ClienteId.Equals(clienteId))
            .ToListAsync();
    }
}