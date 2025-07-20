using ClickMarket.Business.Dtos;
using ClickMarket.Business.Models;
using ClickMarket.Business.Requests;

namespace ClickMarket.Business.Interfaces;

public interface IClienteService : IDisposable
{
    Task<ClienteDto> ObterPorId(Guid id);
    Task<IEnumerable<ClienteDto>> ObterTodos();
    Task<Cliente> Adicionar(ClienteRequest cliente);
    Task<Cliente> Atualizar(Guid id, ClienteRequest cliente);
    Task Remover(Guid id);
}
