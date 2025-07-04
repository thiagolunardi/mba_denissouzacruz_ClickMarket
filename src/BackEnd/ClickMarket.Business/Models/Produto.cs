using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClickMarket.Business.Models
{
    public class Produto: EntityBase
    {
        public Guid CategoriaId { get; set; }
        public Guid VendedorId { get; set; }
        public string? Nome { get; set; }
        public string? Descricao { get; set; }
        public int Valor { get; set; }
        public int QuantidadeEstoque { get; set; }
        public string? Imagem { get; set; }
        public bool Ativo { get; set; }
        public Categoria? Categoria { get; set; }
        public Vendedor? Vendedor { get; set; }
    }
}
