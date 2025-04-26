using ApisConvenciones9.Models;
using Azure.Identity;
using Microsoft.AspNetCore.Identity;

namespace ApisConvenciones9.Servicios
{
    public class ServiciosUsuarios : IServiciosUsuarios
    {
        private readonly UserManager<Usuario> userManager;
        private readonly IHttpContextAccessor contextAccessor;

        public ServiciosUsuarios(UserManager<Usuario> userManager,
            IHttpContextAccessor contextAccessor)
        {
            this.userManager = userManager;
            this.contextAccessor = contextAccessor;
        }

        //Obtener usuario por username desde token
        public async Task<Usuario?> ObtenerUsuario()
        {
            var userNameClaim = contextAccessor.HttpContext!.User.Claims
                .Where(x => x.Type == "NombreUsuario").FirstOrDefault();

            if (userNameClaim is null)
            {
                return null;
            }

            var UserName = userNameClaim.Value;
            return await userManager.FindByNameAsync(UserName);

        }

        //Obtener usuario por UserName
        public async Task<Usuario?> ObtenerUsuario(string username)
        {
            var user = await userManager.FindByNameAsync(username);

            if (user is null)
            {
                return null;
            }
            
            return user;

        }
    }
}
