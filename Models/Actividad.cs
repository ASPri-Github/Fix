namespace ApisConvenciones9.Models
{
    public class Actividad
    {
        public int Id { get; set; }
        public string Titulo { get; set; } = null!;
        public string Subtitulo { get; set; } = null!;
        public string Especificaciones { get; set; } = null!;
        public string Imagen { get; set; } = null!;
        public DateTime Fecha { get; set; }
        public Evento Evento_Id { get; set; } = null!;
        public Categoria_Actividades Categoria_Id { get; set; } = null!;
        //,[usuario_id]
    }
}
