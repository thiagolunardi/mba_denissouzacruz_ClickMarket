namespace ClickMarket.Business.Models
{
    public class Vendedor : EntityBase
    {
        public IEnumerable<Produto> Produtos { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public bool Ativo { get; set; } = true;

    }
}
