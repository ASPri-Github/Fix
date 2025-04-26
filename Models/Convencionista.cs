namespace ApisConvenciones9.Models
{
    public class Convencionista
    {
        public int Id { get; set; }
        public string Clave { get; set; } = null!;
        public string NombreCompleto { get; set; } = null!;
        public string Puesto { get; set; } = null!;
        public string Telefono { get; set; } = null!;
        public string Imagen { get; set; } = null!;
        public Evento Evento_Id { get; set; } = null!;
        public Perfil_Usuario PerfilUsuario_id { get; set; } = null!;
        public Categoria_Usuario CategoriaUsuario_Id { get; set; } = null!;
    }
}
