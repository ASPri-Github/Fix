namespace ApisConvenciones9.DTO
{
    public class ActualizarUsuarioDTO
    {
        public required string NombreUsuario { get; set; }
        public string Nombres { get; set; } = null!;
        public string Apellidos { get; set; } = null!;
        public string Email { get; set; } = null!;
    }
}
