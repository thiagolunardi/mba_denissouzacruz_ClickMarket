using ClickMarket.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClickMarket.Business.Interfaces
{
    public interface ICategoriaRepository: IRepository<Categoria>
    {
        Task<Categoria> ObterCategoriaProduto(Guid id);
    }
}
