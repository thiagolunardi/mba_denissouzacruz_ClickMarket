using ClickMarket.Business.Models;

namespace ClickMarket.Business.Interfaces;

public interface IVendedorService : IDisposable
{
    public Task Adicionar(Vendedor vendedor);
    public Task Remover(Guid id);
    public Task Atualizar(Vendedor vendedor);
    Task<IEnumerable<Vendedor>> ObterTodosAsync();
    Task InativarOuReativarAsync(Guid id);


}
