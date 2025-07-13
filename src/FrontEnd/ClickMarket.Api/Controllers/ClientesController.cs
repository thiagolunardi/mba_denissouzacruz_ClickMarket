using ClickMarket.Business.Dtos;
using ClickMarket.Business.Interfaces;
using ClickMarket.Business.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClickMarket.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/clientes")]
    public class ClientesController(IClienteService clienteService, INotificador notificador, IUser user) : MainController(notificador, user)
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ClienteDto>))]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<IEnumerable<Models.CategoriaListViewModel>>> ObterTodos()
        {
            var clientes = await clienteService.ObterTodos();

            return CustomResponse(clientes);
        }

        [HttpGet("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ClienteDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> ObterPorId(Guid id)
        {
            var cliente = await clienteService.ObterPorId(id);

            if (cliente == null)
            {
                return NotFound();
            }

            return CustomResponse(cliente);
        }

        [HttpGet]
        [Route("favoritos")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<FavoritoDto>))]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> ObterFavoritos()
        {
            var favoritos = await clienteService.ObterTodosFavoritos(UsuarioId);
            return CustomResponse(favoritos);
        }

        [HttpGet]
        [Route("favoritos/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(FavoritoDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> ObterFavoritoPorId(Guid id)
        {
            var favorito = await clienteService.ObterFavoritoPorId(id);
            if (favorito == null)
            {
                return NotFound();
            }
            return CustomResponse(favorito);
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

            var favorito = await clienteService.AdicionarFavorito(produtoId, UsuarioId);
            
            if (!OperacaoValida()) return CustomResponse();

            return CreatedAtAction(nameof(ObterFavoritoPorId), new { id = favorito.Id }, favorito);
        }

        [HttpDelete("favoritos/{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> RemoverFavorito(Guid id)
        {
            var favorito = await clienteService.ObterFavoritoPorId(id);
            if (favorito == null)
            {
                return NotFound();
            }
            await clienteService.RemoverFavorito(id);
            return NoContent();
        }
    }

}
