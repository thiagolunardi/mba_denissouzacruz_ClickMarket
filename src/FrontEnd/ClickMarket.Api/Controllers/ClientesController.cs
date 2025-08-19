using AutoMapper;
using ClickMarket.Api.ViewModels;
using ClickMarket.Business.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClickMarket.Api.Controllers
{
    [Authorize(Roles = "Administrador")]
    [ApiController]
    [Route("api/clientes")]
    public class ClientesController(IClienteService clienteService, INotificador notificador, IUser user, IMapper mapper) : MainController(notificador, user)
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ClienteViewModel>))]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<IEnumerable<ClienteViewModel>>> ObterTodos()
        {
            var clientes = await clienteService.ObterTodos();

            var clientesViewModel = mapper.Map<IEnumerable<ClienteViewModel>>(clientes);

            return CustomResponse(clientesViewModel);
        }

        [HttpGet("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ClienteViewModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> ObterPorId(Guid id)
        {
            var cliente = await clienteService.ObterPorId(id);

            if (cliente == null)
            {
                return NotFound();
            }

            var clienteViewModel = mapper.Map<ClienteViewModel>(cliente);

            return CustomResponse(clienteViewModel);
        }
    }

}
