using AutoMapper;
using ClickMarket.Api.Models;
using ClickMarket.Business.Dtos;
using ClickMarket.Business.Interfaces;
using ClickMarket.Business.Models;
using ClickMarket.Data.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System.Security.Claims;

namespace ClickMarket.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/produtos")]
    public class ProdutosController : MainController
    {
        private readonly IProdutoService _produtoService;
        private readonly IProdutoRepository _produtoRepository;
        private readonly ICategoriaRepository _categoriaRepository;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public ProdutosController(IProdutoRepository produtoRepository,
                                    ICategoriaRepository categoriaRepository,
                                    IMapper mapper,
                                    IConfiguration configuration,
                                    INotificador notificador,
                                    IUser user,
                                    IProdutoService produtoService) : base(notificador, user)
        {
            _produtoRepository = produtoRepository;
            _categoriaRepository = categoriaRepository;
            _mapper = mapper;
            _configuration = configuration;
            _produtoService = produtoService;
        }

        [AllowAnonymous]
        [HttpGet("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<ProdutoListViewModel>> GetProduto(Guid id)
        {
            var produto = await _produtoRepository.ObterProdutoCategoria(id);

            if (produto == null)
            {
                return NotFound();
            }
            var produtoModel = _mapper.Map<Models.ProdutoListViewModel>(produto);
            return produtoModel;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<List<ProdutoListViewModel>>> GetProdutos()
        {
            var produtoCategoria = await _produtoRepository.ObterTodosIncluindoFavoritos(UsuarioId != Guid.Empty ? UsuarioId : null);
            var produtoModel = _mapper.Map<IEnumerable<ProdutoListViewModel>>(produtoCategoria);

            return produtoModel.ToList();
        }

        [AllowAnonymous]
        [HttpGet("categoria/{idCategoria}")]
        public async Task<ActionResult<IEnumerable<Models.ProdutoListViewModel>>> GetProdutosCategoria(Guid idCategoria)
        {
            var produtoCategoria = await _produtoRepository.ObterProdutoPorCategoria(idCategoria);
            var produtoModel = _mapper.Map<IEnumerable<Models.ProdutoListViewModel>>(produtoCategoria);

            return produtoModel.ToList();
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<Models.ProdutoViewModel>> PostProduto([FromForm] Models.ProdutoViewModel produto)
        {
            if (!ModelState.IsValid)
                return ReturnValidationProblem();

            var categoria = await _categoriaRepository.ObterPorId(produto.CategoriaId);
            if (categoria == null)
            {
                ModelState.AddModelError("CategoriaId", "Categoria não encontrada!");
                return ReturnValidationProblem();
            }

            var nomeImagem = ObterNomeImagemUpload(produto);
            if (!await SalvarImagem(produto.Imagem, nomeImagem))
                return ReturnValidationProblem();

            var produtoModel = _mapper.Map<Business.Models.Produto>(produto);
            produtoModel.VendedorId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            produtoModel.Imagem = nomeImagem;
            await _produtoRepository.Adicionar(produtoModel);

            return CreatedAtAction(nameof(GetProduto), new { Id = produtoModel.Id }, produto);
        }

        [HttpPut("{id:Guid}")]
        [Consumes("multipart/form-data")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<Models.ProdutoViewModel>> PutProduto(Guid id, [FromForm] Models.ProdutoViewModel produto)
        {
            if (!ModelState.IsValid)
                return ReturnValidationProblem();

            var produtoBd = await _produtoRepository.ObterPorId(id);
            if (produtoBd == null)
                return NotFound();

            var categoria = await _categoriaRepository.ObterPorId(produto.CategoriaId);
            if (categoria == null)
            {
                ModelState.AddModelError("CategoriaId", "Categoria não encontrada!");
                return ReturnValidationProblem();
            }

            var usuarioLogado = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            if (produtoBd.VendedorId != usuarioLogado)
            {
                ModelState.AddModelError("VendedorId", "Não é possível realizar alterações em produto que não pertence ao usuário logado!");
                return ReturnValidationProblem();
            }

            var nomeImagem = ObterNomeImagemUpload(produto);
            if (!await SalvarImagem(produto.Imagem, nomeImagem))
                return ReturnValidationProblem();

            ExcluirImagem(produtoBd.Imagem);

            _mapper.Map(produto, produtoBd);
            produtoBd.Imagem = nomeImagem;
            try
            {
                await _produtoRepository.Atualizar(produtoBd);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (await _produtoRepository.ObterPorId(id) == null)
                {
                    return NotFound();
                }
                throw;
            }


            return NoContent();
        }

        [HttpDelete("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> DeleteProduto(Guid id)
        {
            var produtoBd = await _produtoRepository.ObterPorId(id);
            if (_produtoRepository.ObterPorId(id) == null)
            {
                return NotFound();
            }
            var usuarioLogado = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            if (produtoBd.VendedorId != usuarioLogado)
                return Problem("Não é possível realizar alterações em produto que não pertence ao usuário logado!");

            await _produtoRepository.Remover(id);
            return NoContent();
        }

        [HttpGet]
        [Route("favoritos")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<FavoritoDto>))]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<List<ProdutoListViewModel>>> ObterFavoritos()
        {
            var produtos = await _produtoRepository.ObterTodosApenasFavoritos(UsuarioId);
            var produtoModel = _mapper.Map<IEnumerable<ProdutoListViewModel>>(produtos);
            return produtoModel.ToList();
        }

        [HttpPost("favoritos/{produtoId}")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(FavoritoDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> AdicionarFavorito(Guid produtoId)
        {
            if (produtoId == Guid.Empty)
            {
                AdicionarErroProcessamento("O ID do produto não pode ser vazio.");
                return CustomResponse();
            }

            var favorito = await _produtoService.AdicionarFavorito(produtoId, UsuarioId);

            if (!OperacaoValida()) return CustomResponse();

            return CustomResponse(favorito);
        }

        [HttpDelete("favoritos/{produtoId:Guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> RemoverFavorito(Guid produtoId)
        {
            var favorito = await _produtoService.ObterFavorito(produtoId, UsuarioId);
            if (favorito == null)
            {
                return NotFound();
            }
            await _produtoService.RemoverFavorito(produtoId, UsuarioId);

            return CustomResponse();
        }

        private ActionResult<ProdutoViewModel> ReturnValidationProblem()
        {
            return ValidationProblem(new ValidationProblemDetails(ModelState)
            { Title = "Ocorreu um ou mais erros ao enviar informações do produto" });
        }

        private string ObterNomeImagemUpload(ProdutoViewModel produtoViewModel)
        {
            var fileName = Path.GetFileName(produtoViewModel.Imagem.FileName);
            return $"{Guid.NewGuid()}-{fileName}";
        }

        private async Task<bool> SalvarImagem(IFormFile arquivo, string nomeImagem)
        {
            var retornoSalvarImagem = true;
            if (arquivo != null && arquivo.Length > 0)
            {
                try
                {
                    string caminhoBase = _configuration["Parametros:DiretorioBaseImagemProduto"];
                    var caminhoUpload = Path.Combine(caminhoBase, "wwwroot", "images", "upload");

                    if (!Directory.Exists(caminhoUpload))
                        Directory.CreateDirectory(caminhoUpload);

                    var diretorio = Path.Combine(caminhoUpload, nomeImagem);

                    using (var stream = new FileStream(diretorio, FileMode.Create))
                    {
                        await arquivo.CopyToAsync(stream);
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("Imagem", $"Erro ao salvar imagem: {ex.Message}");
                    retornoSalvarImagem = false;
                }
            }
            else
            {
                ModelState.AddModelError("Imagem", "A imagem selecionada é inválida!");
                retornoSalvarImagem = false;
            }
            return retornoSalvarImagem;
        }

        private void ExcluirImagem(string nomeImagem)
        {
            string caminhoBase = _configuration["Parametros:DiretorioBaseImagemProduto"];
            var caminhoImagem = Path.Combine(caminhoBase, "wwwroot", "images", "upload", nomeImagem);
            if (System.IO.File.Exists(caminhoImagem))
            {
                System.IO.File.Delete(caminhoImagem);
            }
        }
    }

}
