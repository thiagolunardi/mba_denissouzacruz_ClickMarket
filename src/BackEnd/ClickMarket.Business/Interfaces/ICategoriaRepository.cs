using ClickMarket.Business.Models;

namespace ClickMarket.Business.Interfaces;
public interface ICategoriaRepository : IRepository<Categoria>
{
    Task<Categoria> ObterCategoriaProduto(Guid id);
}
