using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ClickMarket.Business.Models;
using ClickMarket.Business.Interfaces;
using AutoMapper;
using ClickMarket.AppMvc.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace ClickMarket.AppMvc.Controllers
{
    [Route("gestao-categorias")]
    [Authorize]
    public class CategoriasController : Controller
    {
        private readonly ICategoriaRepository _categoriaRepository;
        private readonly IMapper _mapper;

        public CategoriasController(ICategoriaRepository categoriaRepository,
            IMapper mapper)
        {
            _categoriaRepository = categoriaRepository;
            _mapper = mapper;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var categoria = await _categoriaRepository.ObterTodos();
            var categoriaViewModel = _mapper.Map<IEnumerable<CategoriaViewModel>>(categoria);
            return View(categoriaViewModel);
        }

        [HttpGet]
        [Route("detalhes/{id:guid}")]
        [AllowAnonymous]
        public async Task<IActionResult> Details(Guid id)
        {
            var categoria = await _categoriaRepository.ObterPorId(id);

            if (categoria == null)
            {
                return NotFound();
            }

            var categoriaViewModel = _mapper.Map<CategoriaViewModel>(categoria);
            return View(categoriaViewModel);
        }

        [HttpGet]
        [Route("nova")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("nova")]
        public async Task<IActionResult> Create([Bind("Nome,Descricao,Id")] CategoriaViewModel categoriaViewModel)
        {
            if (ModelState.IsValid)
            {
                var categoria = _mapper.Map<Categoria>(categoriaViewModel);
                await _categoriaRepository.Adicionar(categoria);

                return RedirectToAction(nameof(Index));
            }
            return View(categoriaViewModel);
        }

        [HttpGet]
        [Route("editar/{id:guid}")]
        public async Task<IActionResult> Edit(Guid id)
        {
            var categoria = await _categoriaRepository.ObterPorId(id);
            if (categoria == null)
            {
                return NotFound();
            }

            var categoriaViewModel = _mapper.Map<CategoriaViewModel>(categoria);
            return View(categoriaViewModel);
        }

        [HttpPost("editar/{id:guid}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Nome,Descricao,Id")] CategoriaViewModel categoriaViewModel)
        {
            if (id != categoriaViewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var categoria = _mapper.Map<Categoria>(categoriaViewModel);
                    await _categoriaRepository.Atualizar(categoria);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoriaExists(categoriaViewModel.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(categoriaViewModel);
        }

        [HttpGet]
        [Route("excluir/{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var categoria = await _categoriaRepository.ObterPorId(id);
            if (categoria == null)
            {
                return NotFound();
            }

            var categoriaViewModel = _mapper.Map<CategoriaViewModel>(categoria);
            return View(categoriaViewModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Route("excluir/{id:guid}")]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var categoria = await _categoriaRepository.ObterCategoriaProduto(id);
            if (categoria == null)
                return NotFound();

            if (categoria.Produtos.Any())
            {
                ModelState.AddModelError("", "Não é possível excluir uma categoria com produtos associados");
                var categoriaViewModel = _mapper.Map<CategoriaViewModel>(categoria);
                return View(categoriaViewModel);
            }

            await _categoriaRepository.Remover(categoria.Id);

            return RedirectToAction(nameof(Index));
        }

        private bool CategoriaExists(Guid id)
        {
            var retorno = _categoriaRepository.ObterPorId(id);
            return retorno != null;
        }
    }
}
