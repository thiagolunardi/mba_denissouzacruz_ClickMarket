using AutoMapper;
using ClickMarket.Api.ViewModels;
using ClickMarket.Business.Models;

namespace ClickMarket.Api.Configurations;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<CategoriaViewModel, Categoria>().ReverseMap();
        CreateMap<ProdutoViewModel, Produto>();
        CreateMap<Produto, ProdutoViewModel>()
            .ForMember(dest => dest.NaListaDesejos, opt => opt.MapFrom(src => src.Favorito != null));
        CreateMap<VendedorViewModel, Vendedor>().ReverseMap();


        CreateMap<ClienteViewModel, Cliente>();
        CreateMap<Cliente, ClienteViewModel>();
        CreateMap<Favorito, FavoritoViewModel>();
    }
}
