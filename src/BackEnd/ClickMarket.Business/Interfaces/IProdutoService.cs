using ClickMarket.Business.Dtos;
using ClickMarket.Business.Models;

namespace ClickMarket.Business.Interfaces;

public interface IProdutoService : IDisposable
{
    public Task Adicionar(Produto produto);
    public Task Remover(Guid id);
    public Task Atualizar(Produto produto);


    Task<ProdutoDto> ObterFavorito(Guid produtoId, Guid clienteId);
    Task<Favorito> AdicionarFavorito(Guid produtoId, Guid clienteId);
    Task RemoverFavorito(Guid produtoId, Guid clienteId);
}
