using ClickMarket.Business.Interfaces;
using ClickMarket.Business.Models;
using ClickMarket.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace ClickMarket.Data.Repository;

public class ClienteRepository(ClickDbContext context) : Repository<Cliente>(context), IClienteRepository
{
    public async Task<Cliente> ObterPorNome(string nome)
    {
        return await _dbSet.AsNoTracking()
            .FirstOrDefaultAsync(c => c.Nome == nome);
    }
}