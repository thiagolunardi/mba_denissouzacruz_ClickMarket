using AutoMapper;
using ClickMarket.AppMvc.ViewModels;
using ClickMarket.Business.Models;

namespace ClickMarket.AppMvc.Configurations;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        //Quando a aplicação subir, este mapeamento já é definido
        CreateMap<Categoria, CategoriaViewModel>().ReverseMap();
        CreateMap<Produto, ProdutoViewModel>()
            .ForMember(dest => dest.ImagemCaminho, opt => opt.MapFrom(src => ObterImagemCaminho(src)));
        CreateMap<ProdutoViewModel, Produto>();
        CreateMap<Vendedor, VendedorViewModel>().ReverseMap();

    }

    private static string ObterImagemCaminho(Produto produto)
    {
        //caminho da imagem do spa, ambas aplicações (front-end e back-end) estão rodando na mesma máquina
        //var caminhoBase = "~/images/upload/" //usar para testes locais sem SPA
        var caminhoBase = "https://localhost:7019/images/upload/";

        return string.IsNullOrEmpty(produto.Imagem) ? "no-image.png" : caminhoBase + produto.Imagem;
    }
}
