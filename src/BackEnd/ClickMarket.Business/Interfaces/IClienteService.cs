using ClickMarket.Business.Models;

namespace ClickMarket.Business.Interfaces;

public interface IClienteService : IDisposable
{
    Task<Cliente> ObterPorId(Guid id);
    Task<IEnumerable<Cliente>> ObterTodos();
    Task<Cliente> Adicionar(Cliente cliente);
}
