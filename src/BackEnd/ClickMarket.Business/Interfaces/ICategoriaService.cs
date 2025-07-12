using ClickMarket.Business.Models;

namespace ClickMarket.Business.Interfaces;

public interface ICategoriaService : IDisposable
{
    public Task Adicionar(Categoria categoria);
    public Task Remover(Guid id);
    public Task Atualizar(Categoria categoria);
}
