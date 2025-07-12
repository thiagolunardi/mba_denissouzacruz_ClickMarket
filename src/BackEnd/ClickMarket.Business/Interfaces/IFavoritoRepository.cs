using ClickMarket.Business.Models;

namespace ClickMarket.Business.Interfaces;

public interface IFavoritoRepository : IRepository<Favorito>
{
    Task<List<Favorito>> ObterTodosAtivos(Guid clienteId);
    Task<Favorito> ObterPorProdutoCliente(Guid produtoId, Guid clienteId);
}