using Microsoft.AspNetCore.Identity;

namespace ApisConvenciones9.Models
{
    public class Usuario: IdentityUser
    {
        public string? Nombre { get; set; }
        public string? Apellidos { get; set; }
    }
}
