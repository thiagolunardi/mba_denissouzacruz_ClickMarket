using ClickMarket.Data.Context;
using ClickMarket.Business.Interfaces;
using ClickMarket.Business.Models;

namespace ClickMarket.Data.Repository
{
    public class VendedorRepository : Repository<Vendedor>, IVendedorRepository
    {
        public VendedorRepository(ClickDbContext clickDbContext) : base(clickDbContext)
        {

        }
    }
}
