namespace ApisConvenciones9.Models
{
    public class Pregunta
    {
        //Relacion con la convencion a la que pertecene
        //Listado de preguntas que pertenecen a esta encuesta
        public int Id { get; set; }
        public string Texto { get; set; } = null!;
        public Evento Evento_Id { get; set; } = null!;
        public ICollection<Respuesta> Respuestas { get; set; } = null!;
    }
}
