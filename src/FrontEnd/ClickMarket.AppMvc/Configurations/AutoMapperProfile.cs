using AutoMapper;
using ClickMarket.AppMvc.ViewModels;
using ClickMarket.Business.Models;

namespace ClickMarket.AppMvc.Configurations
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            //Quando a aplicação subir, este mapeamento já é definido
            CreateMap<Categoria, CategoriaViewModel>().ReverseMap();
            CreateMap<Produto, ProdutoViewModel>().ReverseMap();
            CreateMap<Vendedor, VendedorViewModel>().ReverseMap();

        }
    }
}
