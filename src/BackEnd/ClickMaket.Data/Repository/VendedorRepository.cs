using ClickMarket.Business.Interfaces;
using ClickMarket.Business.Models;
using ClickMarket.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace ClickMarket.Data.Repository
{
    public class VendedorRepository : Repository<Vendedor>, IVendedorRepository
    {
        public VendedorRepository(ClickDbContext clickDbContext) : base(clickDbContext)
        {

        }

         public async Task<IEnumerable<Vendedor>> ObterTodosAsync()
        {
            return await _dbSet.AsNoTracking().ToListAsync();
        }
    }
}
