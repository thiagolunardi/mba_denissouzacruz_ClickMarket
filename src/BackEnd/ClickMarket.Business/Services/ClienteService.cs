using ClickMarket.Business.Interfaces;
using ClickMarket.Business.Models;

namespace ClickMarket.Business.Services;

public class ClienteService(
    INotificador notificador,
    IClienteRepository clienteRepository) : BaseService(notificador), IClienteService
{
    public async Task<Cliente> Adicionar(Cliente cliente)
    {
        //Não foi pedido, mas pode ter validação de CPF nesse ponto.

        await clienteRepository.Adicionar(cliente);

        return cliente;
    }


    public async Task<Cliente> ObterPorId(Guid id)
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

        return clienteExistente;
    }

    public async Task<IEnumerable<Cliente>> ObterTodos()
    {
        return await clienteRepository.ObterTodos();
    }

    public void Dispose()
    {
        clienteRepository?.Dispose();
        GC.SuppressFinalize(this);
    }
}