namespace ClickMarket.Business.Dtos;

public class ProdutoDto
{
    public Guid Id { get; set; }
    public Guid CategoriaId { get; set; }
    public Guid VendedorId { get; set; }
    public string Nome { get; set; }
    public string Descricao { get; set; }
    public int Valor { get; set; }
    public int QuantidadeEstoque { get; set; }
    public string Imagem { get; set; }
    public bool Ativo { get; set; }
}