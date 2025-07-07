using ClickMarket.Business.Models;

namespace ClickMarket.Business.Interfaces;

public interface IClienteRepository : IRepository<Cliente>
{
    Task<Cliente> ObterPorNome(string nome);
}