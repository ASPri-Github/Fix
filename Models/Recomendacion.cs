namespace ApisConvenciones9.Models
{
    public class Recomendacion
    {
        public int Id { get; set; }
        public string Titulo { get; set; } = null!;
        public string Informacion { get; set; } = null!;
        public string Latitud { get; set; } = null!;
        public string Longitud { get; set; } = null!;
        public string Imagen { get; set; } = null!;
        public Evento Evento_Id { get; set; } = null!;
        public Categoria_Recomendacion CategoriaRecomendacion_Id { get; set; } = null!;
        //usuario_id
    }
}
