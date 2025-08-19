using System.ComponentModel.DataAnnotations;

namespace ClickMarket.Spa.Models;

public class ClienteViewModel
{
    [Key]
    public Guid Id { get; set; }

    [Required(ErrorMessage = "Preencha o campo {0}.")]
    [StringLength(100, ErrorMessage = "O campo {0} precisa ter no mínimo {2} caracteres e no máximo {1}", MinimumLength = 2)]
    public string Nome { get; set; }

    [Required(ErrorMessage = "Preencha o campo {0}.")]
    [StringLength(100, ErrorMessage = "O campo {0} precisa ter no mínimo {2} caracteres e no máximo {1}", MinimumLength = 2)]
    [EmailAddress]
    public string Email { get; set; }

    [StringLength(20, ErrorMessage = "O campo {0} precisa ter no mínimo {2} caracteres e no máximo {1}", MinimumLength = 2)]
    public string Telefone { get; set; }
}
