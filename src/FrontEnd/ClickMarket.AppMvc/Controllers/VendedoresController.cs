using ClickMarket.Business.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClickMarket.AppMvc.Controllers;

[Route("gestao-vendedores")]
[Authorize(Roles = "Administrador")]
public class VendedoresController : Controller
{
    private readonly IVendedorService _vendedorService;

    public VendedoresController(IVendedorService vendedorService)
    {
        _vendedorService = vendedorService;
    }

    public async Task<IActionResult> Index()
    {
        var vendedores = await _vendedorService.ObterTodosAsync();
        return View(vendedores);
    }

    [HttpPost]
    public async Task<IActionResult> AlterarStatus(Guid id)
    {
        await _vendedorService.InativarOuReativarAsync(id);
        return RedirectToAction(nameof(Index));
    }
}