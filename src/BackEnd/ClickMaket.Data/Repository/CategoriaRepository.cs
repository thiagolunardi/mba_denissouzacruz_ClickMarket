using ClickMarket.Data.Context;
using ClickMarket.Business.Interfaces;
using ClickMarket.Business.Models;
using Microsoft.EntityFrameworkCore;

namespace ClickMarket.Data.Repository
{
    public class CategoriaRepository: Repository<Categoria>, ICategoriaRepository
    {
        public CategoriaRepository(ClickDbContext clickDbContext) : base(clickDbContext)
        {

        }

        public async Task<Categoria> ObterCategoriaProduto(Guid id)
        {
            return await _dbSet.AsNoTracking()
                    .Include(x => x.Produtos)
                    .FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
