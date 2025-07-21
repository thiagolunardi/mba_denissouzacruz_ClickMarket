using ClickMarket.Business.Interfaces;
using ClickMarket.Business.Models;
using ClickMarket.Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ClickMarket.Data.Repository
{
    public abstract class Repository<T> : IRepository<T> where T : EntityBase, new()
    {
        protected readonly ClickDbContext _clickDb;
        protected readonly DbSet<T> _dbSet;

        protected Repository(ClickDbContext dbContext)
        {
            _clickDb = dbContext;
            _dbSet = _clickDb.Set<T>();
        }

        public async Task Adicionar(T entity)
        {
            _dbSet.Add(entity);
            await SaveChanges();
        }

        public async Task Atualizar(T entity)
        {
            _clickDb.Entry(entity).State = EntityState.Modified;
            await SaveChanges();
        }

        public async Task<T> ObterPorId(Guid id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<List<T>> ObterTodos()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<IEnumerable<T>> Pesquisar(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.AsNoTracking().Where(predicate).ToListAsync();
        }

        public async Task Remover(Guid id)
        {
            T objeto = await _dbSet.FindAsync(id);
            if (objeto == null)
                return;

            _dbSet.Remove(objeto);
            await SaveChanges();
        }

        public async Task<int> SaveChanges()
        {
            try
            {
                return await _clickDb.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void Dispose()
        {
            _clickDb?.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
