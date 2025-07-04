using AutoMapper;
using ClickMarket.Api.Models;
using ClickMarket.Business.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ClickMarket.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/categorias")]
    public class CategoriasController : ControllerBase
    {
        private readonly IProdutoRepository _produtoRepository;
        private readonly ICategoriaRepository _categoriaRepository;
        private readonly IMapper _mapper;

        public CategoriasController(IProdutoRepository produtoRepository,
                                    ICategoriaRepository categoriaRepository,
                                    IMapper mapper)
        {
            _produtoRepository = produtoRepository;
            _categoriaRepository = categoriaRepository;
            _mapper = mapper;
        }

        [AllowAnonymous]
        [HttpGet("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<Models.CategoriaListViewModel>> GetCategoria(Guid id)
        {
            var categoria = await _categoriaRepository.ObterPorId(id);

            if (categoria == null)
            {
                return NotFound();
            }

            var categoriaModel = _mapper.Map<Models.CategoriaListViewModel>(categoria);
            return categoriaModel;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Models.CategoriaListViewModel>>> GetCategorias()
        {
            var categorias = await _categoriaRepository.ObterTodos();
            var categoriasModel = _mapper.Map<IEnumerable<Models.CategoriaListViewModel>>(categorias);

            return categoriasModel.ToList();
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<Models.CategoriaViewModel>> PostCategoria(Models.CategoriaViewModel categoria)
        {
            if (!ModelState.IsValid)
            {
                return ValidationProblem(new ValidationProblemDetails(ModelState)
                {
                    Title = "Ocorreu um ou mais erros ao tentar inserir a categoria!"
                });
            }

            var categoriaModel = _mapper.Map<Business.Models.Categoria>(categoria);
            await _categoriaRepository.Adicionar(categoriaModel);

            return CreatedAtAction(nameof(GetCategoria), new { Id = categoriaModel.Id }, categoria);
        }

        [HttpPut("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> PutCategoria(Guid id, Models.CategoriaViewModel categoria)
        {
            if (!ModelState.IsValid)
            {
                return ValidationProblem(new ValidationProblemDetails(ModelState)
                {
                    Title = "Ocorreu um ou mais erros ao tentar editar a categoria!"
                });
            }

            var categoriaBd = await _categoriaRepository.ObterPorId(id);
            if (categoriaBd == null)
                return NotFound();

            _mapper.Map(categoria, categoriaBd);

            try
            {
                await _categoriaRepository.Atualizar(categoriaBd);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (await _categoriaRepository.ObterPorId(id) == null)
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
        public async Task<IActionResult> DeleteCategoria(Guid id)
        {
            var categoriaBd = await _categoriaRepository.ObterCategoriaProduto(id);
            if (categoriaBd == null)
                return NotFound();

            if (categoriaBd.Produtos.Any())
                return Problem("Não é possível excluir uma categoria com produtos associados");

            await _categoriaRepository.Remover(id);
            return NoContent();
        }

        private ActionResult<ProdutoViewModel> ReturnValidationProblem()
        {
            return ValidationProblem(new ValidationProblemDetails(ModelState)
            { Title = "Ocorreu um ou mais erros ao enviar informações da categoria" });
        }
    }

}
