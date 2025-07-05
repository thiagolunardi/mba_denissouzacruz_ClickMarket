using ClickMarket.Business.Interfaces;
using ClickMarket.Business.Models;

namespace ClickMarket.Business.Services
{
    public class CategoriaService(ICategoriaRepository categoriaRepository) : ICategoriaService
    {
        private readonly ICategoriaRepository _categoriaRepository = categoriaRepository;

        public async Task Adicionar(Categoria categoria)
        {
            //regra de negócio
            await _categoriaRepository.Adicionar(categoria);
        }

        public async Task Atualizar(Categoria categoria)
        {
            //Regra de negócio
            await _categoriaRepository.Atualizar(categoria);
        }

        public async Task Remover(Guid id)
        {
            //Não é possível remover categoria com produto associado

            await _categoriaRepository.Remover(id);
        }

        public void Dispose()
        {
            _categoriaRepository?.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
