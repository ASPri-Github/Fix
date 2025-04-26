using System.ComponentModel.DataAnnotations;

namespace ApisConvenciones9.DTO
{
    public class UsuarioDTO
    {
        public  required string NombreUsuario { get; set; }
        public string? Nombre { get; set; }
        public string? Apellidos { get; set; }
        //[EmailAddress]
        public string? Email { get; set; }
    }
}
