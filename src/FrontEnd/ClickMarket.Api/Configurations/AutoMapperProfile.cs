using AutoMapper;

namespace ClickMarket.Api.Configurations
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Models.CategoriaViewModel, Business.Models.Categoria>().ReverseMap();
            CreateMap<Models.CategoriaListViewModel, Business.Models.Categoria>().ReverseMap();
            CreateMap<Models.ProdutoViewModel, Business.Models.Produto>().ReverseMap();
            CreateMap<Models.ProdutoListViewModel, Business.Models.Produto>().ReverseMap();
            CreateMap<Models.VendedorViewModel, Business.Models.Vendedor>().ReverseMap();

        }
    }
}
