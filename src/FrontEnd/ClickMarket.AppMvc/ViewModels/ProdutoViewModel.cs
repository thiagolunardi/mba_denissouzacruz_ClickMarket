using ClickMarket.Business.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ClickMarket.AppMvc.ViewModels
{
    public class ProdutoViewModel
    {
        public ProdutoViewModel()
        {
            Id = Guid.NewGuid();
        }

        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Preencha o campo {0}.")]
        [StringLength(100, ErrorMessage = "O campo {0} precisa ter no mínimo {2} caracteres e no máximo {1}", MinimumLength = 2)]
        public string? Nome { get; set; }

        [Required(ErrorMessage = "Preencha o campo {0}.")]
        [StringLength(200, ErrorMessage = "O campo {0} precisa ter no mínimo {2} caracteres e no máximo {1}", MinimumLength = 2)]
        [DisplayName("Descrição")]
        public string? Descricao { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [DisplayName("Categoria")]
        public Guid CategoriaId { get; set; }

        [Required(ErrorMessage = "Preencha o campo {0}.")]
        [Range(1, Int32.MaxValue, ErrorMessage = "O valor precisa ser maior que 0")]
        [DisplayName("Preço")]
        public int Valor { get; set; }

        [Required(ErrorMessage = "Preencha o campo {0}.")]
        [Range(1, Int32.MaxValue, ErrorMessage = "O valor precisa ser maior que 0")]
        [DisplayName("Qtd em Estoque")]
        public int QuantidadeEstoque { get; set; }

        public Guid? VendedorId { get; set; }
        public string? Imagem { get; set; }

        [Required(ErrorMessage = "Preencha o campo {0}.")]
        [DisplayName("Imagem do produto")]
        public IFormFile UploadImagem { get; set; }
        public CategoriaViewModel? Categoria { get; set; }
        public IEnumerable<CategoriaViewModel>? Categorias { get; set; }
        public Vendedor? Vendedor { get; set; }
    }
}
