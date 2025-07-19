namespace ClickMarket.Blazor.Client.Models
{
    public class FavoritoDto
    {
        public Guid Id { get; set; }
        public Guid ProdutoId { get; set; }
        public Guid ClienteId { get; set; }
        public ProdutoDto Produto { get; set; }
    }
}
