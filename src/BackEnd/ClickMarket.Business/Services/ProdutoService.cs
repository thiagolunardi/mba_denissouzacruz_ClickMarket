using ClickMarket.Business.Interfaces;
using ClickMarket.Business.Models;

namespace ClickMarket.Business.Services;

public class ProdutoService(
    INotificador notificador,
    IProdutoRepository produtoRepository) : BaseService(notificador), IProdutoService
{
    private readonly IProdutoRepository _produtoRepository = produtoRepository;

    public async Task Adicionar(Produto produto)
    {
        await _produtoRepository.Adicionar(produto);
    }

    public async Task Atualizar(Produto produto)
    {
        await _produtoRepository.Atualizar(produto);
    }

    public async Task Remover(Guid id)
    {
        await _produtoRepository.Remover(id);
    }

    public void Dispose()
    {
        _produtoRepository?.Dispose();
        GC.SuppressFinalize(this);
    }
}
