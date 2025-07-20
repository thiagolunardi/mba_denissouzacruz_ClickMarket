using AutoMapper;
using ClickMarket.Business.Dtos;
using ClickMarket.Business.Interfaces;
using ClickMarket.Business.Models;
using ClickMarket.Business.Requests;

namespace ClickMarket.Business.Services;

public class ClienteService(
    INotificador notificador,
    IClienteRepository clienteRepository,
    IProdutoRepository produtoRepository,
    IFavoritoRepository favoritoRepository,
    IMapper mapper) : BaseService(notificador), IClienteService
{
    public async Task<Cliente> Adicionar(ClienteRequest request)
    {
        var cliente = mapper.Map<ClienteRequest, Cliente>(request);

        if (!request.IsValid())
        {
            Notificar(request.ValidationResult);
            return cliente;
        }

        //Não foi pedido, mas pode ter validação de CPF nesse ponto.

        await clienteRepository.Adicionar(cliente);

        return cliente;
    }

    public async Task<Cliente> Atualizar(Guid id, ClienteRequest request)
    {
        if (id == Guid.Empty)
        {
            Notificar("O ID do cliente não pode ser vazio.");
            return null;
        }

        if (!request.IsValid())
        {
            Notificar(request.ValidationResult);
            return null;
        }

        // Verifica se o cliente existe
        var clienteExistente = await clienteRepository.ObterPorId(id);

        if (clienteExistente == null)
        {
            Notificar("Cliente não encontrado.");
            return null;
        }

        clienteExistente.Nome = request.Nome;
        clienteExistente.Email = request.Email;
        clienteExistente.Telefone = request.Telefone;
        clienteExistente.Ativo = request.Ativo;

        await clienteRepository.Atualizar(clienteExistente);

        return clienteExistente;
    }

    public async Task<ClienteDto> ObterPorId(Guid id)
    {
        if (id == Guid.Empty)
        {
            Notificar("O ID do cliente não pode ser vazio.");
            return null;
        }

        // Verifica se o cliente existe
        var clienteExistente = await clienteRepository.ObterPorId(id);

        if (clienteExistente == null)
        {
            Notificar("Cliente não encontrado.");
            return null;
        }

        // Mapeia o cliente para o DTO
        var clienteDto = mapper.Map<Cliente, ClienteDto>(clienteExistente);

        return clienteDto;
    }

    public async Task<IEnumerable<ClienteDto>> ObterTodos()
    {
        var clientes = await clienteRepository.ObterTodos();

        return mapper.Map<IEnumerable<Cliente>, IEnumerable<ClienteDto>>(clientes);
    }

    public async Task Remover(Guid id)
    {
        await clienteRepository.Remover(id);
    }


    public async Task<IEnumerable<FavoritoDto>> ObterTodosFavoritos(Guid clienteId)
    {
        //Regra de negócio
        var models = await favoritoRepository.ObterTodosAtivos(clienteId);

        return mapper.Map<List<Favorito>, List<FavoritoDto>>(models);
    }

    public async Task<FavoritoDto> ObterFavoritoPorIds(Guid produtoId, Guid clienteId)
    {
        if (produtoId == Guid.Empty)
        {
            Notificar("O do Produto não pode ser vazio.");
            return null;
        }

        if (clienteId == Guid.Empty)
        {
            Notificar("O ID do Cliente não pode ser vazio.");
            return null;
        }
        var favorito = await favoritoRepository.ObterPorProdutoCliente(produtoId, clienteId);
        if (favorito == null)
        {
            Notificar("Favorito não encontrado.");
            return null;
        }
        return mapper.Map<Favorito, FavoritoDto>(favorito);
    }

    public async Task<Favorito> AdicionarFavorito(Guid produtoId, Guid clienteId)
    {
        var favorito = new Favorito
        {
            Id = Guid.NewGuid(),
            ProdutoId = produtoId,
            ClienteId = clienteId
        };

        var modelExistente = await favoritoRepository.ObterPorProdutoCliente(produtoId, clienteId);

        if (modelExistente != null)
        {
            Notificar("Este produto já está na lista de favoritos.");
            return null;
        }

        var produto = await produtoRepository.ObterPorId(produtoId);

        if (produto == null)
        {
            Notificar("Produto não encontrado ou não está ativo.");
            return null;
        }

        await favoritoRepository.Adicionar(favorito);

        return favorito;
    }

    public async Task RemoverFavorito(Guid id)
    {
        var favorito = await favoritoRepository.ObterPorId(id);

        if (favorito == null)
        {
            Notificar("Favorito não encontrado.");
            return;
        }

        await favoritoRepository.Remover(id);
    }

    public void Dispose()
    {
        clienteRepository?.Dispose();
        GC.SuppressFinalize(this);
    }
}