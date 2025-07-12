using ClickMarket.Business.Models;
using System.Linq.Expressions;

namespace ClickMarket.Business.Interfaces
{
    public interface IRepository<T> : IDisposable where T : EntityBase
    {
        Task Adicionar(T entity);
        Task Remover(Guid id);
        Task Atualizar(T entity);
        Task<T> ObterPorId(Guid id);
        Task<List<T>> ObterTodos();
        Task<IEnumerable<T>> Pesquisar(Expression<Func<T, bool>> predicate);
        Task<int> SaveChanges();
    }
}
