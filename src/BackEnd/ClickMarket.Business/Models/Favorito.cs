namespace ClickMarket.Business.Models;

public class Favorito : EntityBase
{
    public Guid ClienteId { get; set; }
    public Guid ProdutoId { get; set; }

    //EF Relations
    public Cliente Cliente { get; set; }
}
