using ClickMarket.Business.Models;

namespace ClickMarket.Business.Interfaces
{
    public interface IProdutoRepository : IRepository<Produto>
    {
        Task<IEnumerable<Produto>> ObterProdutoCategoria();
        Task<Produto> ObterProdutoCategoria(Guid id);
        Task<IEnumerable<Produto>> ObterProdutoCategoriaPorVendedor(Guid idVendedor);

        Task<IEnumerable<Produto>> ObterProdutoPorCategoria(Guid idCategoria);
    }
}
