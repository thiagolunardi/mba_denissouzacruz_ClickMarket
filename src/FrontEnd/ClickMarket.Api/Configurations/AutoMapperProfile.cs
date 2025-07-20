using AutoMapper;
using ClickMarket.Api.Models;
using ClickMarket.Business.Dtos;
using ClickMarket.Business.Models;
using ClickMarket.Business.Requests;

namespace ClickMarket.Api.Configurations;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<CategoriaViewModel, Categoria>().ReverseMap();
        CreateMap<CategoriaListViewModel, Categoria>().ReverseMap();
        CreateMap<ProdutoViewModel, Produto>().ReverseMap();
        CreateMap<ProdutoListViewModel, Produto>();
        CreateMap<Produto, ProdutoListViewModel>()
            .ForMember(dest => dest.AddListaDesejos, opt => opt.MapFrom(src => src.Favorito != null));
        CreateMap<VendedorViewModel, Vendedor>().ReverseMap();


        CreateMap<ClienteRequest, Cliente>();
        CreateMap<Cliente, ClienteDto>();
        CreateMap<Favorito, FavoritoDto>();
        CreateMap<Produto, ProdutoDto>();
    }
}
