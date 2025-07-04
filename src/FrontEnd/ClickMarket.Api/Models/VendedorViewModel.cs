using System.ComponentModel.DataAnnotations;

namespace ClickMarket.Api.Models
{
    public class VendedorViewModel
    {
        public VendedorViewModel()
        {
            Id = Guid.NewGuid();
        }

        [Key]
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
    }
}
