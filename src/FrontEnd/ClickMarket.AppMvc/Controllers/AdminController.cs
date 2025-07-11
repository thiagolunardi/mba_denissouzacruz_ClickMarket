using ClickMarket.AppMvc.Models;
using ClickMarket.Business.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ClickMarket.AppMvc.Controllers;

public class AdminController : Controller
{
    private readonly IVendedorService _vendedorService;

    public AdminController(IVendedorService vendedorService)
    {
        _vendedorService = vendedorService;
    }

    public async Task<IActionResult> Vendedores()
    {
        var vendedores = await _vendedorService.ObterTodosAsync();
        return View(vendedores);
    }

    [HttpPost]
    public async Task<IActionResult> AlterarStatus(Guid id)
    {
        await _vendedorService.InativarOuReativarAsync(id);
        return RedirectToAction("Vendedores");
    }
}
