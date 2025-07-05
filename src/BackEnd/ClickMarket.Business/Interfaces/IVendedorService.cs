using ClickMarket.Business.Models;

namespace ClickMarket.Business.Interfaces
{
    internal interface IVendedorService : IDisposable
    {
        public Task Adicionar(Vendedor vendedor);
        public Task Remover(Guid id);
        public Task Atualizar(Vendedor vendedor);
    }
}
