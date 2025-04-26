namespace ApisConvenciones9.Models
{
    public class Respuesta
    {
        public int Id { get; set; }
        public Pregunta Pregunta_Id { get; set; } = null!;
        public string Comentario { get; set; } = null!;
        //  usuario
    }
}
