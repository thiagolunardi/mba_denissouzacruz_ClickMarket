using AutoMapper;
using ClickMarket.Api.Models;

namespace ClickMarket.Api.Configurations;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<CategoriaViewModel, Business.Models.Categoria>().ReverseMap();
        CreateMap<CategoriaListViewModel, Business.Models.Categoria>().ReverseMap();
        CreateMap<ProdutoViewModel, Business.Models.Produto>().ReverseMap();
        CreateMap<ProdutoListViewModel, Business.Models.Produto>().ReverseMap();
        CreateMap<VendedorViewModel, Business.Models.Vendedor>().ReverseMap();
    }
}
