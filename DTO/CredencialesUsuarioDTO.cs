using System.ComponentModel.DataAnnotations;

namespace ApisConvenciones9.DTO
{
    public class CredencialesUsuarioDTO
    {
        [Required]
        public required string UserName { get; set; }
        public string? Nombres { get; set; }
        public string? Apellidos { get; set; }
        [EmailAddress]
        public string? Email { get; set; }
        [Required]
        public string? Password { get; set; }
    }
}
