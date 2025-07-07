using ClickMarket.Business.Models;

namespace ClickMarket.Business.Interfaces;

public interface IProdutoService : IDisposable
{
    public Task Adicionar(Produto produto);
    public Task Remover(Guid id);
    public Task Atualizar(Produto produto);
}
