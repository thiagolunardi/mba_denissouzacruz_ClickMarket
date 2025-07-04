namespace ClickMarket.Business.Models
{
    public class Categoria: EntityBase
    {
        public string? Nome { get; set; }
        public string? Descricao { get; set; }
        public IEnumerable<Produto>? Produtos { get; set; }
    }
}
