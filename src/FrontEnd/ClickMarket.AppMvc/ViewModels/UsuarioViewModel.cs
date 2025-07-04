using Microsoft.AspNetCore.Identity;

namespace ClickMarket.AppMvc.ViewModels
{
    public class UsuarioViewModel : IdentityUser
    {
        public string Nome { get; set; }
    }
}
