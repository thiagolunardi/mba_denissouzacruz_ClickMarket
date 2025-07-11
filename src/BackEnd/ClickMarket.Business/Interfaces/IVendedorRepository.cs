using ClickMarket.Business.Models;

namespace ClickMarket.Business.Interfaces
{
    public interface IVendedorRepository : IRepository<Vendedor>
    {
        Task<IEnumerable<Vendedor>> ObterTodosAsync();
    }
}
