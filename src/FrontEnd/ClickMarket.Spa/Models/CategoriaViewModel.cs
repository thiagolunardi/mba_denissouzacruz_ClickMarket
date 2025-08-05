using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClickMarket.Spa.Models
{
    public class CategoriaViewModel
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }

        public string Descricao { get; set; }
    }
}