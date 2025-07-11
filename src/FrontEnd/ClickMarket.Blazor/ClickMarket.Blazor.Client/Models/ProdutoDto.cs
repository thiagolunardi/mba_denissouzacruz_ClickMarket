namespace ClickMarket.Blazor.Client.Models
{
    public class ProdutoDto
    {
        public Guid Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        public decimal Valor { get; set; }
        public int QuantidadeEstoque { get; set; }
        public string Imagem { get; set; }
        public Guid CategoriaId { get; set; }
    }
}
