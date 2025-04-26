using Microsoft.AspNetCore.Identity;

namespace ApisConvenciones9.Models
{
    public class Hotel
    {
        public int Id { get; set; }
        public string NombreHotel { get; set; } = null!;
        public string Telefono { get; set; } = null!;
        public string Direccion { get; set; } = null!;
        public string Latitud { get; set; } = null!;
        public string Longitud { get; set; } = null!;
        public string Imagen { get; set; } = null!;
        public Evento Evento_Id { get; set; } = null!;

        //Propiedad de navegacion para vincular con el usuario
        public required string UsuarioId { get; set; }
        public Usuario? Usuario {  get; set; }
    }
}
