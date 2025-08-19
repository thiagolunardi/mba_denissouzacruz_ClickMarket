using System.ComponentModel.DataAnnotations;

namespace ClickMarket.Spa.Models;

public class FavoritoViewModel
{
    [Key]
    public Guid Id { get; set; }
    public Guid ProdutoId { get; set; }
    public Guid ClienteId { get; set; }
    public ProdutoViewModel Produto { get; set; }
}
